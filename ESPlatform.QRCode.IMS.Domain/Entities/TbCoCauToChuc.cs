namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbCoCauToChuc
{
    /// <summary>
    /// Mã đơn vị người dùng. Khóa chính
    /// </summary>
    public int MaCoCauToChuc { get; set; }

    /// <summary>
    /// Mã đơn vị sử dụng
    /// </summary>
    public int? MaDonViSuDung { get; set; }

    /// <summary>
    /// Tên đơn vị người dùng
    /// </summary>
    public string? TenCoCauToChuc { get; set; }

    /// <summary>
    /// Thuộc tính đánh dấu cạnh trái của 1 node trong kỹ thuật lưu trữ và truy vấn dữ liệu dạng cây
    /// </summary>
    public int? Lft { get; set; }

    /// <summary>
    /// Thuộc tính đánh dấu cạnh phải của 1 node trong kỹ thuật lưu trữ và truy vấn dữ liệu dạng cây
    /// </summary>
    public int? Rght { get; set; }

    /// <summary>
    /// Thuộc tính đánh dấu ID của node cha của 1 node trong kỹ thuật lưu trữ và truy vấn dữ liệu dạng cây
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// Số điện thoại
    /// </summary>
    public string? SoDienThoai { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Địa chỉ liên hệ
    /// </summary>
    public string? DiaChiLienHe { get; set; }

    /// <summary>
    /// Mã người dùng của người quản lý đơn vị người dùng
    /// </summary>
    public int? MaNguoiQuanLy { get; set; }

    public bool? DaXoa { get; set; }

    public int? MaNguoiDung { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhap { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<TbNguoiDungCoCauToChucO> TbNguoiDungCoCauToChucOs { get; set; } = new List<TbNguoiDungCoCauToChucO>();
}
