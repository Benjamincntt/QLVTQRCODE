namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtVatTuImage
{
    /// <summary>
    /// Tên folder chứa danh sách ảnh lấy theo IDVatTu
    /// </summary>
    public int IdvatTu { get; set; }

    /// <summary>
    /// Tên ảnh.
    /// </summary>
    public string? Image { get; set; }
}
