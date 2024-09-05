using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Microsoft.AspNetCore.Http;

namespace ESPlatform.QRCode.IMS.Core.Services.Common;

public class CommonService : ICommonService
{
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IVatTuViTriRepository _vatTuViTriRepository;

    public CommonService(IVatTuRepository vatTuRepository, IVatTuViTriRepository vatTuViTriRepository)
    {
        _vatTuRepository = vatTuRepository;
        _vatTuViTriRepository = vatTuViTriRepository;
    }

    public async Task<int> ModifySuppliesLocationAsync(int vatTuId, int idViTri, ModifiedSuppliesLocationRequest request)
    {
        #region validate
        
        if (vatTuId < 1 || idViTri < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidParameters);
        }

        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), vatTuId.ToString());
        }

        await ValidationHelper.ValidateAsync(request, new ModifiedSuppliesLocationRequestValidation());
        
        #endregion
        // check existed location in db
        var vitri = await _vatTuViTriRepository.GetAsync(x => x.IdVatTu == vatTuId && x.IdViTri == idViTri);
        if (vitri == null)
        {
            throw new NotFoundException(vitri.GetTypeEx(), idViTri.ToString()); 
        }
        vitri.IdToMay = request.IdToMay;
        vitri.IdGiaKe = request.IdGiaKe;
        vitri.IdNgan = request.IdNgan;
        vitri.IdHop = request.IdHop;
        var chuoiKetHop = new[] 
        { 
            string.IsNullOrEmpty(request.TenToMay) ? null : $"{request.TenToMay}",
            string.IsNullOrEmpty(request.TenGiaKe) ? null : $"{request.TenGiaKe}",
            string.IsNullOrEmpty(request.TenNgan) ? null : $"{request.TenNgan}",
            string.IsNullOrEmpty(request.TenHop) ? null : $"{request.TenHop}"
        };
        
        vitri.ViTri = string.Join(" -> ", chuoiKetHop.Where(c => !string.IsNullOrEmpty(c)));
        return await _vatTuViTriRepository.UpdateAsync(vitri);
    }

    public async Task<int> ModifySuppliesImageAsync(int vatTuId, string currentImagePath, IFormFile file)
    {
        #region validate
        if (vatTuId < 1 || file == null || file.Length <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidParameters);
        }
        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), vatTuId.ToString());
        }
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; // Các loại file được phép
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidFileType);
        }
        #endregion 
        // delete old image
        if (File.Exists(currentImagePath))
        {
            File.Delete(currentImagePath);
        }
        
        // create new image
        var pathUpload =  Path.Combine(AppConfig.Instance.Image.FolderPath,vatTuId.ToString());
        var fileName = $"{Path.GetFileName(file.FileName)}";
        var fullPath = Path.Combine(pathUpload, fileName);

        // save file
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return 1;
    }

    public async Task<int> CreateSuppliesImageAsync(int vatTuId, IFormFile file)
    {
        #region validate
        if (vatTuId < 1 || file == null || file.Length <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidParameters);
        }
        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), vatTuId.ToString());
        }
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; 
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidFileType);
        }
        #endregion 
        // create new image
        var pathUpload =  Path.Combine(AppConfig.Instance.Image.FolderPath,vatTuId.ToString());
        var fileName = $"{Path.GetFileName(file.FileName)}";
        var fullPath = Path.Combine(pathUpload, fileName);

        // save file
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return 1;
    }

    public async Task<int> DeleteSuppliesImageAsync(int vatTuId, string currentImagePath)
    {
        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidParameters);
        }
        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), vatTuId.ToString());
        }
        // delete old image
        if (!File.Exists(currentImagePath)) return default;
        File.Delete(currentImagePath);
        return 1;
    }

}