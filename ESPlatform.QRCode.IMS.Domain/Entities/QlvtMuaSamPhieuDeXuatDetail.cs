namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtMuaSamPhieuDeXuatDetail
{
    public int Id { get; set; }

    /// <summary>
    /// Nếu IdVatTu tồn tại thì vật tư được lấy từ hệ thông ERP, nếu không tồn tại thì Vật tư đc thêm mới vào phiếu đề xuất
    /// </summary>
    public int? IdVatTu { get; set; }
    
    /// <summary>
    /// Id phiếu đề xuất
    /// </summary>
    public int PhieuDeXuatId { get; set; }

    public string? TenVatTu { get; set; }

    public int? SoLuong { get; set; }

    public string? DonViTinh { get; set; }

    public string? ThongSoKyThuat { get; set; }

    public string? XuatXu { get; set; }

    public string? GhiChu { get; set; }
    
    /// <summary>
    /// 0: Vật tư chưa có trong hệ thống, 1: vật tư đã có trong hệ thống
    /// </summary>
    public bool? IsSystemSupply { get; set; }
}
