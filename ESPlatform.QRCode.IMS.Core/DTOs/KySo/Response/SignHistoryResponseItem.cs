using ESPlatform.QRCode.IMS.Domain.Enums;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KySo.Response;

public class SignHistoryResponseItem
{
    public string UserName { get; set; }
    public string TenViTriCongViec { get; set; }
    public DateTime NgayKy { get; set; }
    public TrangThaiChuKy TrangThai { get; set; }
}