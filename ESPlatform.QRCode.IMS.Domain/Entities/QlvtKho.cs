namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtKho
{
    public int OrganizationId { get; set; }

    /// <summary>
    /// Mã Kho
    /// </summary>
    public string? OrganizationCode { get; set; }

    /// <summary>
    /// Tên Kho
    /// </summary>
    public string? OrganizationName { get; set; }

    /// <summary>
    /// Id Tổng kho
    /// </summary>
    public int? MasterOrganizationId { get; set; }

    /// <summary>
    /// Id đơn vị
    /// </summary>
    public int? OperatingUnit { get; set; }

    /// <summary>
    /// Phương pháp tính giá
    /// </summary>
    public string? PrimaryCostMethod { get; set; }

    /// <summary>
    /// Loại kho
    /// </summary>
    public string? OrganizationType { get; set; }

    /// <summary>
    /// Mã kho phụ
    /// </summary>
    public string? SubInventoryCode { get; set; }

    /// <summary>
    /// Tên kho phụ
    /// </summary>
    public string? SubInventoryName { get; set; }

    /// <summary>
    /// Người tạo
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Creation_Date
    /// </summary>
    public DateTime? CreationDate { get; set; }
}
