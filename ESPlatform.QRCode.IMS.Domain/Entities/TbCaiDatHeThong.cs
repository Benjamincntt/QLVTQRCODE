namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbCaiDatHeThong
{
    public int MaCaiDatHeThong { get; set; }

    public string TuKhoa { get; set; } = null!;

    public string GiaTri { get; set; } = null!;

    /// <summary>
    /// Sử dụng trong đồng bộ, =1 là kích hoạt đồng bộ tự động
    /// </summary>
    public bool? KichHoat { get; set; }

    /// <summary>
    /// Sử dụng trong đồng bộ, =1 là cho phép hiện thị tính năng đồng bộ tại giao diện quản lý
    /// </summary>
    public bool? KichHoaHienThi { get; set; }
}
