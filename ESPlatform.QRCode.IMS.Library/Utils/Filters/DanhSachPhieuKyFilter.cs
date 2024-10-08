using ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Library.Utils.Filters
{
    public class DanhSachPhieuKyFilter
    {
        [DisplayName("Từ khóa")]
        public string? Keywords { get; set; } = string.Empty;
        //[DisplayName("Ngày bắt đầu")]
        //public DateTime FromDate { get; set; }

        //[DisplayName("Ngày kết thúc")]
        //public DateTime ToDate { get; set; }
        //public DanhSachPhieuKyFilter()
        //{
        //    // Khởi tạo FromDate và ToDate với giá trị mặc định
        //    FromDate = DateTime.MinValue; // Hoặc một giá trị mặc định khác
        //    ToDate = DateTime.MaxValue; // Hoặc một giá trị mặc định khác
        //}
        [DisplayName("Ngày bắt đầu")]
        public string FromDate { get; set; }

        [DisplayName("Ngày kết thúc")]
        public string ToDate { get; set; }

        public DanhSachPhieuKyFilter()
        {
            // Gán giá trị mặc định cho FromDate và ToDate là chuỗi rỗng
            FromDate = string.Empty;
            ToDate = string.Empty;
        }
    }
}
