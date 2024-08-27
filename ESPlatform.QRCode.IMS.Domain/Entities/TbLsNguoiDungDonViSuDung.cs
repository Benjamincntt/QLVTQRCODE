namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbLsNguoiDungDonViSuDung
{
    public int MaLsNguoiDungDonViSuDung { get; set; }

    public int MaNguoiDung { get; set; }

    public int MaDonViSuDung { get; set; }

    public DateTime ThoiGianVao { get; set; }

    public DateTime? ThoiGianRa { get; set; }

    public virtual TbDonViSuDung MaDonViSuDungNavigation { get; set; } = null!;

    public virtual TbNguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
