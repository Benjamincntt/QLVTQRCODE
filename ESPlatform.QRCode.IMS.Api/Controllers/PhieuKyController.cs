using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;
using ESPlatform.QRCode.IMS.Core.Services.PhieuKy;
using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static MassTransit.ValidationResultExtensions;


namespace ESPlatform.QRCode.IMS.Api.Controllers
{
    [Route("/api/v1/phieu-ky")]
    public class PhieuKyController : ApiControllerBase
    {
        private readonly IPhieuKyService _phieuKyService;
        public PhieuKyController(IPhieuKyService phieuKyService)
        {
            _phieuKyService = phieuKyService;
        }
        [HttpGet("danh-sach-phieu")]
        public async Task<ActionResult<IEnumerable<PhieuKyModel>>> DanhSachPhieuKyAsync([FromQuery] DanhSachPhieuKyFilter request)
        {
            try
            {
                var result = await _phieuKyService.GetDanhSachPhieuKyAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về mã lỗi phù hợp
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("bo-qua-ky")]
        public async Task<IActionResult> BoQuaKyAsync([FromBody] ModifiedKySo requests)
        {
            try
            {
                var result = await _phieuKyService.BoQuaKhongKy(requests);
                return Ok(new { success = true, data = result });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra, vui lòng thử lại sau." });
            }
        }
        [HttpGet("check-file-exists")]
        public async Task<IActionResult> CheckFileExistsAsync(int id)
        {
            try
            {
                var result = await _phieuKyService.GetVanBanKyById(id);

                if (string.IsNullOrEmpty(result.FilePath))
                {
                    return BadRequest(new { exists = false, message = "File không tồn tại", data = result });
                }
                // Tạo đường dẫn tuyệt đối
                // Lấy đường dẫn đầy đủ của file
                var fullPath = await _phieuKyService.GetFullFilePath(result.FilePath);
                var kySoPath = await _phieuKyService.GetRelativePath();
                // Kiểm tra file có tồn tại hay không
                bool fileExists = System.IO.File.Exists(fullPath);

                if (fileExists)
                {
                    result.kySoPath = kySoPath;
                    return Ok(new { exists = true, message = "Có tồn tại file", data = result });
                }
                else
                {
                    return NotFound(new { exists = false, message = "Không tồn tại file" });
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra, vui lòng thử lại sau." });
            }
        }

        [HttpPut("UpdateFileCungUng")]
        public async Task<IActionResult> UpdateFileCungUng([FromForm] UpdateFileRequest request)
        {
            try
            {
                // Kiểm tra file upload
                if (request.FileData == null || request.FileData.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Lấy thông tin văn bản ký từ database
                var result = await _phieuKyService.GetVanBanKyById(request.VanBanId);
                if (string.IsNullOrEmpty(result.FilePath))
                {
                    return BadRequest(new { exists = false, message = "File không tồn tại", data = result });
                }

                // Lấy đường dẫn đầy đủ của file gốc
                var fullPath = await _phieuKyService.GetFullFilePath(result.FilePath);

                // Kiểm tra xem file gốc có tồn tại hay không
                if (!System.IO.File.Exists(fullPath))
                {
                    return NotFound(new { exists = false, message = "File không tồn tại" });
                }

                try
                {
                    // Ghi đè file gốc bằng file mới
                    using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        await request.FileData.CopyToAsync(stream);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    return StatusCode(403, new { message = "Bạn không có quyền ghi vào file này." });
                }
                catch (IOException ioEx)
                {
                    return StatusCode(500, new { message = "Lỗi ghi file: " + ioEx.Message });
                }

                // Cập nhật thông tin ký trong database
                var resultUpdate = await _phieuKyService.UpdateThongTinKyAsync(request);

                return Ok(new { success = true, message = "Ký thành công", data = result });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi chi tiết để phục vụ việc debug
                return StatusCode(500, new { message = "Có lỗi xảy ra, vui lòng thử lại sau." +  ex.Message });
            }
        }

        [HttpGet("GetFilePdf/{id}")]
        public async Task<IActionResult> GetPdf(int id)
        {
            try
            {
                // Lấy thông tin file dựa trên id
                var result = await _phieuKyService.GetVanBanKyById(id);

                // Kiểm tra xem result có null không và file path có tồn tại không
                if (result == null || string.IsNullOrEmpty(result.FilePath))
                {
                    return BadRequest(new { exists = false, message = "File không tồn tại", data = result });
                }

                // Lấy đường dẫn đầy đủ của file
                var fullPath = await _phieuKyService.GetFullFilePath(result.FilePath);

                // Kiểm tra file có tồn tại không
                if (!System.IO.File.Exists(fullPath))
                {
                    return NotFound("File không tồn tại");
                }

                // Đọc file và trả về kết quả dưới dạng PDF
                var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(fileBytes, "application/pdf");
            }
            catch (FileNotFoundException)
            {
                return NotFound("File không tồn tại hoặc đã bị xóa.");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Bạn không có quyền truy cập vào file này.");
            }
            catch (Exception ex)
            {
                // Log exception (nếu cần)
                // logger.LogError(ex, "Lỗi khi lấy file PDF với ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý yêu cầu.");
            }
        }



    }
}
