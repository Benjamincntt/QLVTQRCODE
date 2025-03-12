namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;

public class SupplyTicketListResponseItem
{
    public long Id { get; set; }
    
    public string MaPhieu { get; set; } = string.Empty;

    public string TenPhieu { get; set; } = string.Empty;

    public string MoTa { get; set; } = string.Empty;
    
    public DateTime NgayThem { get; set; }

    public byte TrangThai { get; set; } = 0;
    
    public string MaMau { get; set; } = string.Empty;

    public string TenTrangThai { get; set; } = string.Empty;
    
    // Các trường update để ký số
    
    /// <summary>
    /// Cho phép chức năng bỏ qua ký
    /// </summary>
    public bool IsBoQua { get; set; }
    
    /// <summary>
    /// Cho phép user được ký
    /// </summary>
    public bool IsKySo { get; set; }
}