using System.Security.Cryptography;

namespace ESPlatform.QRCode.IMS.Library.Utils;

public static class MiscHelper {
	public static string GenerateRandomBase64String(int dataSize = 64) {
		var data = new byte[dataSize];
		using var generator = RandomNumberGenerator.Create();
		generator.GetBytes(data);
		return Convert.ToBase64String(data);
	}
}