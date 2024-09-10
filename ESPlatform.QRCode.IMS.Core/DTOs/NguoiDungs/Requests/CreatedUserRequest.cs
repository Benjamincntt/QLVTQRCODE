namespace ESPlatform.QRCode.IMS.Core.DTOs.NguoiDungs.Requests;

public class CreatedUserRequest : NguoiDungDto
{
    public string MatKhau { get; set; } = string.Empty;
    
    public string SoDienThoai { get; set; } = string.Empty;
    
    public int MaDonViSuDung { get; set; }
    
    public bool GioiTinh { get; set; } = false;
}