using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Core.Services.GioHang;
using ESPlatform.QRCode.IMS.Core.Services.TbNguoiDungs;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;

namespace ESPlatform.QRCode.IMS.Core.Services.KiemKe;

public class KiemKeService : IKiemKeService
{
    private readonly INguoiDungService _nguoiDungService;
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IKyKiemKeChiTietDffRepository _kyKiemKeChiTietDffRepository;
    private readonly IKyKiemKeChiTietRepository _kyKiemKeChiTietRepository;
    private readonly IKyKiemKeRepository _kyKiemKeRepository;
    private readonly IVatTuTonKhoRepository _vatTuTonKhoRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;
    private readonly IKhoRepository _khoRepository;
    private readonly IMapper _mapper;
    private readonly ImagePath _imagePath;

    public KiemKeService(
        INguoiDungService nguoiDungService,
        IVatTuRepository vatTuRepository,
        IKyKiemKeChiTietDffRepository kyKiemKeChiTietDffRepository,
        IKhoRepository khoRepository,
        IKyKiemKeChiTietRepository kyKiemKeChiTietRepository,
        IKyKiemKeRepository kyKiemKeRepository,
        IVatTuTonKhoRepository vatTuTonKhoRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IMapper mapper,
        IOptions<ImagePath> imagePath)
    {
        _nguoiDungService = nguoiDungService;
        _vatTuRepository = vatTuRepository;
        _kyKiemKeChiTietDffRepository = kyKiemKeChiTietDffRepository;
        _khoRepository = khoRepository;
        _kyKiemKeChiTietRepository = kyKiemKeChiTietRepository;
        _kyKiemKeRepository = kyKiemKeRepository;
        _vatTuTonKhoRepository = vatTuTonKhoRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _mapper = mapper;
        _imagePath = imagePath.Value;
    }

