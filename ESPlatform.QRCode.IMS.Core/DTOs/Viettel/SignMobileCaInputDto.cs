using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using Microsoft.AspNetCore.Http;
using MobileCA.Application.Services.Viettel.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.DTOs.Viettel
{
    public class SignMobileCaInputDto
    {
        /// <summary>
        /// Số điện thoại của khách hàng sử dụng chữ ký số trong giao dịch. 
        /// Định dạng 84xxxxx. 
        /// Ví dụ: 84984012500
        /// </summary>
        public string MobilePhone { get; set; } = null!;
        /// <summary>
        /// CertSerial của từng tài khoản ký số mà NCC cung cấp cho từng tài khoản
        /// </summary>
        public string? CertSerial { get; set; }
        /// <summary>
        /// Tiêu đề hiển thị khi ký số
        /// </summary>
        public string? DataToDisplayed { get; set; }

        /// <summary>
        /// Đường dẫn file Pdf trước khi ký
        /// </summary>
        public string? PdfPath { get; set; }
        /// <summary>
        /// Đường dẫn file Pdf sau khi ký số
        /// </summary>
        public string? PdfPathSigned { get; set; }

        /// <summary>
        /// Thời gian chờ ký
        /// </summary>
        public long? TimeOut { get; set; }

        /// <summary>
        /// Thông tin file ký
        /// </summary>
        //public SignMobileCaLocationInputDto? SignFileInfo { get; set; }
        //public ISignFileDto? SignFileInfo { get; set; }
        public SignFileImgDto? SignFileInfo { get; set; }

        public ChuKyRequest? ChuKyRequest { get; set; } = new ChuKyRequest();
    }
    public class ChuKyRequest
    {
        public int PhieuId { get; set; }
        public int VanBanId { get; set; }
        public int? SignUserId { get; set; }
        public string SignType { get; set; } = string.Empty;
        public string? MaDoiTuongKy { get; set; }
        public int? ThuTuKy { get; set; }
        public int ChuKyId { get; set; }
        public int? NguoiKyId { get; set; }
        public IFormFile? FileDataImageSign { get; set; }

    }

}
