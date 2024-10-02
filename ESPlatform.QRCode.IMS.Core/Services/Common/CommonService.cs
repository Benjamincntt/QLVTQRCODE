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
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ESPlatform.QRCode.IMS.Core.Services.Common;

public class CommonService : ICommonService
{
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IVatTuViTriRepository _vatTuViTriRepository;
    private readonly IViTriRepository _viTriRepository;
    private readonly ImagePath _imagePath;

    public CommonService(
        IVatTuRepository vatTuRepository,
        IVatTuViTriRepository vatTuViTriRepository,
        IViTriRepository viTriRepository,
        IOptions<ImagePath> imagePath)
    {
        _vatTuRepository = vatTuRepository;
        _vatTuViTriRepository = vatTuViTriRepository;
        _viTriRepository = viTriRepository;
        _imagePath = imagePath.Value;
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

        // lấy vị trí hiện tai
        var vitri = await _vatTuViTriRepository.GetAsync(x => x.IdVatTu == vatTuId && x.IdViTri == idViTri);
        // nếu chưa có vị trí => tạo vị trí mới 
        // trong case này nếu vị trí không được tìm thấy và có IdViTri => vẫn phải thêm mới vì trong db, bảng QLVT_VatTu_ViTri có IdViTri là key và là duy nhất
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

        // nếu có vị trí => cập nhật lại vị trí
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

        if (file.Length <= 0)
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

        var rootPath = _imagePath.RootPath; 
        var relativeBasePath = _imagePath.RelativeBasePath; 
        var localBasePath = (rootPath + relativeBasePath).Replace("/", "\\"); 
        var inputLocalPath = (rootPath + inputPath).Replace("/", "\\"); 
        // Xóa ảnh cũ
        if (File.Exists(inputLocalPath))
        {
            File.Delete(inputLocalPath);
        }

        // Tạo ảnh mới
        var fileName = file.FileName.ToFileName();

        // nếu thư mục chứa ảnh chưa tồn tại => tạo thư mục theo Id => lưu ảnh
        var localFolder = Path.Combine(localBasePath, vatTuId.ToString());
        if (!Directory.Exists(localFolder))
        {
            Directory.CreateDirectory(localFolder);
        }

        var fullPath = Path.Combine(localBasePath, vatTuId.ToString(), fileName); 
        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Lấy ảnh đầu tiên trong thư mục làm ảnh đại diện
        var imageFiles = Directory.GetFiles(localFolder)
            .Where(x => allowedExtensions.Contains(Path.GetExtension(x).ToLower()))
            .ToList();
        if (imageFiles.Count > 0)
        {
            var firstImageFile = imageFiles.First();
            vatTu.Image = Path.Combine("/", vatTuId.ToString(), Path.GetFileName(firstImageFile)).Replace("\\", "/"); 
            await _vatTuRepository.UpdateAsync(vatTu);
        }

        return Path.Combine(_imagePath.RelativeBasePath, vatTuId.ToString(), fileName)
            .Replace("\\", "/");                                                                                       
    }

    public async Task<string> CreateSuppliesImageAsync(int vatTuId, IFormFile file)
    {
        #region validate

        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }

        if (file.Length <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.NoSupplyImageSelected);
        }

        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), null);
        }

        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidFileType);
        }

        #endregion

        // create new image
        var rootPath = _imagePath.RootPath;
        var relativeBasePath = _imagePath.RelativeBasePath;
        var localBasePath = (rootPath + relativeBasePath).Replace("/", "\\");
        var fileName = file.FileName.ToFileName();

        // check if server don't have path => create a new directory by path
        var pathUpload = Path.Combine(localBasePath, vatTuId.ToString());
        if (!Directory.Exists(pathUpload))
        {
            Directory.CreateDirectory(pathUpload);
        }

        var fullPath = Path.Combine(pathUpload, fileName);

        // save file
        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        // Lấy ảnh đầu tiên trong thư mục làm ảnh đại diện
        var imageFiles = Directory.GetFiles(pathUpload)
            .Where(x => allowedExtensions.Contains(Path.GetExtension(x).ToLower()))
            .ToList();
        if (imageFiles.Count > 0)
        {
            var firstImageFile = imageFiles.First();
            vatTu.Image = Path.Combine("/", vatTuId.ToString(), Path.GetFileName(firstImageFile)).Replace("\\", "/"); 
            await _vatTuRepository.UpdateAsync(vatTu);
        }

        var urlResult = Path.Combine(relativeBasePath, vatTuId.ToString(), fileName).Replace("\\", "/");
        return urlResult;
    }

    public async Task<int> DeleteSuppliesImageAsync(int vatTuId, string inputPath)
    {
        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }

        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), null);
        }

        var rootPath = _imagePath.RootPath; 
        var relativeBasePath = _imagePath.RelativeBasePath;
        var localBasePath = (rootPath + relativeBasePath).Replace("/", "\\"); 
        var localPath = (rootPath + inputPath).Replace("/", "\\");
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; 
        // nếu không tồn tại ảnh => xóa không thành công 
        if (!File.Exists(localPath))
        {
            return default;
        }
        File.Delete(localPath);
        // Lấy ảnh đầu tiên trong thư mục làm ảnh đại diện
        var localFolder = Path.Combine(localBasePath, vatTuId.ToString()); 
        var imageFiles = Directory.GetFiles(localFolder)
            .Where(x => allowedExtensions.Contains(Path.GetExtension(x).ToLower()))
            .ToList();
        if (imageFiles.Count <= 0)
        {
            vatTu.Image = string.Empty;
            await _vatTuRepository.UpdateAsync(vatTu);
            return 1;
        }
        var firstImageFile = imageFiles.First();
        vatTu.Image = Path.Combine("/", vatTuId.ToString(), Path.GetFileName(firstImageFile)).Replace("\\", "/"); 
        await _vatTuRepository.UpdateAsync(vatTu);
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