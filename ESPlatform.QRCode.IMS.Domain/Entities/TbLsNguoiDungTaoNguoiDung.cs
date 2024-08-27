namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbLsNguoiDungTaoNguoiDung
{
    public int MaLsNguoiDungTaoNguoiDung { get; set; }

    public int MaNguoiTacDong { get; set; }

    public int MaNguoiBiTacDong { get; set; }

    public DateTime ThoiGian { get; set; }

    public virtual TbNguoiDung MaNguoiBiTacDongNavigation { get; set; } = null!;

    public virtual TbNguoiDung MaNguoiTacDongNavigation { get; set; } = null!;
}
