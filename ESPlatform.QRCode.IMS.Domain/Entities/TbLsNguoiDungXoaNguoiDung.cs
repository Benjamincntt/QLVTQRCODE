namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbLsNguoiDungXoaNguoiDung
{
    public int MaLsNguoiDungXoaNguoiDung { get; set; }

    public int MaNguoiTacDong { get; set; }

    public int MaNguoiBiTacDong { get; set; }

    public DateTime ThoiGian { get; set; }

    /// <summary>
    /// Mã người dùng thực hiện khôi phục 1 bản ghi hay xóa vĩnh viễn 1 bản ghi khỏi thùng rác
    /// </summary>
    public int? MaNguoiThucHienKhoiPhuc { get; set; }

    /// <summary>
    /// Thời gian khôi phục hay xóa vĩnh viễ 1 bản ghi trong thùng rác
    /// </summary>
    public DateTime? ThoiGianThucHien { get; set; }

    /// <summary>
    /// Trạng thái: =1 hiển thị;=2 đã khôi phục (không hiển thị);= 10 xóa vĩnh viễn (không hiển thị)
    /// </summary>
    public byte? TranngThai { get; set; }

    public virtual TbNguoiDung MaNguoiBiTacDongNavigation { get; set; } = null!;

    public virtual TbNguoiDung MaNguoiTacDongNavigation { get; set; } = null!;
}
