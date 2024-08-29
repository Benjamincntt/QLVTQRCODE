using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace ESPlatform.QRCode.IMS.Core.Services.KiemKe;

public class KiemKeService : IKiemKeService
{
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IVatTuImageRepository _vatTuImageRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;
    private readonly IMemoryCache _memoryCache;
    private const string CacheKey = "LocationIds";
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public KiemKeService(
        IVatTuRepository vatTuRepository,
        IVatTuImageRepository vatTuImageRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IWebHostEnvironment env,
        IHttpContextAccessor httpContextAccessor,
        IMemoryCache memoryCache, IMapper mapper)
    {
        _vatTuRepository = vatTuRepository;
        _vatTuImageRepository = vatTuImageRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _env = env;
        _httpContextAccessor = httpContextAccessor;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    public async Task<InventoryCheckResponse> GetAsync(int vatTuId)
    {
        var kyKiemkeId = _authorizedContextFacade.KyKiemKeId;
        var response = new InventoryCheckResponse();

        // lấy path ảnh từ wwwroot
        // var webRootPath = _env.WebRootPath;
        // var folderPath = Path.Combine(webRootPath, "Images", vatTuId.ToString());
        // if (Directory.Exists(folderPath))
        // {
        //     var imagePaths = Directory.GetFiles(folderPath)
        //         .Select(Path.GetFileName)
        //         .Select(fileName =>
        //         {
        //             var httpContext = _httpContextAccessor.HttpContext;
        //             if (httpContext == null)
        //             {
        //                 return string.Empty;
        //             }
        //
        //             var actionContext = new ActionContext(httpContext, httpContext.GetRouteData(),
        //                 new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
        //             var urlHelper = new UrlHelper(actionContext);
        //             return urlHelper.Content($"~/Images/{vatTuId}/{fileName}");
        //         })
        //         .Where(url => !string.IsNullOrEmpty(url)) // Loại bỏ các URL rỗng
        //         .ToList();
        //
        //     response.ImageUrls = imagePaths;
        //}

        // vật tư
        var vatTu = await _vatTuRepository.GetAsync(vatTuId);
        if (vatTu != null)
        {
            response.MaVatTu = !string.IsNullOrWhiteSpace(vatTu.MaVatTu) ? vatTu.MaVatTu : string.Empty;
            response.TenVatTu = !string.IsNullOrWhiteSpace(vatTu.TenVatTu) ? vatTu.TenVatTu : string.Empty;
            response.DonViTinh = !string.IsNullOrWhiteSpace(vatTu.DonViTinh) ? vatTu.DonViTinh : string.Empty;
        }
        
        // danh sách ảnh cua vật tư
        var images = (await _vatTuRepository.ListAsync(vatTuId)).Adapt<IEnumerable<ImagesInfo>>();;
        response.ImagesInfo = images.ToList();

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
        var position = await _vatTuRepository.GetPositionAsync(vatTuId);
        if (position != null)
        {
            var positionMapper = _mapper.Map<InventoryCheckResponse>(position);
            response.IdToMay = positionMapper.IdToMay;
            response.IdGiaKe = positionMapper.IdGiaKe;
            response.IdNgan = positionMapper.IdNgan;
            response.IdHop = positionMapper.IdHop;
            response.ViTri = positionMapper.ViTri;

            //lưu 4 id vào cache key phục vụ cho cập nhật vị trí
            var locationIds = new Dictionary<string, int>
            {
                { "IdToMay", positionMapper.IdToMay },
                { "IdGiaKe", positionMapper.IdGiaKe },
                { "IdNgan", positionMapper.IdNgan },
                { "IdHop", positionMapper.IdHop }
            };
            _memoryCache.Set(CacheKey, locationIds, TimeSpan.FromMinutes(15));
        }

        // LOT
        var wareHouse = await _vatTuRepository.GetWareHouseAsync(vatTuId);
        if (wareHouse == null) return response;
        var wareHouseMapper = _mapper.Map<InventoryCheckResponse>(wareHouse);
        response.LotNumber = wareHouseMapper.LotNumber;
        return response;
    }
}