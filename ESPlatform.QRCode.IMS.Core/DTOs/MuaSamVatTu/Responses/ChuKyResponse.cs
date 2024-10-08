using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses
{
    public class ChuKyResponse
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
    }
}
