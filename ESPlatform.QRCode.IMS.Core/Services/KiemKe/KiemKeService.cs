using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Mapster;
using MapsterMapper;

namespace ESPlatform.QRCode.IMS.Core.Services.KiemKe;

public class KiemKeService : IKiemKeService
{
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IVatTuViTriRepository _vatTuViTriRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;
    private readonly IMapper _mapper;
    // private readonly IMemoryCache _memoryCache;
    // private const string CacheKey = "LocationIds";

    public KiemKeService(
        IVatTuRepository vatTuRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        //IMemoryCache memoryCache,
        IMapper mapper,
        IVatTuViTriRepository vatTuViTriRepository)
    {
        _vatTuRepository = vatTuRepository;
        _authorizedContextFacade = authorizedContextFacade;
        //_memoryCache = memoryCache;
        _mapper = mapper;
        _vatTuViTriRepository = vatTuViTriRepository;
    }

    public async Task<InventoryCheckResponse> GetAsync(int vatTuId)
    {
        if (vatTuId <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidId);
        }

        var kyKiemkeId = _authorizedContextFacade.KyKiemKeId;
        var response = new InventoryCheckResponse();

        // vật tư 
        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), vatTuId.ToString());
        }

        response.MaVatTu = !string.IsNullOrWhiteSpace(vatTu.MaVatTu) ? vatTu.MaVatTu : string.Empty;
        response.TenVatTu = !string.IsNullOrWhiteSpace(vatTu.TenVatTu) ? vatTu.TenVatTu : string.Empty;
        response.DonViTinh = !string.IsNullOrWhiteSpace(vatTu.DonViTinh) ? vatTu.DonViTinh : string.Empty;
        // ảnh đại diện
        response.Image = string.IsNullOrWhiteSpace(vatTu.Image) ? string.Empty : vatTu.Image;
        var folderPath = $@"{AppConfig.Instance.Image.FolderPath}\{vatTuId}";
        var urlPath = $"{AppConfig.Instance.Image.UrlPath}/{vatTuId}";

        if (Directory.Exists(folderPath))
        {
            var imageFiles = Directory.GetFiles(folderPath);

            // Xây dựng đường dẫn hoàn chỉnh cho mỗi ảnh
            foreach (var file in imageFiles)
            {
                // Lấy tên file (không bao gồm đường dẫn)
                var fileName = Path.GetFileName(file);

                // Tạo URL hoàn chỉnh từ urlPath và tên file
                var fullPath = $"{urlPath}/{fileName}";

                // Thêm vào danh sách đường dẫn
                response.ImagePaths.Add(fullPath);
            }
        }


        // kỳ kiểm kê và thông tin kho phụ
        var inventoryCheckInformation = await _vatTuRepository.GetInventoryCheckInformationAsync(vatTuId, kyKiemkeId);
        if (inventoryCheckInformation != null)
        {
            var inventoryCheckInformationMapper =
                _mapper.Map<InventoryCheckResponse>(inventoryCheckInformation);
            response.PhysicalInventoryName = inventoryCheckInformationMapper.PhysicalInventoryName;
            response.OrganizationCode = inventoryCheckInformationMapper.OrganizationCode;
            response.SubInventoryCode = inventoryCheckInformationMapper.SubInventoryCode;
            response.SubInventoryName = inventoryCheckInformationMapper.SubInventoryName;
            response.SoLuongSoSach = inventoryCheckInformationMapper.SoLuongSoSach;
            response.SoLuongKiemKe = inventoryCheckInformationMapper.SoLuongKiemKe;
            response.SoLuongChenhLech = inventoryCheckInformationMapper.SoLuongChenhLech;
        }

        // vị trí
        var positions = (await _vatTuRepository.GetPositionAsync(vatTuId)).Adapt<IEnumerable<SuppliesLocation>>()
            .ToList();

        if (positions.Count > 0)
        {
            response.SuppliesLocation = positions;
        }

        //lưu 4 id vào cache key phục vụ cho cập nhật vị trí
        // var locationIds = new Dictionary<string, int>
        // {
        //     { "IdToMay", positionMapper.IdToMay },
        //     { "IdGiaKe", positionMapper.IdGiaKe },
        //     { "IdNgan", positionMapper.IdNgan },
        //     { "IdHop", positionMapper.IdHop }
        // };
        // _memoryCache.Set(CacheKey, locationIds, TimeSpan.FromMinutes(15));
        //}

        // LOT
        var wareHouse = await _vatTuRepository.GetWareHouseAsync(vatTuId);
        if (wareHouse == null) return response;
        var wareHouseMapper = _mapper.Map<InventoryCheckResponse>(wareHouse);
        response.LotNumber = wareHouseMapper.LotNumber;
        return response;
    }

    public async Task<int> ModifySuppliesLocationAsync(int vatTuId, int idViTri, ModifiedSuppliesLocationRequest request)
    {
        #region validate
        
        if (vatTuId < 0 || idViTri < 0)
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
}