namespace ESPlatform.QRCode.IMS.Core.DTOs.KySo;

public class EmployeeProfileOutputDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string MobilePhone { get; set; }
    public string ViettelSimCaMobilePhone { get; set; }
    public string HoTen { get; set; }
    public string TenChucVu { get; set; }
    public string MaPhongBan { get; set; }
    public string TenPhongBan { get; set; }
    public string PhongBanColor { get; set; }
    public int CaLamViecId { get; set; }
    public Avatar Avatar { get; set; }
    public Signature Signature { get; set; }
}

public class Avatar
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string FileBytes { get; set; }
}
public class Signature
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string FileBytes { get; set; }
}