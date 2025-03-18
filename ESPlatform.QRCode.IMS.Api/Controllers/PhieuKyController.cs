using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.Viettel;
using ESPlatform.QRCode.IMS.Core.Services.PhieuKy;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Diagnostics;
using ESPlatform.QRCode.IMS.Core.DTOs.KySo.Response;
using ESPlatform.QRCode.IMS.Core.Engine;

namespace ESPlatform.QRCode.IMS.Api.Controllers;

[Route("/api/v1/phieu-ky")]
public class PhieuKyController : ApiControllerBase
{
    private readonly IPhieuKyService _phieuKyService;
    private readonly IUnitOfWork _unitOfWork;

    public PhieuKyController(IPhieuKyService phieuKyService,
        IUnitOfWork unitOfWork)
    {
        _phieuKyService = phieuKyService;
        _unitOfWork = unitOfWork;
    }
    
    /// <summary>
    /// Huỷ 1 phiếu(dành cho người ký số 5 và 6)
    /// </summary>
    /// <param name="ticketId"> Mã phiếu</param>
    /// <param name="isPhieuDeXuat"> true: phiếu đề xuất, false: phiếu duyệt</param>
    /// <param name="reason"> Lý do huỷ </param>
    /// <returns></returns>
    [HttpPatch("{ticketId:int}/{isPhieuDeXuat:bool}/cancel-ticket")]
    public async Task<IActionResult> CancelTicketAsync(int ticketId, bool isPhieuDeXuat, [FromBody] string? reason)
    {
        var result = await _phieuKyService.CancelTicketAsync(ticketId, isPhieuDeXuat, reason);
        if (result > 0)
        {
            return Ok(Constants.Exceptions.Messages.SupplyTicket.TicketCancelSuccess);
        }
        else
        {
            return StatusCode(500, Constants.Exceptions.Messages.SupplyTicket.TicketCancelSuccess);
        }
    }

    /// <summary>
    /// Hiển thị thông tin người ký
    /// </summary>
    /// <param name="phieuId"> Mã phiếu </param>
    /// <returns> Ảnh chữ ký và số điện thoại </returns>
    [HttpGet("{phieuId:int}/check-sign-user-info")]
    public async Task<IActionResult> CheckedNumberAndSignImageAsync(int phieuId)
    {
        var accessToken = GetAccessToken();
        var signInfo = await _phieuKyService.CheckedNumberAndSignImageAsync(phieuId, accessToken);
        if (signInfo is null)
        {
            return StatusCode(500, Constants.Exceptions.Messages.KyCungUng.NotFoundSignInfo);
        }
        return Ok(signInfo);
    }
    
    /// <summary>
    /// Chi tiêt thông tin ký số
    /// </summary>
    /// <param name="phieuId"></param>
    /// <returns></returns>
    [HttpGet("{phieuId:int}")]
    public async Task<IActionResult> GetAsync(int phieuId)
    {
        var result = await _phieuKyService.GetAsync(phieuId);
        if (result is null)
        {
            return NotFound(Constants.Exceptions.Messages.KyCungUng.NotFoundSignInfo);
        }
        return Ok(result);
    }
    
