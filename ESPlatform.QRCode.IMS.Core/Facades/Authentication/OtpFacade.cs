using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using OtpNet;
using QRCoder;

namespace ESPlatform.QRCode.IMS.Core.Facades.Authentication;

public class OtpFacade {
	private readonly OtpSettings _otpSettings;

	public OtpFacade(OtpSettings otpSettings) {
		_otpSettings = otpSettings;
	}

	public static string GenerateSecretKey() {
		var bytes = KeyGeneration.GenerateRandomKey();
		return Base32Encoding.ToString(bytes) ?? string.Empty;
	}

	public byte[] GenerateQrCode(string otpSecretKey, string username) {
		var otpUri = new OtpUri(OtpType.Totp,
								Base32Encoding.ToBytes(otpSecretKey),
								username,
								_otpSettings.Issuer,
								algorithm: OtpHashMode.Sha1,
								digits: _otpSettings.Digits,
								period: _otpSettings.Period);

		var qrGenerator = new QRCodeGenerator();
		var qrCodeData = qrGenerator.CreateQrCode(otpUri.ToString(), QRCodeGenerator.ECCLevel.Q);
		var qrCode = new PngByteQRCode(qrCodeData);
		return qrCode.GetGraphic(1);
	}

	public bool VerifyOtp(string otpValue, string otpSecretKey) {
		var otp = new Totp(Base32Encoding.ToBytes(otpSecretKey),
						   mode: OtpHashMode.Sha1,
						   totpSize: _otpSettings.Digits,
						   step: _otpSettings.Period);
		return otp.VerifyTotp(otpValue, out _);
	}
}
