namespace ESPlatform.QRCode.IMS.Domain.Enums;
// lấy từ bảng TbCauHinhTrangThai
public enum SupplyTicketStatus
{
    Unknown = 0,
    Unsigned = 1,
    SigningInProgress = 2,
    Signed = 3,
    Deleted = 4
}