using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.TraCuu.Responses;
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
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;
using WarehouseResponseItem = ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses.WarehouseResponseItem;

namespace ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;

public class MuaSamVatTuService : IMuaSamVatTuService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGioHangService _gioHangService;
    private readonly INguoiDungService _nguoiDungService;
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IMuaSamVatTuNewRepository _muaSamVatTuNewRepository;
    private readonly IMuaSamPhieuDeXuatRepository _muaSamPhieuDeXuatRepository;
    private readonly IMuaSamPhieuDeXuatDetailRepository _muaSamPhieuDeXuatDetailRepository;
    private readonly IKhoRepository _khoRepository;
    private readonly IGioHangRepository _gioHangRepository;
    private readonly IVanBanKyRepository _vanBanKyRepository;
    private readonly IVatTuBoMaRepository _vatTuBoMaRepository;
    private readonly IMuaSamPdxKyRepository _muaSamPdxKyRepository;
    private readonly IVatTuTonKhoRepository _vatTuTonKhoRepository;
    private readonly INguoiDungRepository _nguoiDungRepository;
    private readonly IViTriCongViecRepository _viTriCongViecRepository;
    private readonly ICauHinhVanBanKyRepository _cauHinhVanBanKyRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;
    private readonly ImagePath _imagePath;
    private readonly IMapper _mapper;

    public MuaSamVatTuService(
        IGioHangService gioHangService,
        INguoiDungService nguoiDungService,
        IVatTuRepository vatTuRepository,
        IMuaSamVatTuNewRepository muaSamVatTuNewRepository,
        IMuaSamPhieuDeXuatRepository muaSamPhieuDeXuatRepository,
        IMuaSamPhieuDeXuatDetailRepository muaSamPhieuDeXuatDetailRepository,
        IKhoRepository khoRepository,
        IGioHangRepository gioHangRepository,
        IVanBanKyRepository vanBanKyRepository,
        IVatTuBoMaRepository vatTuBoMaRepository,
        IMuaSamPdxKyRepository muaSamPdxKyRepository,
        IVatTuTonKhoRepository vatTuTonKhoRepository,
        INguoiDungRepository nguoiDungRepository,
        IViTriCongViecRepository viTriCongViecRepository,
        ICauHinhVanBanKyRepository cauHinhVanBanKyRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IUnitOfWork unitOfWork,
        IOptions<ImagePath> imagePath,
        IMapper mapper)
    {
        _gioHangService = gioHangService;
        _nguoiDungService = nguoiDungService;
        _vatTuRepository = vatTuRepository;
        _muaSamVatTuNewRepository = muaSamVatTuNewRepository;
        _muaSamPhieuDeXuatRepository = muaSamPhieuDeXuatRepository;
        _muaSamPhieuDeXuatDetailRepository = muaSamPhieuDeXuatDetailRepository;
        _khoRepository = khoRepository;
        _gioHangRepository = gioHangRepository;
        _vanBanKyRepository = vanBanKyRepository;
        _vatTuBoMaRepository = vatTuBoMaRepository;
        _muaSamPdxKyRepository = muaSamPdxKyRepository;
        _vatTuTonKhoRepository = vatTuTonKhoRepository;
        _nguoiDungRepository = nguoiDungRepository;
        _viTriCongViecRepository = viTriCongViecRepository;
        _cauHinhVanBanKyRepository = cauHinhVanBanKyRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imagePath = imagePath.Value;
    }

    public async Task<PagedList<SupplyListResponseItem>> ListVatTuAsync(SupplyListRequest request)
    {
        // Validate
        await ValidationHelper.ValidateAsync(request, new SupplyListRequestValidation());
        // Nhập thông tin kho là bắt buộc với các trường hợp vật tư trong hệ thống
        if (request.IsSystemSupply is true && request.IdKho == 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidOrganization);
        }
        
        // Nếu là vật tư ngoài hệ thống thì show kết quả luôn
        if (request.IsSystemSupply is false)
        {
            var listVatTuNew = (await _muaSamVatTuNewRepository.ListAsync(
                    string.IsNullOrWhiteSpace(request.TenVatTu) ? string.Empty : request.TenVatTu.ToLower(),
                    string.IsNullOrWhiteSpace(request.MaVatTu) ? string.Empty : request.MaVatTu.ToLower(),
                    request.GetPageIndex(),
                    request.GetPageSize()))
                .Adapt<PagedList<SupplyListResponseItem>>();
            return listVatTuNew;
        }

        // Mặc định khi load trang => lấy vật tư từ bảng tồn kho theo khoId
        if (request.Is007A == false)
        {
            var listVatTuTonKho = (await _vatTuTonKhoRepository.ListAsync(
                    string.IsNullOrWhiteSpace(request.TenVatTu) ? string.Empty : request.TenVatTu.ToLower(),
                    string.IsNullOrWhiteSpace(request.MaVatTu) ? string.Empty : request.MaVatTu.ToLower(),
                    request.IdKho,
                    request.ListIdToMay,
                    request.ListIdGiaKe,
                    request.ListIdNgan,
                    request.ListMaNhom,
                    request.GetPageIndex(),
                    request.GetPageSize()))
                .Adapt<PagedList<SupplyListResponseItem>>();
            var updatedItems = listVatTuTonKho.Items.ToList();
            foreach (var item in updatedItems)
            {
                item.Image = _gioHangService.GetSupplyImage(item.VatTuId);
            }
            listVatTuTonKho.Items = updatedItems; 
            return listVatTuTonKho;
        }
        // Case lọc theo filter => lấy các vật tư trong bảng vật tư/ vật tư mới mà không có trong bảng tồn kho 
        else
        {
            var listVatTuTonKhoIds = (await _vatTuTonKhoRepository.ListVatTuIdAsync()).ToList();
            // Case vật tư có trong hệ thống => vật tư
            var listVatTu = (await _vatTuRepository.ListAsync(
                    string.IsNullOrWhiteSpace(request.TenVatTu) ? string.Empty : request.TenVatTu.ToLower(),
                    string.IsNullOrWhiteSpace(request.MaVatTu) ? string.Empty : request.MaVatTu.ToLower(),
                    request.IdKho,
                    request.ListIdToMay,
                    request.ListIdGiaKe,
                    request.ListIdNgan,
                    request.ListMaNhom,
                    listVatTuTonKhoIds,
                    request.GetPageIndex(),
                    request.GetPageSize()))
                .Adapt<PagedList<SupplyListResponseItem>>();
            // Case vật tư không có trong hệ thống => vật tư mới
            if (listVatTu.Total == 0 || request.IsSystemSupply == false)
            {
                var listVatTuNew = (await _muaSamVatTuNewRepository.ListAsync(
                        string.IsNullOrWhiteSpace(request.TenVatTu) ? string.Empty : request.TenVatTu.ToLower(),
                        string.IsNullOrWhiteSpace(request.MaVatTu) ? string.Empty : request.MaVatTu.ToLower(),
                        request.GetPageIndex(),
                        request.GetPageSize()))
                    .Adapt<PagedList<SupplyListResponseItem>>();
                return listVatTuNew;
            }
            var updatedItems = listVatTu.Items.ToList();
            foreach (var item in updatedItems)
            {
                item.Image = _gioHangService.GetSupplyImage(item.VatTuId);
            }
            listVatTu.Items = updatedItems; 
            return listVatTu;
        }
    }

    public async Task<SupplyOrderDetailResponse> GetSupplyOrderDetailAsync(int vatTuId, int khoId, bool isSystemSupply)
    {
        if (vatTuId <= 0 )
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }
        
        var response = new SupplyOrderDetailResponse();
        // if Id is VatTuId => get information from QlvtVatTu table
        if (isSystemSupply)
        {
            var vatTu = await _vatTuRepository.GetAsync(x => x.VatTuId == vatTuId && x.KhoId == khoId);
            if (vatTu == null)
            {
                throw new NotFoundException(vatTu.GetTypeEx(), null);
            }
            
            response.TenVatTu = vatTu.TenVatTu ?? string.Empty;
            response.ThongSoKyThuat = vatTu.MoTa ?? string.Empty;
            response.GhiChu = vatTu.GhiChu ?? string.Empty;
            response.DonGia = vatTu.DonGia ?? 0;
            var rootPath = _imagePath.RootPath;              
            var relativeBasePath = _imagePath.RelativeBasePath;             
            var localBasePath =  (rootPath + relativeBasePath).Replace("/", "\\"); 
            var folderImagePath = $@"{localBasePath}\{vatTuId}";
            if (Directory.Exists(folderImagePath))
            {
                var imageFiles = Directory.GetFiles(folderImagePath);

                foreach (var file in imageFiles)
                {
                    var fileName = Path.GetFileName(file);
                    var fullPath = Path.Combine(relativeBasePath, vatTuId.ToString(), fileName).Replace("\\", "/");
                    response.ImagePaths.Add(fullPath);
                }
            }

            var existedTonKho = await _vatTuTonKhoRepository.ExistsAsync(x => x.VatTuId == vatTuId);
            response.Is007A = existedTonKho ? Is007A.TonKho : Is007A.KhongTonKho;
            var inventory = await _vatTuRepository.GetLotNumberAsync(vatTuId, vatTu.KhoId);
            if (inventory == null) return response;
               
            var inventoryMapper = _mapper.Map<LookupSuppliesResponse>(inventory);
            response.OnhandQuantity = inventoryMapper.OnhandQuantity;
            return response;
        }
        //if Id is VatTuNewId => get information from QlvtMuaSamVatTuNew table
         var vatTuNew = await _muaSamVatTuNewRepository.GetAsync(vatTuId);
         if (vatTuNew == null)
         {
             throw new NotFoundException(vatTuNew.GetTypeEx(), null);
         }

         response.TenVatTu = vatTuNew.TenVatTu;
         response.ThongSoKyThuat = vatTuNew.ThongSoKyThuat ?? string.Empty;
         response.DonGia = vatTuNew.DonGia ?? 0;
         response.GhiChu = vatTuNew.GhiChu ?? string.Empty;
         response.OnhandQuantity = 0;
         response.Is007A = Is007A.KhongTonKho;
         return response;
    }

    public async Task<int> ProcessSupplyTicketCreationAsync(ProcessSupplyTicketCreationRequest request)
    {
        var username = _authorizedContextFacade.Username;
        var currentUser = await _nguoiDungRepository.GetAsync(x => x.TenDangNhap == username);
        if (currentUser is null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Login.FirstTimeLogin);
        }

        var currentUserId = currentUser.MaNguoiDung;
        if (!request.SupplyTicketDetails.Any())
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.EmptySupplies);
        }
        // Bắt đầu transaction
        using (var transaction = _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                // Thêm phiếu
                var supplyTicketName = await CreateSupplyTicketAsync(request.Description, currentUserId);

                var addedSupplyTicket =
                    await _muaSamPhieuDeXuatRepository.GetAsync(x => x.TenPhieu == supplyTicketName);
                if (addedSupplyTicket == null)
                {
                    throw new BadRequestException(Constants.Exceptions.Messages.Common.InsertFailed);
                }
                // thêm các vật tư đã chọn trong giỏ hàng vào phiếu
                var supplyTicketId = addedSupplyTicket.Id;
                await CreateManySupplyTicketDetailAsync(supplyTicketId, request.SupplyTicketDetails);
                
                // xóa các vật tư vừa chọn trong giỏ hàng
                var gioHangIds = request.SupplyTicketDetails.Select(supplyTicketDetail => supplyTicketDetail.GioHangId).ToList();
                var suppliesInCart = await _gioHangRepository.ListAsync(x => gioHangIds.Contains(x.GioHangId));
                await _gioHangRepository.DeleteManyAsync(suppliesInCart);
                
                // thêm mới 2 phiếu vào bảng ký
                await CreateTwoTextToSignAsync(supplyTicketId);
                
                await _unitOfWork.CommitAsync();
                return supplyTicketId;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
    
    public async Task<PagedList<SupplyTicketListResponseItem>> ListSupplyTicketAsync(SupplyTicketRequest requests)
     {
        #region Validate  
        if (requests.SupplyTicketStatus is not null && !Enum.IsDefined(typeof(SupplyTicketStatus), requests.SupplyTicketStatus) 
            || requests.SupplyTicketStatus == SupplyTicketStatus.Unknown)
        {
            throw new ArgumentException(Constants.Exceptions.Messages.SupplyTicket.InvalidSupplyTicketStatus);
        }

        #endregion
        
        #region Lấy thông tin user
        var username = _authorizedContextFacade.Username;
        var currentUser = await _nguoiDungRepository.GetAsync(x => x.TenDangNhap == username);
        if (currentUser is null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Login.FirstTimeLogin);
        }
        var userId = currentUser.MaNguoiDung;
        #endregion
        
        #region TH1: là quản trị viên cao cấp: được xem tất cả phiếu
        var phieuDeXuatOtherIds = new List<int>();
        if (currentUser.LaQuanTriVienCaoCap == true)
        {
            var listPhieu = (await _muaSamPhieuDeXuatRepository.ListSupplyTicketAsync(
                    string.IsNullOrWhiteSpace(requests.Keywords) ? string.Empty : requests.Keywords.ToLower(),
                    requests.SupplyTicketStatus ?? SupplyTicketStatus.Unknown,
                    0, 
                    Constants.MaDoiTuongKy.Unknown,
                    phieuDeXuatOtherIds,        // set phieuDeXuatOtherIds = null cho dễ query chung hàm
                    requests.GetPageIndex(),
                    requests.GetPageSize()))
                .Adapt<PagedList<SupplyTicketListResponseItem>>();
            return listPhieu;
        }
        #endregion
        // TH2: là người thường vào ký
        //var maDonViSuDung = currentUser.MaDonViSuDung;
         
        var viTriCongViecHienTai = await _viTriCongViecRepository.GetAsync(x => x.Id == currentUser.ViTri);
        if (viTriCongViecHienTai is null)
        {                                                           
            return default;
        }
        var maDoiTuongKyHienTai = viTriCongViecHienTai.MaDoiTuongKy;
        if (maDoiTuongKyHienTai is null) 
        {
            return default;
        }
        // Lấy danh sách phiếu đề xuất theo maDoiTuongKyHienTai(ví dụ: Người lập)
        var listPhieuDeXuatIds = (await _muaSamPdxKyRepository.ListPhieuDeXuatIdsAsync(maDoiTuongKyHienTai)).ToList();
        if (!listPhieuDeXuatIds.Any()) return default;
        
        #region TH2.1: Người login là người lập phiếu đề xuất
        
        // => Trả về các phiếu có mã người thêm = mã người đang login
        if (maDoiTuongKyHienTai == Constants.MaDoiTuongKy.NguoiLap)
        {
            var listPhieu = (await _muaSamPhieuDeXuatRepository.ListSupplyTicketAsync(
                    string.IsNullOrWhiteSpace(requests.Keywords) ? string.Empty : requests.Keywords.ToLower(),
                    requests.SupplyTicketStatus ?? SupplyTicketStatus.Unknown,
                    userId,
                    Constants.MaDoiTuongKy.NguoiLap,
                    listPhieuDeXuatIds,
                    requests.GetPageIndex(),
                    requests.GetPageSize()))
                .Adapt<PagedList<SupplyTicketListResponseItem>>();
            return listPhieu;
        }
        #endregion
        
        #region TH2.2: Người login là người ký sau người thứ nhất => set trạng thái lùi về 1 người
        else
        { 
            var previousMaDoiTuongKy = maDoiTuongKyHienTai switch
            {
                Constants.MaDoiTuongKy.KiemSoatAT => Constants.MaDoiTuongKy.NguoiLap, 
                Constants.MaDoiTuongKy.TruongDonVi => Constants.MaDoiTuongKy.KiemSoatAT,
                Constants.MaDoiTuongKy.Ph_KTAT => Constants.MaDoiTuongKy.TruongDonVi,
                Constants.MaDoiTuongKy.Ph_KHVT => Constants.MaDoiTuongKy.Ph_KTAT,
                Constants.MaDoiTuongKy.TongGiamDoc => Constants.MaDoiTuongKy.Ph_KHVT,
                _ => null
            };
            if (previousMaDoiTuongKy == null) return default;
            
            // Trả về danh sách phiếu "đã ký" của người ngay trước người dùng hiện tại
            phieuDeXuatOtherIds = (await _muaSamPdxKyRepository.ListPhieuDeXuatOtherIdsAsync(previousMaDoiTuongKy, listPhieuDeXuatIds)).ToList();
            if (!phieuDeXuatOtherIds.Any())
            {
                return default;
            }

            var listPhieu = (await _muaSamPhieuDeXuatRepository.ListSupplyTicketAsync(
                    string.IsNullOrWhiteSpace(requests.Keywords) ? string.Empty : requests.Keywords.ToLower(),
                    requests.SupplyTicketStatus ?? SupplyTicketStatus.Unknown,
                    userId,
                    maDoiTuongKyHienTai,
                    phieuDeXuatOtherIds,
                    requests.GetPageIndex(),
                    requests.GetPageSize()))
                .Adapt<PagedList<SupplyTicketListResponseItem>>();
            return listPhieu;
        }
        #endregion
    }

    private async Task<int> CreateManySupplyTicketDetailAsync(int supplyTicketId, List<SupplyTicketDetailRequest> requests)
     {
         var relativeBasePath = _imagePath.RelativeBasePath;
        if (!requests.Any())
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.EmptySupplies);
        }
        var listSupplyTicketDetail = new List<QlvtMuaSamPhieuDeXuatDetail>();
        foreach (var supplyCart in requests)
        {
            if (supplyCart.GioHangId < 1)
            {
                throw new BadRequestException(Constants.Exceptions.Messages.Cart.InvalidCartInfo, new List<string>{ nameof(supplyCart.GioHangId)+ " is invalid"});
            }

            if (supplyCart.VatTuId < 1)
            {
                throw new BadRequestException(Constants.Exceptions.Messages.Cart.InvalidCartInfo, new List<string>{ nameof(supplyCart.VatTuId)+ " is invalid"});
            }
            await ValidationHelper.ValidateAsync(supplyCart, new SupplyTicketDetailRequestValidation());
            
            var supplyTicketDetail = supplyCart.Adapt<QlvtMuaSamPhieuDeXuatDetail>();
            supplyTicketDetail.PhieuDeXuatId = supplyTicketId;
            listSupplyTicketDetail.Add(supplyTicketDetail);
        }
        return await _muaSamPhieuDeXuatDetailRepository.InsertManyAsync(listSupplyTicketDetail);
    }

    public async Task<SupplyTicketDetailResponse> GetSupplyTicketDetailAsync(int supplyTicketId)
    {
        #region Lấy thông tin user hiện tại
        var username = _authorizedContextFacade.Username;
        var currentUser = await _nguoiDungRepository.GetAsync(x => x.TenDangNhap == username);
        if (currentUser is null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Login.FirstTimeLogin);
        }
        #endregion

        var response = new SupplyTicketDetailResponse();
        response.IsEditable = false;
        if (supplyTicketId <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.SupplyTicket.InvalidSupplyTicket,
                new List<string> { nameof(supplyTicketId) + " is invalid" });
        }
        var supplyTicket = await _muaSamPhieuDeXuatRepository.GetAsync(x => x.Id == supplyTicketId);
        if (supplyTicket == null)
        {
            throw new NotFoundException(supplyTicket.GetTypeEx(), null);
        }
        response.TenPhieu = supplyTicket.TenPhieu ?? string.Empty;
        response.MoTa = supplyTicket.MoTa ?? string.Empty;
        
        #region Cho phép hiển thị nút sửa
        
        // nếu người ký là người số 4(Ph_KHVT) và trạng thái phiếu là huỷ duyệt => cho phép sửa
        var viTriCongViecHienTai = await _viTriCongViecRepository.GetAsync(x => x.Id == currentUser.ViTri);
        if (viTriCongViecHienTai is not null)
        {
            var maDoiTuongKyHienTai = viTriCongViecHienTai.MaDoiTuongKy;
            if (supplyTicket.TrangThai == (byte)SupplyTicketStatus.CancelledApproval 
                && maDoiTuongKyHienTai== Constants.MaDoiTuongKy.Ph_KTAT)
            {
                response.IsEditable = true;
            }
        }
        #endregion
        
        #region Danh sách vật tư
        var listSupplies = (await _muaSamPhieuDeXuatDetailRepository.ListAsync(supplyTicketId)).Adapt<IEnumerable<SupplyInListResponse>>().ToList();
        if (!listSupplies.Any())
        {
            response.DanhSachVatTu = listSupplies;
            response.Tong = 0;
            return response;
        }
        
        foreach (var supply in listSupplies.Where(supply => supply.IsSystemSupply))
        {
            supply.Image = _gioHangService.GetSupplyImage(supply.VatTuId);
        }
        #endregion
        
        response.DanhSachVatTu = listSupplies;
        response.Tong = response.DanhSachVatTu.Count;
        return response;
    }

    public async Task<int> DeleteSupplyTicketAsync(int supplyTicketId)
    {
        if (supplyTicketId <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.SupplyTicket.InvalidSupplyTicket);
        }
        var currentSupplyTicket = await _muaSamPhieuDeXuatRepository.GetAsync(supplyTicketId);
        if (currentSupplyTicket == null)
        {
            throw new NotFoundException(currentSupplyTicket.GetTypeEx(), null);
        }
        var currentSupplyTicketDetails = (await _muaSamPhieuDeXuatDetailRepository.ListAsync(x => x.PhieuDeXuatId == supplyTicketId)).ToList();
        if (currentSupplyTicketDetails.Any())
        {
            await _muaSamPhieuDeXuatDetailRepository.DeleteManyAsync(currentSupplyTicketDetails);
        }

        var currentSignedDocuments = (await _vanBanKyRepository.ListAsync(x => x.PhieuId == supplyTicketId)).ToList();
        if (currentSignedDocuments.Any())
        {
            await _vanBanKyRepository.DeleteManyAsync(currentSignedDocuments);
        }

        var currentSignedSupplyTickets = (await _muaSamPdxKyRepository.ListAsync(x => x.PhieuDeXuatId == supplyTicketId)).ToList();
        if (currentSignedSupplyTickets.Any())
        {
            await _muaSamPdxKyRepository.DeleteManyAsync(currentSignedSupplyTickets);
        }
        return await _muaSamPhieuDeXuatRepository.DeleteAsync(currentSupplyTicket);
    }

    public async Task<IEnumerable<WarehouseResponseItem>> ListWarehousesAsync()
    {
        var response = (await _khoRepository.ListWarehousesAsync()).Adapt<IEnumerable<WarehouseResponseItem>>();
        return response;
    }

    private async Task<string> CreateSupplyTicketAsync(string? description, int currentUserId)
    {
        var supplyTicket = new QlvtMuaSamPhieuDeXuat
        {
            TenPhieu = $"Phiếu đề xuất cung ứng VTTB - {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
            MoTa = description,
            TrangThai = (byte?)SupplyTicketStatus.Unsigned,
            NgayThem = DateTime.Now,
            MaNguoiThem = currentUserId,
        };
        await _muaSamPhieuDeXuatRepository.InsertAsync(supplyTicket);
        return supplyTicket.TenPhieu;
    }

    private async Task<int> CreateTwoTextToSignAsync(int supplyTicketId)
    {
        var textTosign = new List<QlvtVanBanKy>()
        {
            new QlvtVanBanKy
            {
                PhieuId = supplyTicketId,
                MaLoaiVanBan = "PhieuDeXuat",
                NgayTao = DateTime.Now
            },
            new QlvtVanBanKy
            {
                PhieuId = supplyTicketId,
                MaLoaiVanBan = "PhieuDuyet",
                NgayTao = DateTime.Now
            }
        };
        return await _vanBanKyRepository.InsertManyAsync(textTosign);
    }
    
    public async Task<IEnumerable<QlvtVatTuBoMa>> ListGroupCodeAsync()
    {
        return await _vatTuBoMaRepository.ListAsync();
    }

    public async Task<int> CountSupplyTicketsByStatusAsync(SupplyTicketStatus status)
    {
        if (status == SupplyTicketStatus.Unknown || !Enum.IsDefined(typeof(SupplyTicketStatus), status))
        {
            throw new ArgumentException(Constants.Exceptions.Messages.SupplyTicket.InvalidSupplyTicketStatus);
        }
        return await _muaSamPhieuDeXuatRepository.CountAsync(x => x.TrangThai == (byte)status);
    }

    public async Task<IEnumerable<string>> ListCreatedSupplyTicketWarningAsync(List<int> vatTuIds)
    {
        var existingSupplyTickets = (await _muaSamPhieuDeXuatRepository.ListCreatedSupplyTicketWarningAsync(vatTuIds)).Adapt<List<SupplyTicketWithTotalCount>>();
        if (!existingSupplyTickets.Any())
        {
            return Enumerable.Empty<string>();
        }

        return existingSupplyTickets
            .Select(x => $"Vật tư {x.TenVatTu} đã được đề xuất với số lượng {x.SoLuong.FormatDecimal()} trong 1 phiếu khác.")
            .ToList();
    }

    public async Task<int> UpdateManySupplyQuantitiesAsync(int ticketId, List<UpdateSupplyQuantityRequestItem> requests)
    {
        #region Validate
        if (!requests.Any())
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.EmptySupplies);
        }
        var supplyTicket = await _muaSamPhieuDeXuatRepository.GetAsync(x => x.Id == ticketId);
        if (supplyTicket is null)
        {
            throw new NotFoundException(Constants.Exceptions.Messages.KyCungUng.InvalidPdx);
        }
        #endregion
        
        #region Update số lượng
        var supplyTicketDetailIds = requests.Select(x => x.Id).ToList();
        var listSupplyTicketDetails = (await _muaSamPhieuDeXuatDetailRepository.ListAsync(x => supplyTicketDetailIds.Contains(x.Id))).ToList();
        if (!listSupplyTicketDetails.Any())
        {
            throw new NotFoundException(Constants.Exceptions.Messages.KyCungUng.InvalidSupply);   
        }
        
        var updateList = new List<object>();
        foreach (var item in listSupplyTicketDetails)
        {
            var requestItem = requests.FirstOrDefault(x => x.Id == item.Id);
            if (requestItem != null)
            {
                item.SoLuong = requestItem.SoLuong;
                updateList.Add(new { Id = item.Id, SoLuong = item.SoLuong });
            }
        }

        return await _muaSamPhieuDeXuatDetailRepository.UpdateManyPartialAsync(listSupplyTicketDetails, updateList.ToArray());
        
        #endregion
    }
}