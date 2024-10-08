using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses
{
    public class DanhSachPhieuKyResponse
    {
        public int Id { get; set; }

        public string? MaPhieu { get; set; }

        public string? TenPhieu { get; set; }

        public string? MoTa { get; set; }

        public DateTime? NgayThem { get; set; }

        public byte? TrangThai { get; set; }
        public VanBanKyResponse VanBanKy { get; set; } = new VanBanKyResponse();
        public ChuKyResponse? ChuKys { get; set; } = new ChuKyResponse();
    }
}
