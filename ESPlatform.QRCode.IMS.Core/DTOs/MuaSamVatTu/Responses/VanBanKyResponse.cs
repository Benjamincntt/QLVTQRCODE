using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses
{
    public class VanBanKyResponse
    {
        public int Id { get; set; }
        public int? PhieuId { get; set; }
        public bool? TrangThaiTaoFile { get; set; }
        public string? MaLoaiVanBan { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