    public async Task<InventoryCheckResponse> GetAsync(string maVatTu, int khoId)
    {
        if (string.IsNullOrWhiteSpace(maVatTu))
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.EmptySupplyCode);
        }
        if (khoId <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidOrganization);
        }

        var response = new InventoryCheckResponse();

        // KyKiemKeId
        var kiKiemKeChinh = await _kyKiemKeRepository.GetAsync(x => x.KyKiemKeChinh == 1);
        if (kiKiemKeChinh != null)
        {
            response.KyKiemKeId = kiKiemKeChinh.PhysicalInventoryId ?? 0;
            response.PhysicalInventoryName = string.IsNullOrWhiteSpace(kiKiemKeChinh.PhysicalInventoryName)? string.Empty : kiKiemKeChinh.PhysicalInventoryName;
        }
        // vật tư 
        var vatTuTonKho = await _vatTuTonKhoRepository.GetAsync(x => x.MaVatTu == maVatTu && x.OrganizationId == khoId);
        if (vatTuTonKho == null)
        {
            throw new NotFoundException(vatTuTonKho.GetTypeEx(), maVatTu);
        }

        var vatTuId = vatTuTonKho.VatTuId; 
        response.VatTuId = vatTuId;
        response.MaVatTu = maVatTu;
        response.TenVatTu = !string.IsNullOrWhiteSpace(vatTuTonKho.TenVatTu) ? vatTuTonKho.TenVatTu : string.Empty;
        response.DonViTinh = !string.IsNullOrWhiteSpace(vatTuTonKho.DonViTinh) ? vatTuTonKho.DonViTinh : string.Empty;
        
        var rootPath = _imagePath.RootPath;                               // "D:"
        var relativeBasePath = _imagePath.RelativeBasePath;               // "/4.Dev/NMD.24.TMQRCODE.5031-5035/WebAdmin/IMGVatTu"
        var localBasePath =  (rootPath + relativeBasePath).Replace("/", "\\");
        var folderImagePath = $@"{localBasePath}\{vatTuId}";

        if (Directory.Exists(folderImagePath))
        {
            var imageFiles = Directory.GetFiles(folderImagePath);

            // Xây dựng đường dẫn hoàn chỉnh cho mỗi ảnh
            foreach (var file in imageFiles)
            {
                // Lấy tên file (không bao gồm đường dẫn)
                var fileName = Path.GetFileName(file);
                
                // Tạo URL hoàn chỉnh từ urlPath và tên file
                var fullPath = Path.Combine(relativeBasePath, vatTuId.ToString(), fileName).Replace("\\", "/");

                // Thêm vào danh sách đường dẫn
                response.ImagePaths.Add(fullPath);
            }
        }
        // ảnh đại diện
        if (response.ImagePaths.Any())
        {
            response.Image = response.ImagePaths.First();
        }
        
        // kỳ kiểm kê
        var inventoryCheckInformation =
            await _vatTuRepository.GetInventoryCheckInformationAsync(vatTuId, response.KyKiemKeId);
        if (inventoryCheckInformation != null)
        {
            var inventoryCheckInformationMapper = _mapper.Map<InventoryCheckResponse>(inventoryCheckInformation);
            response.KyKiemKeChiTietId = inventoryCheckInformationMapper.KyKiemKeChiTietId;
            response.PhysicalInventoryName = inventoryCheckInformationMapper.PhysicalInventoryName;
            response.SoLuongSoSach = inventoryCheckInformationMapper.SoLuongSoSach;
            response.SoLuongKiemKe = inventoryCheckInformationMapper.SoLuongKiemKe;
            response.SoLuongChenhLech = inventoryCheckInformationMapper.SoLuongChenhLech;
            response.SoThe = inventoryCheckInformationMapper.SoThe;
            response.TrangThai = inventoryCheckInformationMapper.TrangThai is null ? 0 : inventoryCheckInformationMapper.TrangThai;
            response.TinhTrang_text = response.TrangThai == 0 ? $"Chưa kiểm kê" : $"Đã kiểm kê";
        }

        // DFF
        response.SupplyDff =
            (await _kyKiemKeChiTietDffRepository.GetAsync(x =>
                x.VatTuId == vatTuTonKho.VatTuId && x.KyKiemKeChiTietId == response.KyKiemKeChiTietId))
            .Adapt<SupplyDffResponse>();

        // vị trí kho chính và phụ

        response.OrganizationCode = vatTuTonKho.MaKho ?? string.Empty ;
        var subInventoryCode = vatTuTonKho.SubinventoryCode ?? string.Empty ;
        response.SubInventoryCode = subInventoryCode;
        if (subInventoryCode == string.Empty)
        {
            response.SubInventoryName = string.Empty ;
        }
        else
        {
            var subInventory = await _khoRepository.GetAsync(x => x.OrganizationId == vatTuTonKho.OrganizationId);
            response.SubInventoryName = subInventory is null ? string.Empty : string.IsNullOrWhiteSpace(subInventory.OrganizationName) ? string.Empty : subInventory.OrganizationName;
        }

        // vị trí chi tiết trong kho
        var positions = (await _vatTuRepository.GetPositionAsync(vatTuId)).Adapt<IEnumerable<SuppliesLocation>>()
            .ToList();

        if (positions.Count > 0)
        {
            response.SuppliesLocation = positions;
        }

        // LOT
        response.LotNumber = string.IsNullOrWhiteSpace(vatTuTonKho.LotNumber) ? string.Empty : vatTuTonKho.LotNumber;
        // Số lượng tồn kho
        response.OnhandQuantity = vatTuTonKho.OnhandQuantity ?? 0;
        return response;
    }


    public async Task<int> ModifySuppliesDffAsync(int vatTuId, int kyKiemKeId, int kyKiemKeChiTietId, decimal soLuongKiemKe,
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

        if (kyKiemKeChiTietId < 0)
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
        // nếu vật tư chưa kiểm kê bao giờ => thêm mới kì kiểm kê chi tiết cho vật tư => thêm mới DFF
        if (kyKiemKeChiTietId == 0)
        {
            // Có 2 trường hợp:
            var kyKiemKeChiTiet = await _kyKiemKeChiTietRepository.GetAsync(x => x.VatTuId == vatTuId && x.KyKiemKeId == kyKiemKeId);
            // 1 là có kỳ kiểm kê chi tiết rồi => nhập sai Id
            if (kyKiemKeChiTiet != null)
            {
                throw new BadRequestException(Constants.Exceptions.Messages.InventoryCheck.InvalidKyKiemKeChiTiet,
                    new List<string>{nameof(kyKiemKeChiTietId) + " is existed and not true."});
            }
            // 2 là không có kỳ kiểm kê chi tiết nào => thêm mới kỳ kiểm kê chi tiết
            await CreateInventoryCheckDetailAsync(vatTuId, kyKiemKeId, soLuongKiemKe);
            var kyKiemKeChiTietNew = await _kyKiemKeChiTietRepository.GetAsync(x => x.VatTuId == vatTuId && x.KyKiemKeId == kyKiemKeId);
            var kyKiemKeChiTietIdNew = kyKiemKeChiTietNew != null ? kyKiemKeChiTietNew.KyKiemKeChiTietId : 0;
            var dffToCreate = new QlvtKyKiemKeChiTietDff();

            dffToCreate.VatTuId = vatTuId;
            dffToCreate.KyKiemKeChiTietId = kyKiemKeChiTietIdNew;
            if (soLuongKiemKe > 0)
            {
                dffToCreate.PhanTramMatPhamChat = request.SoLuongMatPhamChat / soLuongKiemKe * 100;
                dffToCreate.PhanTramKemPhamChat = request.SoLuongKemPhamChat / soLuongKiemKe * 100;
                dffToCreate.PhanTramDong = request.SoLuongDong / soLuongKiemKe * 100;
                dffToCreate.TsKemPcMatPc = request.SoLuongMatPhamChat + request.SoLuongKemPhamChat;
            }
            else
            {
                dffToCreate.PhanTramMatPhamChat = 0;
                dffToCreate.PhanTramKemPhamChat = 0;
                dffToCreate.PhanTramDong = 0;
                dffToCreate.TsKemPcMatPc = 0;
            }
            var responseToCreate = _mapper.Map(request, dffToCreate);
            return await _kyKiemKeChiTietDffRepository.InsertAsync(responseToCreate);
        }

        var currentSuppliesDff = await _kyKiemKeChiTietDffRepository.GetAsync(x => x.VatTuId == vatTuId && x.KyKiemKeChiTietId == kyKiemKeChiTietId);
        // Nếu có kỳ kiểm kê chi tiết nhưng chưa có dữ liệu DFF => thêm mới DFF
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

    public async Task<int> ModifySuppliesQtyAsync(int vatTuId, int kyKiemKeId, decimal soLuongKiemKe)
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
          return await CreateInventoryCheckDetailAsync(vatTuId, kyKiemKeId, soLuongKiemKe);
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
    

    public async Task<int> CreateInventoryCheckDetailAsync(int vatTuId, int kyKiemKeId, decimal soLuongKiemKe)
    {
        var currentUser = await _nguoiDungService.GetCurrentUserAsync();
        var accountId = currentUser.MaNguoiDung;
        var warehouses = await _vatTuRepository.GetWarehouseIdAsync(vatTuId);
        if (warehouses != null)
        {
            warehouses = _mapper.Map<WarehouseIdResponse>(warehouses);

        }
        var qtyToCreate = new QlvtKyKiemKeChiTiet
        {
            VatTuId = vatTuId,
            KyKiemKeId = kyKiemKeId,
            SoLuongKiemKe = soLuongKiemKe,
            SoLuongSoSach = soLuongKiemKe,
            SoLuongChenhLech = 0,
            NgayKiemKe = DateTime.Now,
            NguoiKiemKeId = accountId,
            NguoiKiemKeTen = _authorizedContextFacade.Username,
            TrangThai = (short?)TrangThaiKyKiemKeChiTiet.DaKiemKe,
            KhoChinhId = warehouses != null? warehouses.KhoChinhId : null,
            KhoPhuId = warehouses != null? warehouses.KhoPhuId : null,
        };
        return await _kyKiemKeChiTietRepository.InsertAsync(qtyToCreate);
    }
    
}