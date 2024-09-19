using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Entities;
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
    private readonly IKyKiemKeChiTietDffRepository _kyKiemKeChiTietDffRepository;
    private readonly IKyKiemKeChiTietRepository _kyKiemKeChiTietRepository;
    private readonly IKyKiemKeRepository _kyKiemKeRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;
    private readonly IKhoRepository _khoRepository;
    private readonly IMapper _mapper;

    public KiemKeService(
        IVatTuRepository vatTuRepository,
        IKyKiemKeChiTietDffRepository kyKiemKeChiTietDffRepository,
        IKhoRepository khoRepository,
        IKyKiemKeChiTietRepository kyKiemKeChiTietRepository,
        IKyKiemKeRepository kyKiemKeRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IMapper mapper)
    {
        _vatTuRepository = vatTuRepository;
        _kyKiemKeChiTietDffRepository = kyKiemKeChiTietDffRepository;
        _khoRepository = khoRepository;
        _kyKiemKeChiTietRepository = kyKiemKeChiTietRepository;
        _kyKiemKeRepository = kyKiemKeRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _mapper = mapper;
    }

    public async Task<InventoryCheckResponse> GetAsync(string maVatTu)
    {
        if (string.IsNullOrWhiteSpace(maVatTu))
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupplyCode);
        }

        var response = new InventoryCheckResponse();

        // KyKiemKeId
        var kiKiemKeChinh = await _kyKiemKeRepository.GetAsync(x => x.KyKiemKeChinh == 1);
        if (kiKiemKeChinh != null) response.KyKiemKeId = kiKiemKeChinh.Id;
        // vật tư 
        var vatTu = await _vatTuRepository.GetAsync(x => x.MaVatTu == maVatTu);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), maVatTu);
        }

        var vatTuId = vatTu.VatTuId;
        response.VatTuId = vatTuId;
        response.MaVatTu = maVatTu;
        response.TenVatTu = !string.IsNullOrWhiteSpace(vatTu.TenVatTu) ? vatTu.TenVatTu : string.Empty;
        response.DonViTinh = !string.IsNullOrWhiteSpace(vatTu.DonViTinh) ? vatTu.DonViTinh : string.Empty;
        // ảnh đại diện
        response.Image = string.IsNullOrWhiteSpace(vatTu.Image) ? string.Empty : vatTu.Image;
        var folderPath = AppConfig.Instance.Image.FolderPath;//   "D:"
        var urlPath = AppConfig.Instance.Image.UrlPath;//         "/Images"
        var localBasePath =  (folderPath + urlPath).Replace("/", "\\");
        var folderImagePath = $@"{localBasePath}\{vatTuId}";
        //var urlPath = $"{AppConfig.Instance.Image.UrlPath}/{vatTuId}";

        if (Directory.Exists(folderImagePath))
        {
            var imageFiles = Directory.GetFiles(folderImagePath);

            // Xây dựng đường dẫn hoàn chỉnh cho mỗi ảnh
            foreach (var file in imageFiles)
            {
                // Lấy tên file (không bao gồm đường dẫn)
                var fileName = Path.GetFileName(file);
                
                // Tạo URL hoàn chỉnh từ urlPath và tên file
                var fullPath = Path.Combine(urlPath, vatTuId.ToString(), fileName).Replace("\\", "/");

                // Thêm vào danh sách đường dẫn
                response.ImagePaths.Add(fullPath);
            }
        }
        
        // kỳ kiểm kê
        var inventoryCheckInformation =
            await _vatTuRepository.GetInventoryCheckInformationAsync(vatTuId, response.KyKiemKeId);
        if (inventoryCheckInformation != null)
        {
            var inventoryCheckInformationMapper =
                _mapper.Map<InventoryCheckResponse>(inventoryCheckInformation);
            response.KyKiemKeChiTietId = inventoryCheckInformationMapper.KyKiemKeChiTietId;
            response.PhysicalInventoryName = inventoryCheckInformationMapper.PhysicalInventoryName;
            response.SoLuongSoSach = inventoryCheckInformationMapper.SoLuongSoSach;
            response.SoLuongKiemKe = inventoryCheckInformationMapper.SoLuongKiemKe;
            response.SoLuongChenhLech = inventoryCheckInformationMapper.SoLuongChenhLech;
        }

        // DFF
        response.SupplyDff =
            (await _kyKiemKeChiTietDffRepository.GetAsync(x =>
                x.VatTuId == vatTu.VatTuId && x.KyKiemKeChiTietId == response.KyKiemKeChiTietId))
            .Adapt<SupplyDffResponse>();

        // vị trí kho chính và phụ
        var warehouse = await _khoRepository.GetAsync(x => x.OrganizationId == vatTu.KhoId);
        if (warehouse != null)
        {
            if (warehouse.OrganizationCode != null) response.OrganizationCode = warehouse.OrganizationCode;
            if (warehouse.SubInventoryCode != null) response.SubInventoryCode = warehouse.SubInventoryCode;
            if (warehouse.SubInventoryName != null) response.SubInventoryName = warehouse.SubInventoryName;
        }

        // vị trí chi tiết trong kho
        var positions = (await _vatTuRepository.GetPositionAsync(vatTuId)).Adapt<IEnumerable<SuppliesLocation>>()
            .ToList();

        if (positions.Count > 0)
        {
            response.SuppliesLocation = positions;
        }

        // LOT
        var inventory = await _vatTuRepository.GetInventoryAsync(vatTuId, vatTu.KhoId);
        if (inventory == null) return response;
        var inventoryMapper = _mapper.Map<InventoryCheckResponse>(inventory);
        response.LotNumber = inventoryMapper.LotNumber;
        return response;
    }


    public async Task<int> ModifySuppliesDffAsync(int vatTuId, int kyKiemKeId, int kyKiemKeChiTietId, int soLuongKiemKe,
        ModifiedSuppliesDffRequest request)
    {
        #region Validate

        await ValidationHelper.ValidateAsync(request, new ModifiedSuppliesDffRequestValidation());
        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }

        if (kyKiemKeId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.InventoryCheck.InvalidKyKiemKe);
        }

        if (kyKiemKeChiTietId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.InventoryCheck.InvalidKyKiemKeChiTiet);
        }

        if (soLuongKiemKe < 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.InventoryCheck.InvalidSoLuongKiemKe);
        }

        if (soLuongKiemKe < request.SoLuongMatPhamChat + request.SoLuongKemPhamChat)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.InventoryCheck.InvalidToTalMatVaKemPhamChat);
        }

        if (soLuongKiemKe < request.SoLuongDong)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.InventoryCheck.InvalidSoLuongUDong);
        }

        if (soLuongKiemKe < request.SoLuongDeNghiThanhLy)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.InventoryCheck.InvalidSoLuongDeNghiThanhLy);
        }

        #endregion

        var currentSuppliesDff =
            await _kyKiemKeChiTietDffRepository.GetAsync(x =>
                x.VatTuId == vatTuId && x.KyKiemKeChiTietId == kyKiemKeChiTietId);
        // if has no DFF => create
        if (currentSuppliesDff == null)
        {
            var dffToCreate = new QlvtKyKiemKeChiTietDff();

            dffToCreate.VatTuId = vatTuId;
            dffToCreate.KyKiemKeChiTietId = kyKiemKeChiTietId;
            if (soLuongKiemKe > 0)
            {
                dffToCreate.PhanTramMatPhamChat = request.SoLuongMatPhamChat / soLuongKiemKe * 100;
                dffToCreate.PhanTramKemPhamChat = request.SoLuongKemPhamChat / soLuongKiemKe * 100;
                dffToCreate.PhanTramDong = request.SoLuongDong / soLuongKiemKe * 100;
                dffToCreate.TsKemPcMatPc = request.SoLuongMatPhamChat + request.SoLuongKemPhamChat;
            }

            var responseToCreate = _mapper.Map(request, dffToCreate);
            return await _kyKiemKeChiTietDffRepository.InsertAsync(responseToCreate);
        }

        // has DFF => update
        var dffToUpdate = _mapper.Map(request, currentSuppliesDff);
        if (soLuongKiemKe > 0)
        {
            dffToUpdate.PhanTramMatPhamChat = request.SoLuongMatPhamChat / soLuongKiemKe * 100;
            dffToUpdate.PhanTramKemPhamChat = request.SoLuongKemPhamChat / soLuongKiemKe * 100;
            dffToUpdate.PhanTramDong = request.SoLuongDong / soLuongKiemKe * 100;
            dffToUpdate.TsKemPcMatPc = request.SoLuongMatPhamChat + request.SoLuongKemPhamChat;
        }

        return await _kyKiemKeChiTietDffRepository.UpdateAsync(dffToUpdate);
    }

    public async Task<int> ModifySuppliesQtyAsync(int vatTuId, int kyKiemKeId, int soLuongKiemKe)
    {
        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }

        if (kyKiemKeId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.InventoryCheck.InvalidKyKiemKe);
        }

        if (soLuongKiemKe < 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.InventoryCheck.InvalidSoLuongKiemKe);
        }

        //var kyKiemkeId = _authorizedContextFacade.KyKiemKeId;
        var kyKiemKeChiTiet =
            await _kyKiemKeChiTietRepository.GetAsync(x => x.KyKiemKeId == kyKiemKeId && x.VatTuId == vatTuId);
        // has no QTY => create new 
        if (kyKiemKeChiTiet == null)
        {
            var qtyToCreate = new QlvtKyKiemKeChiTiet();
            qtyToCreate.VatTuId = vatTuId;
            qtyToCreate.KyKiemKeId = kyKiemKeId;
            qtyToCreate.SoLuongKiemKe = soLuongKiemKe;
            qtyToCreate.SoLuongChenhLech = soLuongKiemKe;
            qtyToCreate.SoLuongSoSach = 0;
            qtyToCreate.NgayKiemKe = DateTime.Now;
            return await _kyKiemKeChiTietRepository.InsertAsync(qtyToCreate);
        }

        // has QTY => update
        kyKiemKeChiTiet.SoLuongKiemKe = soLuongKiemKe;
        kyKiemKeChiTiet.SoLuongChenhLech = soLuongKiemKe - kyKiemKeChiTiet.SoLuongSoSach;
        kyKiemKeChiTiet.NgayKiemKe = DateTime.Now;
        return await _kyKiemKeChiTietRepository.UpdateAsync(kyKiemKeChiTiet);
    }

    public async Task<IEnumerable<InventoryCheckListResponseItem>> ListAsync()
    {
        var response = (await _kyKiemKeRepository.ListInputAsync()).Adapt<List<InventoryCheckListResponseItem>>();
        return response;
    }

    public async Task<QlvtKyKiemKe> GetCurrentInventoryCheckAsync()
    {
        var currentInventoryCheck = await _kyKiemKeRepository.GetAsync(x => x.KyKiemKeChinh == 1);
        if (currentInventoryCheck == null)
        {
            throw new NotFoundException(Constants.Exceptions.Messages.InventoryCheck.NotFoundKyKiemKeChinh);
        }

        return currentInventoryCheck;
    }
}