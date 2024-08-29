namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtMuaSamPhieuDeXuatDetail
{
    public int Id { get; set; }

    /// <summary>
    /// Nếu IdVatTu tồn tại thì vật tư được lấy từ hệ thông ERP, nếu không tồn tại thì Vật tư đc thêm mới vào phiếu đề xuất
    /// </summary>
    public int? IdVatTu { get; set; }

    public string? MaVatTu { get; set; }

    public string? TenVatTu { get; set; }

    public int? SoLuong { get; set; }

    public string? DonViTinh { get; set; }

    public string? ThongSoKyThuat { get; set; }

    public string? XuatXu { get; set; }

    public string? MoTa { get; set; }
}
