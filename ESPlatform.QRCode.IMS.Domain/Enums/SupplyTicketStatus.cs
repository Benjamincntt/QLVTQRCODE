namespace ESPlatform.QRCode.IMS.Domain.Enums;
// lấy từ bảng TbCauHinhTrangThai
public enum SupplyTicketStatus
{
    Unknown = 0,
    Unsigned = 1,
    SigningInProgress = 2,
    Signed = 3,
    Deleted = 4,
    CancelledProposal = 5,    // Huỷ đề xuất
    CancelledApproval = 6     // Huỷ duyệt
}