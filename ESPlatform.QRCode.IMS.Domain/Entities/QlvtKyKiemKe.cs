namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtKyKiemKe
{
    public int Id { get; set; }

    /// <summary>
    /// Id kho
    /// </summary>
    public int? OrganizationId { get; set; }

    /// <summary>
    /// Mã kho
    /// </summary>
    public string? OrganizationCode { get; set; }

    /// <summary>
    /// Id kỳ kiểm kê
    /// </summary>
    public int? PhysicalInventoryId { get; set; }

    /// <summary>
    /// Tên kỳ kiểm kê
    /// </summary>
    public string? PhysicalInventoryName { get; set; }

    /// <summary>
    /// Diễn giải
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ngày kiểm kê
    /// </summary>
    public DateTime? PhysicalInventoryDate { get; set; }

    /// <summary>
    /// Ngày khó
    /// </summary>
    public DateTime? FreezeDate { get; set; }

    /// <summary>
    /// Số thẻ đầu tiên
    /// </summary>
    public string? StartTag { get; set; }

    /// <summary>
    /// Số thẻ kết thúc
    /// </summary>
    public string? EndTag { get; set; }

    /// <summary>
    /// Người tạo
    /// </summary>
    public string? UserName { get; set; }

    public short? KyKiemKeChinh { get; set; }

    public virtual ICollection<QlvtKyKiemKeChiTiet> QlvtKyKiemKeChiTiets { get; set; } = new List<QlvtKyKiemKeChiTiet>();
}

