using ESPlatform.QRCode.IMS.Core.DTOs.Viettel;
using MassTransit.Configuration;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KySo.Response;

public class SignInfomationReponse
{
    public string? PdfPath { get; set; }
    
    //Thời gian chờ ký
    public long? TimeOut { get; set; } = 1000;
    
    public bool IsHuyDeXuat { get; set; } = false;
    
    public bool IsHuyDuyet { get; set; } = false;
    
    
    
    /// Thông tin file ký
    public SignFileImgDto? SignFileInfo { get; set; }

    public ChuKyRequest? ChuKyRequest { get; set; } = new ChuKyRequest();

    public List<SignHistoryResponseItem> SignTicketResponseItems { get; set; } = new List<SignHistoryResponseItem>();
    
    public List<string> ListFullPaths { get; set; } = null!;
    
}