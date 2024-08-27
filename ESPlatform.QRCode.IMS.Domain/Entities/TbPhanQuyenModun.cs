namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbPhanQuyenModun
{
    public int MaModunKhongPhanQuyen { get; set; }

    public int? MaModun { get; set; }

    public int? MaLoaiKieuNguoiDung { get; set; }

    public string? TenHienThi { get; set; }

    public string? TenController { get; set; }

    public string? TenAction { get; set; }

    public string? Icon { get; set; }

    public bool? CoKiemTraQuyen { get; set; }
}
