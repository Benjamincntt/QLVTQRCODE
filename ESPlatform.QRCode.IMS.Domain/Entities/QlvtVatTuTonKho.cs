namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtVatTuTonKho
{
    /// <summary>
    /// Id vật tư
    /// </summary>
    public int InventoryItemId { get; set; }

    /// <summary>
    /// ID kho
    /// </summary>
    public int OrganizationId { get; set; }

    /// <summary>
    /// Số lô vật tư
    /// </summary>
    public string? LotNumber { get; set; }

    /// <summary>
    /// Số lượng tồn
    /// </summary>
    public int? OnhandQuantity { get; set; }

    /// <summary>
    /// Mã kho phụ
    /// </summary>
    public string? SubinventoryCode { get; set; }

    /// <summary>
    /// Người tạo
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Ngày tạo
    /// </summary>
    public DateTime? CreationDate { get; set; }
}
