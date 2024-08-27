namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbHopThuDen
{
    public int MaTinNhan { get; set; }

    public int MaNguoiDung { get; set; }

    public bool DaXem { get; set; }

    public DateTime NgayXem { get; set; }

    public virtual TbNguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual TbTinNhan MaTinNhanNavigation { get; set; } = null!;
}
