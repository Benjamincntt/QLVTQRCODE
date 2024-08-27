namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbTinNhan
{
    public int MaTinNhan { get; set; }

    public int MaNguoiDung { get; set; }

    public string? ChuDe { get; set; }

    public string NoiDung { get; set; } = null!;

    public string? TepDinhKem { get; set; }

    public DateTime NgayGui { get; set; }

    public int? MaTinNhanPhanHoi { get; set; }

    public bool LaCaNhan { get; set; }

    public bool DaXoa { get; set; }

    public virtual ICollection<TbTinNhan> InverseMaTinNhanPhanHoiNavigation { get; set; } = new List<TbTinNhan>();

    public virtual TbNguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual TbTinNhan? MaTinNhanPhanHoiNavigation { get; set; }

    public virtual ICollection<TbHopThuDen> TbHopThuDens { get; set; } = new List<TbHopThuDen>();

    public virtual ICollection<TbNguoiDung> MaNguoiDungs { get; set; } = new List<TbNguoiDung>();
}
