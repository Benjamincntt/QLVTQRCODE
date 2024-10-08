using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;
using ESPlatform.QRCode.IMS.Core.Services.PhieuKy;
using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Microsoft.AspNetCore.Mvc;


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


        [HttpPost("ky-phieu")]
        public async Task<IActionResult> KyPhieuAsync([FromBody] KyPhieuRequest requests)
        {
            try
            {
                var result = await _phieuKyService.CapNhatThongTinKyAsync(requests);
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
    }
}
