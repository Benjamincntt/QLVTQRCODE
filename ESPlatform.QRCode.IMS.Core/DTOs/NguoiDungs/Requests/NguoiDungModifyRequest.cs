using ESPlatform.QRCode.IMS.Domain.Enums;

namespace ESPlatform.QRCode.IMS.Core.DTOs.NguoiDungs.Requests;

public class NguoiDungModifyRequest : NguoiDungDto {
	// public string MaDinhDanh { get; set; } = string.Empty;

	// public string MatKhau { get; set; } = string.Empty;

	public string Ho { get; set; } = string.Empty;

	public string Ten { get; set; } = string.Empty;

	public string AnhDaiDien { get; set; } = string.Empty;

	public bool? GioiTinh { get; set; }

	public string HocHam { get; set; } = string.Empty;

	public string HocVi { get; set; } = string.Empty;

	public string NoiCongTac { get; set; } = string.Empty;

	public string SoDienThoai { get; set; } = string.Empty;

	public bool? KichHoat { get; set; }

	public string TieuSu { get; set; } = string.Empty;

	public double? TienTrongTaiKhoan { get; set; }

	//public int SoNguoiDungThich { get; set; }

	public bool? CoChoPhepHienThi { get; set; }

	//public DateTime NgayTao { get; set; }

	// public int MaTimeZone { get; set; }

	// public int MaKieuNguoiDung { get; set; }
	//
	// public int MaDonViSuDung { get; set; }

	public bool LaQuanTriVienCaoCap { get; set; }

	public bool DaXoa { get; set; }

	// public string Salt { get; set; } = string.Empty;

	public int IdluongXuLy { get; set; }

	public int IddonVi { get; set; }

	//public DateTime? ThoiGianMatKhau { get; set; }

	public string QueQuan { get; set; } = string.Empty;

	public string ChucVu { get; set; } = string.Empty;

	/// <summary>
	/// 1: Cán bộ; 2: Công chức; 3: Viên chức
	/// </summary>
	public DoiTuongNguoiDung DoiTuong { get; set; }
	//
	// public int PhienBanSuDung { get; set; }
}