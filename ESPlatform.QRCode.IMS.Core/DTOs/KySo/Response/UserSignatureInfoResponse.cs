namespace ESPlatform.QRCode.IMS.Core.DTOs.KySo.Response;

public class UserSignatureInfoResponse
{
    public string? ViettelSimCaMobilePhone { get; set; }
    public SignatureResponse? SignatureResponse { get; set; }
}

public class SignatureResponse
{
    public string? FileName { get; set; } = string.Empty;
    
    public string? ContentType { get; set; } = string.Empty;
    public string? FileBytes { get; set; } = string.Empty;
}
