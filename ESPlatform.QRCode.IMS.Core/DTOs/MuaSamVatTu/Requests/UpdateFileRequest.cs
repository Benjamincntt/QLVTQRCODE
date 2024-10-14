using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests
{
    public class UpdateFileRequest
    {
        public int PhieuId { get; set; }
        public int VanBanId { get; set; }
        public int SignUserId { get; set; }
        public string? SignType { get; set; }
        public string? MaDoiTuongKy { get; set; }
        public IFormFile? FileData { get; set; }
        public int? ThuTuKy { get; set; }
        public int? ChuKyId { get; set; }
    }
}
