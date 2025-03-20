namespace ESPlatform.QRCode.IMS.Core.DTOs.KySo.Requests;

public class CancelTicketRequest
{
    // Dạng phiếu bị hủy
    public bool IsPhieuDeXuat { get; set; }
    // Lý do huỷ
    public string? Reason { get; set; }
    // local path của file duyệt
    public string? PhieuDuyetRelativePath { get; set; }
}