    // [HttpGet("danh-sach-phieu")]
    // [AllowAnonymous]
    // public async Task<ActionResult<IEnumerable<PhieuKyModel>>> DanhSachPhieuKyAsync(
    //     [FromQuery] DanhSachPhieuKyFilter request)
    // {
    //     try
    //     {
    //         var result = await _phieuKyService.GetDanhSachPhieuKyAsync(request);
    //         return Ok(result);
    //     }
    //     catch (Exception ex)
    //     {
    //         // Xử lý lỗi và trả về mã lỗi phù hợp
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }

    [HttpPost("bo-qua-ky")]
    public async Task<IActionResult> BoQuaKyAsync([FromBody] ModifiedKySo requests)
    {
        try
        {
            var result = await _phieuKyService.BoQuaKhongKy(requests);
            if (result is ErrorResponse errorResponse)
            {
                // Xử lý lỗi
                return BadRequest(errorResponse);
            }

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
            await _unitOfWork.BeginTransactionAsync(); // Bắt đầu giao dịch

            // Kiểm tra file upload
            if (request.FileData == null || request.FileData.Length == 0)
            {
                return BadRequest("Không có file nào được tải lên.");
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

            // Cập nhật thông tin ký trong database
            var resultUpdate = await _phieuKyService.UpdateThongTinKyAsync(request);
            if (resultUpdate is ErrorResponse errorResponse)
            {
                await _unitOfWork.RollbackAsync(); // Rollback giao dịch nếu có lỗi
                return BadRequest(errorResponse);
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
                await _unitOfWork.RollbackAsync(); // Rollback nếu có lỗi
                return StatusCode(403, new { message = "Bạn không có quyền ghi vào file này." });
            }
            catch (IOException ioEx)
            {
                await _unitOfWork.RollbackAsync(); // Rollback nếu có lỗi
                return StatusCode(500, new { message = "Lỗi ghi file: " + ioEx.Message });
            }

            await _unitOfWork.CommitAsync(); // Cam kết giao dịch nếu mọi thứ thành công
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
            return StatusCode(500, new { message = "Có lỗi xảy ra, vui lòng thử lại sau." + ex.Message });
        }
    }


    [HttpGet("get-file/{id}")]
    public async Task<ActionResult<byte[]>> GetFileById(int id)
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

            // Đọc file và trả về kết quả dưới dạng byte[]
            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return Ok(fileBytes); // Trả về dữ liệu dạng byte
        }
        catch (Exception ex)
        {
            // Xử lý lỗi, có thể log lại hoặc trả về thông báo lỗi
            return StatusCode(500, new { message = "Đã xảy ra lỗi", error = ex.Message });
        }
    }


    [HttpPost("sign-viettel")]
    public async Task<IActionResult> SignViettelCA([FromBody] SignMobileCaInputDto input, int? phieuId)
    {
        Serilog.Log.Information("Bắt đầu ký số Viettel CA - PhieuId: {PhieuId}, VanBanId: {VanBanId}", input?.ChuKyRequest?.PhieuId, input?.ChuKyRequest?.VanBanId);

        if (input?.ChuKyRequest == null)
        {
            Serilog.Log.Warning("Dữ liệu đầu vào không hợp lệ");
            return BadRequest(new { message = "Dữ liệu đầu vào không hợp lệ." });
        }

        if (input.ChuKyRequest.VanBanId <= 0)
        {
            Serilog.Log.Warning("VanBanId không hợp lệ: {VanBanId}", input.ChuKyRequest.VanBanId);
            return BadRequest(new { message = "VanBanId không được để trống." });
        }

        if (input.PdfPath == null)
        {
            Serilog.Log.Warning("PdfPath trống cho VanBanId: {VanBanId}", input.ChuKyRequest.VanBanId);
            return BadRequest(new { message = "PdfPath trống." });
        }

        try
        {
            // Log thông tin chi tiết về request
            Serilog.Log.Information(
                "Thông tin ký số - NguoiKyId: {NguoiKyId}, MaDoiTuongKy: {MaDoiTuongKy}, ThuTuKy: {ThuTuKy}",
                input.ChuKyRequest.NguoiKyId,
                input.ChuKyRequest.MaDoiTuongKy,
                input.ChuKyRequest.ThuTuKy
            );

            // Kiểm tra file PDF đầu vào
            var fullPath = await _phieuKyService.GetFullFilePath(input.PdfPath);
            if (string.IsNullOrEmpty(fullPath))
            {
                Serilog.Log.Warning("Không tìm thấy file PDF tại đường dẫn: {PdfPath}", input.PdfPath);
                return BadRequest(new { message = "Không có file nào được tải lên." });
            }

            input.PdfPath = fullPath;
            input.PdfPathSigned = fullPath;

            input.DataToDisplayed = "PhieuMS";
            Serilog.Log.Debug("Đường dẫn file PDF đầy đủ: {FullPath}", fullPath);

            string directoryPath = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileName(fullPath);

            input.PdfPathSigned = Path.Combine(directoryPath, "Sign_" + fileName);

            // Kiểm tra file tồn tại trên hệ thống
            var checkFileResult = await CheckFileExistsAsync(input.ChuKyRequest.VanBanId);
            if (checkFileResult is ObjectResult errorResult && errorResult.StatusCode != (int)HttpStatusCode.OK)
            {
                Serilog.Log.Warning("Kiểm tra file thất bại - VanBanId: {VanBanId}, StatusCode: {StatusCode}",
                    input.ChuKyRequest.VanBanId, errorResult.StatusCode);
                return errorResult;
            }

            // Cập nhật path ảnh chữ ký vào request
            // Thực hiện ký số
            Serilog.Log.Information("Bắt đầu quá trình ký số - VanBanId: {VanBanId}", input.ChuKyRequest.VanBanId);

            await _phieuKyService.SignViettelCA(input);

            foreach (var process in Process.GetProcessesByName("Acrobat")) //đóng hết file pdf
            {
                process.Kill();
            }

            //xóa file 
            if (System.IO.File.Exists(fullPath)) //xóa file gốc
            {
                System.IO.File.Delete(fullPath);
            }

            if (System.IO.File.Exists(input.PdfPathSigned)) //đổi tên file về tên file gốc
            {
                System.IO.File.Move(input.PdfPathSigned, input.PdfPath);
            }

            // Cập nhật trạng thái chữ ký
            var updateFileRequest = new UpdateFileRequest
            {
                PhieuId = input.ChuKyRequest.PhieuId,
                VanBanId = input.ChuKyRequest.VanBanId,
                SignUserId = input.ChuKyRequest.NguoiKyId ?? 0,
                MaDoiTuongKy = input.ChuKyRequest.MaDoiTuongKy ?? "Unknown",
                ThuTuKy = input.ChuKyRequest.ThuTuKy ?? 0,
                ChuKyId = input.ChuKyRequest.ChuKyId
            };
            Serilog.Log.Information("Cập nhật trạng thái ký số - PhieuId: {PhieuId}, VanBanId: {VanBanId}",
                updateFileRequest.PhieuId, updateFileRequest.VanBanId);

            await _phieuKyService.UpdateKySimCaAsync(updateFileRequest);

            // Xóa file ảnh chữ ký sau khi đã ký và cập nhật trạng thái
            if (!string.IsNullOrEmpty(input.SignFileInfo?.pathImage))
            {
                try
                {
                    if (string.IsNullOrEmpty(input.SignFileInfo.pathImage) ||
                        !System.IO.File.Exists(input.SignFileInfo.pathImage))
                    {
                        Serilog.Log.Warning("Không tìm thấy file ảnh chữ ký: {ImagePath}",
                            input.SignFileInfo.pathImage);
                        return NotFound(new { message = "File không tồn tại." });
                    }

                    // Thực hiện xóa file
                    System.IO.File.Delete(input.SignFileInfo.pathImage);
                    Serilog.Log.Information("Đã xóa file ảnh chữ ký: {ImagePath}", input.SignFileInfo.pathImage);
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu việc xóa file gặp sự cố
                    Serilog.Log.Error(ex, "Lỗi khi xóa file ảnh chữ ký: {ImagePath}", input.SignFileInfo.pathImage);
                }
            }

            return Ok(new { success = true, message = "Ký thành công." });
        }
        catch (NotFoundException ex)
        {
            Serilog.Log.Error(ex, "NotFoundException khi ký số - PhieuId: {PhieuId}, VanBanId: {VanBanId}",
                phieuId, input?.ChuKyRequest?.VanBanId);
            return NotFound(new { message = ex.Message });
        }
        catch (BadRequestException ex)
        {
            Serilog.Log.Error(ex, "BadRequestException khi ký số - PhieuId: {PhieuId}, VanBanId: {VanBanId}",
                phieuId, input?.ChuKyRequest?.VanBanId);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex,
                "Lỗi không xác định khi ký số - PhieuId: {input?.ChuKyRequest?.PhieuId}, VanBanId: {VanBanId}",
                phieuId, input?.ChuKyRequest?.VanBanId);

            return StatusCode(500, new { message = "Ký số thất bại.", error = ex.Message });
        }
    }


}