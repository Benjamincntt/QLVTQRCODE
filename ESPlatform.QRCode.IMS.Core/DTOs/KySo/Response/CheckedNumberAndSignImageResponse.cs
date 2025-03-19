namespace ESPlatform.QRCode.IMS.Core.DTOs.KySo.Response;

public class CheckedNumberAndSignImageResponse
{
    public string ViettelSimCaMobilePhone { get; set; } = string.Empty;
    
    public string? PathImage { get; set; }

    public string PhysicalFilePath { get; set; }
}