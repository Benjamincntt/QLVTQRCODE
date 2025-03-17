using ESPlatform.QRCode.IMS.Core.DTOs.Viettel;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KySo.Response;

public class SignInfomationReponse
{
    public string ViettelSimCaMobilePhone { get; set; } = null!;

    //Đường dẫn file Pdf trước khi ký
    public string? PdfPath { get; set; }

    //Đường dẫn file Pdf sau khi ký số
    public string? PdfPathSigned { get; set; }
    
    //Thời gian chờ ký
    public long? TimeOut { get; set; }
    
    /// Thông tin file ký
    public SignFileImgDto? SignFileInfo { get; set; }

    public ChuKyRequest? ChuKyRequest { get; set; } = new ChuKyRequest();
}