using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.TraCuu.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;

namespace ESPlatform.QRCode.IMS.Core.Services.TraCuu;

public class TraCuuService : ITraCuuService
{
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IKhoRepository _khoRepository;
    private readonly IVatTuTonKhoRepository _vatTuTonKhoRepository;
    private readonly IMapper _mapper;
    private readonly ImagePath _imagePath;

    public TraCuuService(
        IVatTuRepository vatTuRepository,
        IKhoRepository khoRepository,
        IVatTuTonKhoRepository vatTuTonKhoRepository,
        IMapper mapper,
        IOptions<ImagePath> imagePath)
    {
        _vatTuRepository = vatTuRepository;
        _khoRepository = khoRepository;
        _vatTuTonKhoRepository = vatTuTonKhoRepository;
        _mapper = mapper;
        _imagePath = imagePath.Value;
    }

    public async Task<LookupSuppliesResponse> GetAsync(int khoId, string maVatTu)
    {
        if (string.IsNullOrWhiteSpace(maVatTu))
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.EmptySupplyCode);
        }

        var response = new LookupSuppliesResponse();

        // Vật tư
        var vatTu = await _vatTuRepository.GetAsync(x => x.KhoId == khoId && x.MaVatTu == maVatTu);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), maVatTu);
        }

        var vatTuId = vatTu.VatTuId;
        response.VatTuId = vatTuId;
        response.MaVatTu = maVatTu;
        response.TenVatTu = !string.IsNullOrWhiteSpace(vatTu.TenVatTu) ? vatTu.TenVatTu : string.Empty;
        response.DonViTinh = !string.IsNullOrWhiteSpace(vatTu.DonViTinh) ? vatTu.DonViTinh : string.Empty;
        var rootPath = _imagePath.RootPath;
        var relativeBasePath = _imagePath.RelativeBasePath;
        var localBasePath = (rootPath + relativeBasePath).Replace("/", "\\");
        var folderImagePath = $@"{localBasePath}\{vatTuId}";
        // Danh sách ảnh
        if (Directory.Exists(folderImagePath))
        {
            var imageFiles = Directory.GetFiles(folderImagePath);

            // Xây dựng đường dẫn hoàn chỉnh cho mỗi ảnh
            foreach (var file in imageFiles)
            {
                var fileName = Path.GetFileName(file);
                var fullPath = Path.Combine(relativeBasePath, vatTuId.ToString(), fileName).Replace("\\", "/");
                response.ImagePaths.Add(fullPath);
            }
        }
        // Ảnh đại diện
        if (response.ImagePaths.Any())
        {
            response.Image = response.ImagePaths.First();
        }

        // Vị trí kho chính và phụ,
        var warehouse = await _khoRepository.GetAsync(x => x.OrganizationId == khoId);
        response.OrganizationCode = warehouse == null ? string.Empty : warehouse.OrganizationCode ?? string.Empty;

        // LOT, Số lượng tồn
        var inventory = await _vatTuTonKhoRepository.GetAsync(x => x.VatTuId == vatTuId && x.KhoId == khoId);
        if (inventory == null) return response;
        response.LotNumber = inventory.LotNumber ?? string.Empty;
        response.OnhandQuantity = inventory.OnhandQuantity ?? 0;
        response.SubInventoryCode = inventory.SubinventoryCode ?? string.Empty;

        // Vị trí chi tiết trong kho
        var positions = (await _vatTuRepository.GetPositionAsync(vatTuId)).Adapt<IEnumerable<SuppliesLocation>>()
            .ToList();
        if (positions.Count > 0)
        {
            response.SuppliesLocation = positions;
        }
        return response;
    }
}