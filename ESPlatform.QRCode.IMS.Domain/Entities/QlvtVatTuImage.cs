namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtVatTuImage
{
    /// <summary>
    /// Tên folder chứa danh sách ảnh lấy theo IdVatTu
    /// </summary>
    public int IdVatTu { get; set; }

    /// <summary>
    /// Tên ảnh.
    /// </summary>
    public int AttachmentId { get; set; }
}
