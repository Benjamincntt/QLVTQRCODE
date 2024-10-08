using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests
{
    public class KyPhieuRequest
    {
        [DisplayName("Lý do")]
        public string? LyDo { get; set; } = string.Empty;
        [DisplayName("Usb_Serial")]
        public string? Usb_Serial { get; set; }
        [Required(ErrorMessage = "Danh sách phiếu ký không được bỏ trống.")]
        public List<ModifiedKySo> ItemsPhieuKy { get; set; } = new List<ModifiedKySo>();
    }
    public class ModifiedKySo
    {
        [DisplayName("Id Phiếu cung ứng")]
        public int PhieuId { get; set; }
        [DisplayName("Id Phiếu danh sách chữ ký")]
        public int ChuKyId { get; set; }
        [DisplayName("Thứ tự chứ ký")]
        public int ThuTu_Ky { get; set; }
        [DisplayName("Văn bản ký Id")]
        public int VanBan_Id { get; set; }
        [DisplayName("Mã đối tượng ký")]
        public string? MaDoiTuongKy { get; set; }
    }
}
