using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Domain.Models.MuaSam
{
    public class PhieuKyModel
    {
        public int Id { get; set; }

        public string? MaPhieu { get; set; }

        public string? TenPhieu { get; set; }

        public string? MoTa { get; set; }

        public int? MaDonViSuDung { get; set; }

        public DateTime? NgayThem { get; set; }

        public int? MaNguoiThem { get; set; }

        public DateTime? NgaySua { get; set; }

        public int? MaNguoiSua { get; set; }

        public byte? TrangThai { get; set; }

        public string? GhiChu { get; set; }
        public VanBanKyModel? VanBanKy { get; set; } = new VanBanKyModel();

        public ChuKyModel? ChuKy { get; set; } = new ChuKyModel();
        public string? TenDonViSuDung { get; set; }
        public string? TenNguoiThem { get; set; }
    }
    public class ChuKyModel
    {
        public int ChuKyId { get; set; }
        public string? UsbSerial { get; set; }
        public int? PhieuDeXuatId { get; set; }

        public int? NguoiKyId { get; set; }

        public string? LyDo { get; set; }

        public short? ThuTuKy { get; set; }

        public byte? TrangThai { get; set; }

        public string? MaDoiTuongKy { get; set; }

        public string? ToaDo { get; set; }

        public DateTime? NgayKy { get; set; }

        public int? VanBanId { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }
        public int? page { get; set; }
        public float? pageHeight { get; set; }
    }
    public class VanBanKyModel
    {
        public int Id { get; set; }
        public int? PhieuId { get; set; } 
        public bool? TrangThaiTaoFile { get; set; }
        public string? MaLoaiVanBan { get; set; }
        public string? FilePath { get; set; }
        public string ? FileName { get; set; }
        public DateTime? NgayTao { get; set; }

    }

}
