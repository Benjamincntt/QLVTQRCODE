using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Domain.Entities;
[DisplayName("Vật tư tồn kho")]
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
    public decimal? OnhandQuantity { get; set; }

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

    public string? MaVatTu { get; set; }

    public string? TenVatTu { get; set; }

    public string? DonViTinh { get; set; }

    public short? TrangThaiInQr { get; set; }

    public int VatTuId { get; set; }

    public int KhoId { get; set; }

    public string? MaKho { get; set; }
}
