using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.ViTris.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace ESPlatform.QRCode.IMS.Core.Services.Common;

public class CommonService : ICommonService
{
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IVatTuViTriRepository _vatTuViTriRepository;
    private readonly IViTriRepository _viTriRepository;

    public CommonService(IVatTuRepository vatTuRepository, IVatTuViTriRepository vatTuViTriRepository,
        IViTriRepository viTriRepository)
    {
        _vatTuRepository = vatTuRepository;
        _vatTuViTriRepository = vatTuViTriRepository;
        _viTriRepository = viTriRepository;
    }

    public async Task<int> ModifySuppliesLocationAsync(int vatTuId, int idViTri,
        ModifiedSuppliesLocationRequest request)
    {
        #region validate

        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply,
                new List<string> { nameof(vatTuId) + " is invalid" });
        }

        if (idViTri < 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupplyLocation,
                new List<string> { nameof(idViTri) + " is invalid" });
        }

        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), null);
        }

        await ValidationHelper.ValidateAsync(request, new ModifiedSuppliesLocationRequestValidation());

        if (request.IdToMay == null && request.IdGiaKe == null && request.IdNgan == null && request.IdHop == null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.NoSupplyLocationSelected);
        }

        #endregion

        var vitri = await _vatTuViTriRepository.GetAsync(x => x.IdVatTu == vatTuId && x.IdViTri == idViTri);

        if (idViTri == 0 || vitri == null)
        {
            var viTriVatTuNew = new QlvtVatTuViTri();
            viTriVatTuNew.IdToMay = request.IdToMay;
            viTriVatTuNew.IdGiaKe = request.IdGiaKe;
            viTriVatTuNew.IdNgan = request.IdNgan;
            viTriVatTuNew.IdHop = request.IdHop;
            var chuoiToAdd = new[]
            {
                string.IsNullOrEmpty(request.TenToMay) ? null : $"{request.TenToMay}",
                string.IsNullOrEmpty(request.TenGiaKe) ? null : $"{request.TenGiaKe}",
                string.IsNullOrEmpty(request.TenNgan) ? null : $"{request.TenNgan}",
                string.IsNullOrEmpty(request.TenHop) ? null : $"{request.TenHop}"
            };
            viTriVatTuNew.IdVatTu = vatTuId;
            viTriVatTuNew.TenVatTu = vatTu.TenVatTu ?? string.Empty;
            viTriVatTuNew.IdKhoErp = vatTu.KhoId;
            viTriVatTuNew.MaVatTu = vatTu.MaVatTu ?? string.Empty;
            viTriVatTuNew.ViTri = string.Join(" -> ", chuoiToAdd.Where(c => !string.IsNullOrEmpty(c)));
            return await _vatTuViTriRepository.UpdateAsync(viTriVatTuNew);
        }

        vitri.IdToMay = request.IdToMay;
        vitri.IdGiaKe = request.IdGiaKe;
        vitri.IdNgan = request.IdNgan;
        vitri.IdHop = request.IdHop;
        var chuoiToUpdate = new[]
        {
            string.IsNullOrEmpty(request.TenToMay) ? null : $"{request.TenToMay}",
            string.IsNullOrEmpty(request.TenGiaKe) ? null : $"{request.TenGiaKe}",
            string.IsNullOrEmpty(request.TenNgan) ? null : $"{request.TenNgan}",
            string.IsNullOrEmpty(request.TenHop) ? null : $"{request.TenHop}"
        };
        vitri.ViTri = string.Join(" -> ", chuoiToUpdate.Where(c => !string.IsNullOrEmpty(c)));
        return await _vatTuViTriRepository.UpdateAsync(vitri);
    }

    public async Task<string> ModifySuppliesImageAsync(int vatTuId, string inputPath, IFormFile file)
    {
        #region validate

        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }
        if(file.Length <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.NoSupplyImageSelected);
        }

        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), null);
        }

        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; // Các loại file được phép
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidFileType);
        }

        #endregion

        var folderPath = AppConfig.Instance.Image.FolderPath; //   "D:"
        var urlPath = AppConfig.Instance.Image.UrlPath; //         "/Images"
        var localBasePath = (folderPath + urlPath).Replace("/", "\\");
        var localPath = (folderPath + inputPath).Replace("/", "\\");
        // delete old image
        if (File.Exists(localPath))
        {
            File.Delete(localPath);
        }
        // create new image
        var fileName = $"{Path.GetFileName(file.FileName)}";
        
        // case not exist folder
        var localFolder = Path.Combine(localBasePath, vatTuId.ToString());
        if (!Directory.Exists(localFolder))
        {
            Directory.CreateDirectory(localFolder);
            vatTu.Image = Path.Combine(urlPath, vatTuId.ToString(), fileName).Replace("\\", "/");
            await _vatTuRepository.UpdateAsync(vatTu);
        }
        var fullPath = Path.Combine(localBasePath, vatTuId.ToString(), fileName);
        
        // save file
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var urlResult = vatTu.Image = Path.Combine(AppConfig.Instance.Image.UrlPath, vatTuId.ToString(), fileName)
            .Replace("\\", "/");
        return urlResult;
    }

    public async Task<string> CreateSuppliesImageAsync(int vatTuId, IFormFile file)
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
        var folderPath = AppConfig.Instance.Image.FolderPath;
        var urlPath = AppConfig.Instance.Image.UrlPath;
        var localBasePath = (folderPath + urlPath).Replace("/", "\\");
        var fileName = $"{Path.GetFileName(file.FileName)}";

        // check if server don't have path => create a new directory by path
        var pathUpload = Path.Combine(localBasePath, vatTuId.ToString());
        if (!Directory.Exists(pathUpload))
        {
            Directory.CreateDirectory(pathUpload);
            vatTu.Image = Path.Combine(urlPath, vatTuId.ToString(), fileName).Replace("\\", "/");
            await _vatTuRepository.UpdateAsync(vatTu);
        }

        var fullPath = Path.Combine(pathUpload, fileName);

        // save file
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var urlResult = Path.Combine(urlPath, vatTuId.ToString(), fileName).Replace("\\", "/");
        return urlResult;
    }

    public async Task<int> DeleteSuppliesImageAsync(int vatTuId, string inputPath)
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

        var folderPath = AppConfig.Instance.Image.FolderPath; //   "D:"
        // delete old image
        var localPath = (folderPath + inputPath).Replace("/", "\\");
        if (!File.Exists(localPath)) return default;
        File.Delete(localPath);
        return 1;
    }

    public async Task<IEnumerable<SupplyLocationListResponseItem>> ListSuppliesLocationAsync(int parentId)
    {
        if (parentId is < 0 or > 4)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidParameters);
        }

        var response = (await _viTriRepository.ListSuppliesLocationAsync(parentId))
            .Adapt<IEnumerable<SupplyLocationListResponseItem>>();
        return response;
    }
}