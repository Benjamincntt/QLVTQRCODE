﻿using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.Lookup.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using Mapster;
using MapsterMapper;

namespace ESPlatform.QRCode.IMS.Core.Services.Lookup;

public class LookupService : ILookupService
{
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IMapper _mapper;

    public LookupService(
        IVatTuRepository vatTuRepository,
        IMapper mapper)
    {
        _vatTuRepository = vatTuRepository;
        _mapper = mapper;
    }

    public async Task<LookupSuppliesResponse> GetAsync(string maVatTu)
    {
        if (string.IsNullOrWhiteSpace(maVatTu))
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidId);
        }
        var response = new LookupSuppliesResponse();

        // vật tư : mã vt, 
        var vatTu = await _vatTuRepository.GetAsync(x => x.MaVatTu == maVatTu);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), maVatTu);
        }
        
        var vatTuId = vatTu.VatTuId;
        response.MaVatTu = maVatTu;
        response.TenVatTu = !string.IsNullOrWhiteSpace(vatTu.TenVatTu) ? vatTu.TenVatTu : string.Empty;
        response.DonViTinh = !string.IsNullOrWhiteSpace(vatTu.DonViTinh) ? vatTu.DonViTinh : string.Empty;
        // ảnh đại diện
        response.Image = string.IsNullOrWhiteSpace(vatTu.Image) ? string.Empty : vatTu.Image;
        var folderPath = $@"{AppConfig.Instance.Image.FolderPath}\{vatTuId}";
        var urlPath = $"{AppConfig.Instance.Image.UrlPath}/{vatTuId}";
        // list ảnh
        if (Directory.Exists(folderPath))
        {
            var imageFiles = Directory.GetFiles(folderPath);

            // Xây dựng đường dẫn hoàn chỉnh cho mỗi ảnh
            foreach (var file in imageFiles)
            {
                var fileName = Path.GetFileName(file);
                var fullPath = $"{urlPath}/{fileName}";
                response.ImagePaths.Add(fullPath);
            }
        }
        // vị trí
        var positions = (await _vatTuRepository.GetPositionAsync(vatTuId)).Adapt<IEnumerable<SuppliesLocation>>()
            .ToList();

        if (positions.Count > 0)
        {
            response.SuppliesLocation = positions;
        }
        //todo
        //chưa có phần mã kho chính kho phụ
        
        // LOT
        var wareHouse = await _vatTuRepository.GetWareHouseAsync(vatTuId);
        if (wareHouse == null) return response;
        var wareHouseMapper = _mapper.Map<InventoryCheckResponse>(wareHouse);
        response.LotNumber = wareHouseMapper.LotNumber;
        return response;
        
    }
}