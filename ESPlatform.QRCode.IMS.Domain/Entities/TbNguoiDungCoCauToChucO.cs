namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbNguoiDungCoCauToChucO
{
    /// <summary>
    /// Mã người dùng _ đơn vị người dùng. Khóa chính
    /// </summary>
    public int MaNguoiDungCoCauToChuc { get; set; }

    /// <summary>
    /// Mã người dùng
    /// </summary>
    public int MaNguoiDung { get; set; }

    /// <summary>
    /// Mã đơn vị người dùng
    /// </summary>
    public int MaCoCauToChuc { get; set; }

    /// <summary>
    /// Ngày người dùng bắt đầu thuộc đơn vị
    /// </summary>
    public DateTime? TuNgay { get; set; }

    /// <summary>
    /// Ngày người dùng hết thuộc đơn vị
    /// </summary>
    public DateTime? DenNgay { get; set; }

    /// <summary>
    /// Đánh dấu người dùng có đang thuộc đơn vị hay không. Mặc định là 0 (False)
    /// </summary>
    public bool? HienTai { get; set; }

    /// <summary>
    /// Chức danh của người dùng trong đơn vị đó
    /// </summary>
    public string? ChucDanh { get; set; }

    public virtual TbCoCauToChuc MaCoCauToChucNavigation { get; set; } = null!;

    public virtual TbNguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
