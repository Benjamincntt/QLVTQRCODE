using System.Net.Http.Headers;
using System.Text.Json;
using ESPlatform.QRCode.IMS.Core.DTOs.KySo;
using ESPlatform.QRCode.IMS.Core.DTOs.KySo.Response;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.Viettel;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Services.TbNguoiDungs;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Mapster;
using MassTransit.Initializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MobileCA.Application.Services.Viettel;
using MobileCA.Application.Services.Viettel.Dtos;
using SignFileImgDto = ESPlatform.QRCode.IMS.Core.DTOs.Viettel.SignFileImgDto;

namespace ESPlatform.QRCode.IMS.Core.Services.PhieuKy
{
    public class PhieuKyService : IPhieuKyService
    {
        private readonly INguoiDungService _nguoiDungService;
        private readonly IPhieuKyRepository _phieuKyRepository;
        private readonly IMuaSamPhieuDeXuatRepository _muaSamPhieuDeXuatRepository;
        private readonly IMuaSamPdxKyRepository _muaSamPdxKyRepository;
        private readonly ICauHinhVanBanKyRepository _cauHinhVanBanKyRepository;
        private readonly IVanBanKyRepository _vanBanKyRepository;
        private readonly IViTriCongViecRepository _viTriCongViecRepository;
        private readonly IConfiguration _configuration;
        private readonly IViettelMobileCAService _viettelCAService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ImagePath _imagePath;
        private readonly KySoPathVersion2 _kySoPathVersion2;
        public PhieuKyService(
            INguoiDungService nguoiDungService,
            IPhieuKyRepository phieuKyRepository,
            IMuaSamPhieuDeXuatRepository muaSamPhieuDeXuatRepository,
            IMuaSamPdxKyRepository muaSamPdxKyRepository,
            ICauHinhVanBanKyRepository cauHinhVanBanKyRepository,
            IVanBanKyRepository vanBanKyRepository, 
            IViTriCongViecRepository viTriCongViecRepository,
            IConfiguration configuration,
            IViettelMobileCAService viettelCAService,
            IUnitOfWork unitOfWork,
            IHttpClientFactory httpClientFactory,
            IOptions<ImagePath> imagePath,
            IOptions<KySoPathVersion2> kySoPathVersion2)
        {
            _nguoiDungService = nguoiDungService;
            _phieuKyRepository = phieuKyRepository;
            _muaSamPhieuDeXuatRepository = muaSamPhieuDeXuatRepository;
            _muaSamPdxKyRepository = muaSamPdxKyRepository;
            _cauHinhVanBanKyRepository = cauHinhVanBanKyRepository;
            _vanBanKyRepository = vanBanKyRepository;
            _viTriCongViecRepository = viTriCongViecRepository;
            _configuration = configuration;
            _viettelCAService = viettelCAService;
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _imagePath = imagePath.Value;
            _kySoPathVersion2 = kySoPathVersion2.Value;
        }

        public async Task<List<PhieuKyModel>> GetDanhSachPhieuKyAsync(DanhSachPhieuKyFilter requests)
        {
            var currentUser = await _nguoiDungService.GetCurrentUserAsync();
            var currentUserId = currentUser.MaNguoiDung;
            var lstPhieuKy = (await _phieuKyRepository.DanhSachPhieuDeXuatKy(requests, currentUserId))
                .Adapt<IEnumerable<PhieuKyModel>>().ToList(); 
            // Chuyển đổi lstPhieuKy thành DanhSachPhieuKyResponse
            var response = lstPhieuKy.Select(p => new PhieuKyModel
            {
                Id = p.Id,
                MaPhieu = p.MaPhieu,
                TenPhieu = p.TenPhieu,
                MoTa = p.MoTa,
                NgayThem = p.NgayThem,
                TrangThai = p.TrangThai,
                MaNguoiThem = p.MaNguoiThem,
                TenDonViSuDung = p.TenDonViSuDung,
                TenNguoiThem = p.TenNguoiThem,
                VanBanKy = p.VanBanKy != null ? new VanBanKyModel // Kiểm tra ChuKys có null không
                {
                    Id = p.VanBanKy.Id,
                    TrangThaiTaoFile = p.VanBanKy.TrangThaiTaoFile,
                    MaLoaiVanBan = p.VanBanKy.MaLoaiVanBan,
                    FilePath = p.VanBanKy.FilePath,
                    FileName = p.VanBanKy.FileName,
                } : null,
                ChuKy = p.ChuKy != null ? new ChuKyModel // Kiểm tra ChuKys có null không
                {
                    ChuKyId = p.ChuKy.ChuKyId,
                    ToaDo = p.ChuKy.ToaDo,
                    VanBanId = p.ChuKy.VanBanId,
                    ThuTuKy = p.ChuKy.ThuTuKy,
                    NgayKy = p.ChuKy.NgayKy,
                    MaDoiTuongKy = p.ChuKy?.MaDoiTuongKy,
                    page = p.ChuKy?.page,
                    pageHeight = p.ChuKy?.pageHeight,
                    TrangThai = p.ChuKy?.TrangThai ?? 0
                } : null
            }).ToList();
            return response;
        }

        /// <summary>
        /// ký số nhiều phiếu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public async Task<int> CapNhatThongTinKyAsync(KyPhieuRequest request)
        {
            if (!request.ItemsPhieuKy.Any() || request.ItemsPhieuKy == null)
            {
                throw new BadRequestException(Constants.Exceptions.Messages. KyCungUng.EmptyPhieuIds);
            }
            
            var currentUser = await _nguoiDungService.GetCurrentUserAsync();
            var userId = currentUser.MaNguoiDung;
            
            List<int> DanhSachIdPhieuDaKy = new List<int>();
            var lstMuaSamPdxKy = new List<QlvtMuaSamPdxKy>();
            var danhSachLoi = new List<string>(); // Danh sách để lưu trữ các lỗi
            using (var transaction = _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    foreach (var phieuKy in request.ItemsPhieuKy)
                    {
                        // Lấy thông tin phiếu mua sắm ký từ database dựa trên Id
                        var phieuMuaSam = await _phieuKyRepository.GetAsync(x => x.Id == phieuKy.PhieuId);

                        // Kiểm tra xem phiếu có tồn tại hay không
                        if (phieuMuaSam == null)
                        {
                            danhSachLoi.Add($"Phiếu có Id: {phieuKy.PhieuId} không tồn tại.");
                            continue;
                        }
                        if (phieuMuaSam.TrangThai != null && phieuMuaSam.TrangThai > 0)
                        {

                            danhSachLoi.Add($"Phiếu có Id: {phieuKy.PhieuId} {Constants.Exceptions.Messages.KyCungUng.Signed}.");
                            continue;
                        }
                        var maDoiTuongKy = phieuKy.MaDoiTuongKy != null ? phieuKy.MaDoiTuongKy.ToLower() : "";
                        var phieuMuaSamKy = await _muaSamPdxKyRepository.GetAsync(x => x.Id == phieuKy.ChuKyId
                                                                                    && x.ThuTuKy == phieuKy.ThuTu_Ky
                                                                                     && x.VanBanId == phieuKy.VanBan_Id
                                                                                      && (x.MaDoiTuongKy != null && x.MaDoiTuongKy.ToLower() == maDoiTuongKy));

                        if (phieuMuaSamKy == null)
                        {
                            danhSachLoi.Add($"Chưa có cấu hình chữ ký cho người ký với Id: {phieuKy.ChuKyId}.");
                            continue;
                        }
                        // Kiểm tra trạng thái của chữ ký
                        if (phieuMuaSamKy.TrangThai != null && phieuMuaSamKy.TrangThai > 0)
                        {
                            danhSachLoi.Add($"Chữ ký với Id: {phieuKy.ChuKyId} {Constants.Exceptions.Messages.KyCungUng.Signed}.");
                            continue;
                        }

                        // Nếu phiếu chưa ký, cập nhật thông tin

                        phieuMuaSamKy.LyDo = request.LyDo;
                        phieuMuaSamKy.NgayKy = DateTime.Now;
                        phieuMuaSamKy.NguoiKyId = userId;
                        phieuMuaSamKy.UsbSerial = request.Usb_Serial;
                        phieuMuaSamKy.TrangThai = Constants.Exceptions.Messages.KyCungUng.DaKy;
                        lstMuaSamPdxKy.Add(phieuMuaSamKy);

                    }
                    if (danhSachLoi.Any())
                    {
                        // Trả về danh sách lỗi
                        throw new BadRequestException(string.Join("; ", danhSachLoi));
                    }
                    if (lstMuaSamPdxKy.Any())
                    {
                        await _muaSamPdxKyRepository.UpdateManyAsync(lstMuaSamPdxKy);
                    }

                    await _unitOfWork.CommitAsync();

                    return lstMuaSamPdxKy.Count;
                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }
        }


        public async Task<object> BoQuaKhongKy(ModifiedKySo request)
        {
            if (request.PhieuId < 1)
            {
                return new ErrorResponse
                {
                    Message = $"Phiếu có Id: {request.PhieuId} {Constants.Exceptions.Messages.KyCungUng.InvalidPdx}."
                };
            }

            var currentUser = await _nguoiDungService.GetCurrentUserAsync();
            var userId = currentUser.MaNguoiDung;
            using (var transaction = _unitOfWork.BeginTransactionAsync())
            {
                try
                {

                    // Lấy thông tin phiếu mua sắm ký từ database dựa trên Id
                    var phieuMuaSam = await _phieuKyRepository.GetAsync(x => x.Id == request.PhieuId);

                    // Kiểm tra xem phiếu có tồn tại hay không
                    if (phieuMuaSam == null)
                    {
                        return new ErrorResponse
                        {
                            Message = $"Phiếu có Id: {request.PhieuId} không tồn tại."
                        };
                    }
                    var maDoiTuongKy = request.MaDoiTuongKy != null ? request.MaDoiTuongKy.ToLower() : "";

                    var cauHinhVanBanKy = await _cauHinhVanBanKyRepository.GetAsync(x => x.MaDoiTuongKy != null && x.MaDoiTuongKy.ToLower() == maDoiTuongKy);
                    if (cauHinhVanBanKy != null)
                    {
                        if (cauHinhVanBanKy.CoTheBoQua == false)
                        {
                            throw new BadRequestException(Constants.Exceptions.Messages.KyCungUng.CanNotIgnore);
                        }

                    }

                    var phieuMuaSamKy = await _muaSamPdxKyRepository.GetAsync(x => x.Id == request.ChuKyId
                                                                                 && x.VanBanId == request.VanBan_Id
                                                                                  && (x.MaDoiTuongKy != null && x.MaDoiTuongKy.ToLower() == maDoiTuongKy));

                    if (phieuMuaSamKy == null)
                    {
                        return new ErrorResponse
                        {
                            Message = "Chưa có cấu hình chữ ký cho người ký này."
                        };
                    }
                    else
                    {
                        if (phieuMuaSamKy.TrangThai >= 1)
                        {
                            return new ErrorResponse
                            {
                                Message = "Chữ ký này đã được ký."
                            };
                        }
                    }

                    phieuMuaSamKy.NgayKy = DateTime.Now;
                    phieuMuaSamKy.NguoiKyId = userId;
                    phieuMuaSamKy.TrangThai = Constants.Exceptions.Messages.KyCungUng.BoQua;

                    await _muaSamPdxKyRepository.UpdateAsync(phieuMuaSamKy);

                    await _unitOfWork.CommitAsync();

                    return request.PhieuId;

                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<VanBanKyModel> GetVanBanKyById(int id)
        {
            var vanBanKy = await _vanBanKyRepository.GetAsync(x => x.Id == id);
            if (vanBanKy == null)
            {
                return new VanBanKyModel();
                //throw new BadRequestException("Không tồn tại văn bản ký này");
            }
            var response = new VanBanKyModel
            {
                Id = vanBanKy.Id,
                PhieuId = vanBanKy.PhieuId,
                FileName = vanBanKy.FileName,
                FilePath = vanBanKy.FilePath,
                TrangThaiTaoFile = vanBanKy.TrangThaiTaoFile
            };
            return response;
        }

        public Task<string> GetFullFilePath(string filePath)
        {
            var kySoPath = _configuration.GetSection("KySoPath");
            var rootPath = kySoPath.GetValue<string>("RootPath");
            var relativeBasePath = kySoPath.GetValue<string>("RelativeBasePath");

            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(relativeBasePath))
            {
                throw new Exception("RootPath hoặc RelativeBasePath không được cấu hình đúng.");
            }

            // Lấy phần sau dấu "/" thứ hai
            var segments = filePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length >= 2)
            {
                filePath = string.Join("/", segments.Skip(1)); // Bỏ phần đầu tiên và nối lại
            }

            // Kết hợp đường dẫn rootPath và relativeBasePath
            var fullPath = Path.Combine(rootPath, relativeBasePath.Trim(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar), filePath.Trim(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));

            // Chuẩn hóa đường dẫn thành backslash cho Windows
            fullPath = fullPath.Replace("/", "\\");

            return Task.FromResult(fullPath);
        }

        public Task<string> GetRelativePath()
        {
            var kySoPath = _configuration.GetSection("KySoPath");
            var relativeBasePath = kySoPath.GetValue<string>("RelativeBasePath");

            if (string.IsNullOrEmpty(relativeBasePath))
            {
                throw new Exception("RootPath hoặc RelativeBasePath không được cấu hình đúng.");
            }
            // Chuẩn hóa đường dẫn thành backslash cho Windows
            return Task.FromResult(relativeBasePath.Replace("/", "\\"));
        }

        public async Task<object> UpdateThongTinKyAsync_(UpdateFileRequest request)
        {
            using (var transaction = _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    // Lấy thông tin phiếu mua sắm ký từ database dựa trên Id
                    var phieuMuaSam = await _phieuKyRepository.GetAsync(x => x.Id == request.PhieuId);
                    var maDoiTuongKy = request.MaDoiTuongKy; //request.MaDoiTuongKy?.ToLower() ?? "";
                    // Kiểm tra xem phiếu có tồn tại hay không
                    if (phieuMuaSam == null)
                    {
                        return new ErrorResponse
                        {
                            Message = $"Phiếu có Id: {request.PhieuId} không tồn tại."
                        };
                    }
                    // Lấy thông tin ký từ bảng QLVT_MuaSam_PhieuDeXuat_Ky

                    var phieuMuaSamKy = await _muaSamPdxKyRepository.GetAsync(x => x.Id == request.ChuKyId);

                    // Kiểm tra xem chữ ký có tồn tại hay không
                    if (phieuMuaSamKy == null)
                    {
                        return new ErrorResponse
                        {
                            Message = "Chưa có cấu hình chữ ký cho người ký này."
                        };
                    }
                    else
                    {
                        if (phieuMuaSamKy.TrangThai == 1)
                        {
                            return new ErrorResponse
                            {
                                Message = "Chữ ký này đã được ký."
                            };
                        }
                    }
                    // Nếu chữ ký hợp lệ, tiến hành cập nhật thông tin
                    phieuMuaSamKy.NgayKy = DateTime.Now;
                    phieuMuaSamKy.NguoiKyId = request.SignUserId;
                    phieuMuaSamKy.UsbSerial = request.SignType;
                    phieuMuaSamKy.TrangThai = Constants.Exceptions.Messages.KyCungUng.DaKy;
                    await _muaSamPdxKyRepository.UpdateAsync(phieuMuaSamKy);

                    switch (maDoiTuongKy)
                    {
                        case MaDoiTuongKyConstants.NguoiLap:
                            phieuMuaSam.TrangThai = TrangThaiPhieu.TrongQuaTrinhKy;
                            break;
                        case MaDoiTuongKyConstants.TongGiamDoc:
                            phieuMuaSam.TrangThai = TrangThaiPhieu.TongGiamDoc;
                            break;

                        default:
                            phieuMuaSam.TrangThai = TrangThaiPhieu.TrongQuaTrinhKy;
                            break;
                    }


                    await _phieuKyRepository.UpdateAsync(phieuMuaSam);

                    await _unitOfWork.CommitAsync();
                    return new { PhieuId = request.PhieuId, Message = "Cập nhật thông tin thành công." };
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi xảy ra, rollback transaction và ném ngoại lệ
                    await _unitOfWork.RollbackAsync();
                    return new ErrorResponse
                    {
                        Message = "Cập nhật thông tin không thành công.",
                        Details = ex.Message
                    };
                }
            }
        }

        public async Task<object> UpdateThongTinKyAsync(UpdateFileRequest request)
        {
            // Không cần bắt đầu transaction ở đây nếu transaction đã được bắt đầu ở nơi gọi
            try
            {
                var currentUser = await _nguoiDungService.GetCurrentUserAsync();
                var userId = currentUser.MaNguoiDung;
                // Lấy thông tin phiếu mua sắm ký từ database dựa trên Id
                var phieuMuaSam = await _phieuKyRepository.GetAsync(x => x.Id == request.PhieuId);
                var maDoiTuongKy = request.MaDoiTuongKy; // request.MaDoiTuongKy?.ToLower() ?? "";

                // Kiểm tra xem phiếu có tồn tại hay không
                if (phieuMuaSam == null)
                {
                    return new ErrorResponse
                    {
                        Message = $"Phiếu có Id: {request.PhieuId} không tồn tại."
                    };
                }

                // Lấy thông tin ký từ bảng QLVT_MuaSam_PhieuDeXuat_Ky
                var phieuMuaSamKy = await _muaSamPdxKyRepository.GetAsync(x => x.Id == request.ChuKyId);

                // Kiểm tra xem chữ ký có tồn tại hay không
                if (phieuMuaSamKy == null)
                {
                    return new ErrorResponse
                    {
                        Message = "Chưa có cấu hình chữ ký cho người ký này."
                    };
                }
                else
                {
                    if (phieuMuaSamKy.TrangThai == 1)
                    {
                        return new ErrorResponse
                        {
                            Message = "Chữ ký này đã được ký."
                        };
                    }
                }


                phieuMuaSamKy.NgayKy = DateTime.Now;
                phieuMuaSamKy.NguoiKyId = userId; //request.SignUserId;
                phieuMuaSamKy.UsbSerial = request.SignType;
                phieuMuaSamKy.TrangThai = Constants.Exceptions.Messages.KyCungUng.DaKy;

                await _muaSamPdxKyRepository.UpdateAsync(phieuMuaSamKy);

                switch (maDoiTuongKy)
                {
                    case MaDoiTuongKyConstants.NguoiLap:
                        phieuMuaSam.TrangThai = TrangThaiPhieu.TrongQuaTrinhKy;
                        break;
                    case MaDoiTuongKyConstants.TongGiamDoc:
                        phieuMuaSam.TrangThai = TrangThaiPhieu.TongGiamDoc;
                        break;

                    default:
                        phieuMuaSam.TrangThai = TrangThaiPhieu.TrongQuaTrinhKy;
                        break;
                }

                await _phieuKyRepository.UpdateAsync(phieuMuaSam);
                return new { PhieuId = request.PhieuId, Message = "Cập nhật thông tin thành công." };
            }
            catch (Exception ex)
            {
                // Ghi log chi tiết và trả về thông báo lỗi
                return new ErrorResponse
                {
                    Message = "Cập nhật thông tin không thành công.",
                    Details = ex.Message
                };
            }
        }



        #region ký simCA

        public async Task SignViettelCA(SignMobileCaInputDto input)
        {
            ParamDto param = BuildParamModel(input);
            ConfigDto config = GetViettelCaConfig();
            await _viettelCAService.SignMobileCA(param, config, 0, 2);
        }

        private ParamDto BuildParamModel(SignMobileCaInputDto input)
        {
            ParamDto param = new ParamDto
            {
                MobilePhone = input.MobilePhone,
                CertSerial = input.CertSerial,
                DataToDisplayed = input.DataToDisplayed,
                PdfPath = input.PdfPath,
                PdfPathSigned = input.PdfPathSigned,
                SignFileInfo = BuildSignViettelFileInfo(input.SignFileInfo)
            };

            return param;
        }

        private ISignFileDto BuildSignViettelFileInfo(DTOs.Viettel.SignFileImgDto? signFileInfo)
        {
            if (signFileInfo == null)
            {
                throw new ArgumentNullException(nameof(signFileInfo), "SignFileInfo cannot be null.");
            }

            MobileCA.Application.Services.Viettel.Dtos.SignFileImgDto signFile = new MobileCA.Application.Services.Viettel.Dtos.SignFileImgDto
            {
                numberPageSign = signFileInfo.numberPageSign,
                coorX = signFileInfo.coorX,
                coorY = signFileInfo.coorY,
                width = signFileInfo.width,
                height = signFileInfo.height,
                pathImage = signFileInfo.pathImage
            };
            return signFile;
        }
        private ConfigDto GetViettelCaConfig()
        {
            string ws = Constants.ViettelCa.ws;
            string apId = Constants.ViettelCa.ApId;
            string privateKey = Constants.ViettelCa.PrivateKey;

            ConfigDto item = new ConfigDto(ws, apId, privateKey);
            return item;
        }


        public async Task<object> UpdateKySimCaAsync(UpdateFileRequest request)
        {
            Serilog.Log.Information("Bắt đầu cập nhật trạng thái ký số - PhieuId: {PhieuId}, ChuKyId: {ChuKyId}",
        request.PhieuId, request.ChuKyId);
            
            var currentUser = await _nguoiDungService.GetCurrentUserAsync();
            var userId = currentUser.MaNguoiDung;
            
            using (var transaction = _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    // Lấy thông tin phiếu
                    var phieuMuaSam = await _phieuKyRepository.GetAsync(x => x.Id == request.PhieuId);
                    if (phieuMuaSam == null)
                    {
                        Serilog.Log.Warning("Không tìm thấy phiếu - PhieuId: {PhieuId}", request.PhieuId);
                        return new ErrorResponse { Message = $"Phiếu có Id: {request.PhieuId} không tồn tại." };
                    }

                    var phieuMuaSamKy = await _muaSamPdxKyRepository.GetAsync(x => x.Id == request.ChuKyId);
                    if (phieuMuaSamKy == null)
                    {
                        Serilog.Log.Warning("Không tìm thấy cấu hình chữ ký - ChuKyId: {ChuKyId}", request.ChuKyId);
                        return new ErrorResponse { Message = "Chưa có cấu hình chữ ký cho người ký này." };
                    }

                    if (phieuMuaSamKy.TrangThai == Constants.Exceptions.Messages.KyCungUng.DaKy)
                    {
                        Serilog.Log.Warning("Chữ ký đã được ký trước đó - ChuKyId: {ChuKyId}", request.ChuKyId);
                        return new ErrorResponse { Message = "Chữ ký này đã được ký." };
                    }
                    // var userId = _authorizedContextFacade.AccountId;
                    Serilog.Log.Information("Cập nhật thông tin ký - UserId: {UserId}, ChuKyId: {ChuKyId}",
               userId, request.ChuKyId);

                    // Cập nhật trạng thái chữ ký
                    phieuMuaSamKy.NgayKy = DateTime.Now;
                    phieuMuaSamKy.NguoiKyId = userId;
                    phieuMuaSamKy.UsbSerial = request.SignType;
                    phieuMuaSamKy.TrangThai = Constants.Exceptions.Messages.KyCungUng.DaKy;
                    await _muaSamPdxKyRepository.UpdateAsync(phieuMuaSamKy);

                    // Cập nhật trạng thái phiếu
                    phieuMuaSam.TrangThai = request.MaDoiTuongKy switch
                    {
                        MaDoiTuongKyConstants.NguoiLap => TrangThaiPhieu.TrongQuaTrinhKy,
                        MaDoiTuongKyConstants.TongGiamDoc => TrangThaiPhieu.TongGiamDoc,
                        _ => TrangThaiPhieu.TrongQuaTrinhKy
                    };
                    await _phieuKyRepository.UpdateAsync(phieuMuaSam);

                    Serilog.Log.Information(
                "Cập nhật trạng thái phiếu thành công - PhieuId: {PhieuId}, TrangThaiMoi: {TrangThai}",
                request.PhieuId, phieuMuaSam.TrangThai);

                    // Commit transaction nếu mọi thứ thành công
                    await _unitOfWork.CommitAsync();
                    return new { PhieuId = request.PhieuId, Message = "Cập nhật thông tin thành công." };
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();

                    Serilog.Log.Error(ex, "Lỗi khi cập nhật trạng thái ký số - PhieuId: {PhieuId}, ChuKyId: {ChuKyId}",
              request.PhieuId, request.ChuKyId);
                    // Log lỗi chi tiết
                    return new ErrorResponse
                    {
                        Message = "Cập nhật thông tin không thành công.",
                        Details = ex.Message
                    };
                }
            }
        }
        #endregion
        
        public async Task<int> CancelTicketAsync(int phieuId, bool isPhieuDeXuat, string? reason)
        {
            // Validate
            if (phieuId <= 0)
            {
                throw new BadRequestException(Constants.Exceptions.Messages.KyCungUng.InvalidTicketId);
            }
            var result = await _muaSamPhieuDeXuatRepository.GetAsync(x => x.Id == phieuId);
            if (result is null)
            {
                throw new NotFoundException(Constants.Exceptions.Messages.KyCungUng.InvalidPdx);
            }
            // Nếu huỷ phiếu đề xuất => người dùng phải tạo phiếu mới 
            // => trang thái phiếu cũ = huỷ đề xuất
            // => 
            if (isPhieuDeXuat)
            {
                
                result.TrangThai = (byte)SupplyTicketStatus.CancelledProposal;
            }
            else
            {
                result.TrangThai = (byte)SupplyTicketStatus.CancelledApproval;
                var listPhieuDeXuatKys = await _muaSamPdxKyRepository.ListAsync(x => x.PhieuDeXuatId == result.Id);
                if (listPhieuDeXuatKys.Any())
                {
                    foreach (var item in listPhieuDeXuatKys)
                    {
                        item.NguoiKyId= null;
                        item.LyDo = null;
                        item.TrangThai = null;
                    }
                }
            }

            result.GhiChu = reason;
            return await _muaSamPhieuDeXuatRepository.UpdateAsync(result);
        }

        public async Task<CheckedNumberAndSignImageResponse> CheckedNumberAndSignImageAsync(int phieuId, string acessToken)
        {   
            var response = new CheckedNumberAndSignImageResponse();
            
            // Lấy thông tin chữ ký của người dùng từ API => response : sdt, image path
            //var apiUrl = "https://app.tmhpp.com.vn/api/Admin/Profile";
            var apiUrl = "http://thacmo.e-solutions.com.vn/api/Admin/Profile";
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", acessToken);
            var apiThacMoResponse = await httpClient.GetAsync(apiUrl);
        
            if (!apiThacMoResponse.IsSuccessStatusCode)
            {
                throw new NotFoundException(Constants.Exceptions.Messages.KyCungUng.NotFoundSignInfo);
            }

            var jsonString = await apiThacMoResponse.Content.ReadAsStringAsync();
            var userSignatureInfo = JsonSerializer.Deserialize<EmployeeProfileOutputDto>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (userSignatureInfo is null)
            {
                return response;
            }
            response.ViettelSimCaMobilePhone =  userSignatureInfo.Result.ViettelSimCaMobilePhone?? string.Empty;
            if (userSignatureInfo?.Result.Signature is null)
            {
                return response;
            }
           
            // Lưu ảnh chữ ký vào thư mục images
            var imageBytes = Convert.FromBase64String(userSignatureInfo.Result.Signature.FileBytes);
            
            var rootPath = _imagePath.RootPath; 
            var relativeBasePath = _imagePath.RelativeBasePath; 
            var localBasePath = (rootPath + relativeBasePath).Replace("/", "\\"); 
            // nếu thư mục chứa ảnh chưa tồn tại => tạo thư mục theo Id => lưu ảnh
            var localFolder = Path.Combine(localBasePath);
            if (!Directory.Exists(localFolder))
            {
                Directory.CreateDirectory(localFolder);
            }
            var filePath = Path.Combine(localFolder, userSignatureInfo.Result.Signature.FileName);
            await File.WriteAllBytesAsync(filePath, imageBytes);
            response.PathImage = Path.Combine(_imagePath.RelativeBasePath, userSignatureInfo.Result.Signature.FileName)
                .Replace("\\", "/");
            return response;
        }

        public async Task<SignInfomationReponse> GetAsync(int phieuId)
        {
            var currentUser = await _nguoiDungService.GetCurrentUserAsync();
            
            // SignFileInfo
            var viTriCongViecHienTai = await _viTriCongViecRepository.GetAsync(x => x.Id == currentUser.ViTri);
            if (viTriCongViecHienTai is null)
            {                                                           
                return default;
            }
            var maDoiTuongKyHienTai = viTriCongViecHienTai.MaDoiTuongKy;
            // thông tin phiếu ký
            var phieuKy = await _muaSamPdxKyRepository.GetAsync(x => x.PhieuDeXuatId == phieuId && x.MaDoiTuongKy == maDoiTuongKyHienTai);
            if (phieuKy is null)
            {
                return default;
            }
            
            // SignFileImgDto 
            var response = new SignInfomationReponse();
            if (!string.IsNullOrWhiteSpace(phieuKy.ToaDo))
            {
                var toaDo = CalculateBottomCoordinatesFromString(phieuKy.ToaDo);
                response.SignFileInfo = new SignFileImgDto()
                {
                    numberPageSign = phieuKy.Page ?? 1,
                    coorX = toaDo.coorX,
                    coorY = toaDo.coorY,
                    width = toaDo.width,
                    height = toaDo.height,
                };

            };
            // ChuKyRequest
            response.ChuKyRequest = new ChuKyRequest
            {
                PhieuId = phieuId,
                VanBanId = phieuKy.VanBanId ?? 0,
                SignUserId = currentUser.MaNguoiDung,
                MaDoiTuongKy = phieuKy.MaDoiTuongKy,
                ThuTuKy = phieuKy.ThuTuKy ?? 0,
                ChuKyId = phieuKy.Id,
            };
            // Đường dẫn pdf trước khi ký
            var vanBanKyHienTai = await _vanBanKyRepository.GetAsync(x => x.Id == phieuKy.VanBanId);
            if (vanBanKyHienTai is not null && !string.IsNullOrWhiteSpace(vanBanKyHienTai.FilePath))
            {
                response.PdfPath =  vanBanKyHienTai.FilePath.Replace("KySo", "Storage_SignedFile");;
                response.PdfPathSigned = response.PdfPath;
            }
            var listPaths = await _vanBanKyRepository.ListVanbanKyUrlAsync(phieuId);
            
            var listPathsResponse = new List<string>();
            if (listPaths.Any())
            {
                foreach (var item in listPaths)
                {
                    var path = GetRelativePathVer2(item);
                    listPathsResponse.Add(path);
                }
            }
            response.ListFullPaths = listPathsResponse;
            
            // SignTicketResponseItems
            var listSignHistoryResponse = (await _muaSamPdxKyRepository.GetSignHistoryAsync(phieuId)).Adapt<IEnumerable<SignHistoryResponseItem>>().ToList();
            if (!listSignHistoryResponse.Any())
            {
                return response;
            }
            response.SignTicketResponseItems = listSignHistoryResponse;

            if (maDoiTuongKyHienTai == Constants.MaDoiTuongKy.Ph_KHVT || maDoiTuongKyHienTai == Constants.MaDoiTuongKy.TongGiamDoc)
            {
                response.IsHuyDuyet = true;
                response.IsHuyDeXuat = true;
            }
            return response;
        }


        private SignFileImgDto CalculateBottomCoordinatesFromString(string coordinateString)
        {
            if (string.IsNullOrWhiteSpace(coordinateString))
            {
                return default;
            }
            var toaDoArray = coordinateString.Split(',').Select(value => float.Parse(value.Trim())).ToArray();
            if (toaDoArray.Length != 4 || toaDoArray.Any(float.IsNaN)) {
                Console.Error.WriteLine("Chuỗi tọa độ không đủ hoặc chứa giá trị không hợp lệ.");
                return default;
            }

            // Gán giá trị từ mảng
            var x = toaDoArray[0];
            var y = toaDoArray[1];
            var width = toaDoArray[2];
            var height = toaDoArray[3];

            // Tính toán tọa độ
            var bottomLeftX = Math.Round(x) + 28; // Lề bên trái
            var bottomLeftY = Math.Round(y) + 8; // Khoảng cách trên

            var bottomW = Math.Round(width) - 15; // Thu nhỏ lại để nằm trong khung
            var bottomH = Math.Round(height) - 16; // Thu nhỏ lại để nằm trong khung

            return new SignFileImgDto
            {
                coorX = (float)bottomLeftX,
                coorY = (float)bottomLeftY,
                width = (float)bottomW,
                height = (float)bottomH
            };
        }
        private string GetRelativePathVer2(string filePath)
        {
           //var kySoPath = _kySoPathVersion2.RootPath;
            var relativeBasePath = _kySoPathVersion2.RelativeBasePath;

            if (string.IsNullOrEmpty(relativeBasePath))
            {
                throw new Exception("RootPath hoặc RelativeBasePath không được cấu hình đúng.");
            }

            // Kiểm tra nếu filePath bắt đầu bằng "KySo". Phần này do db thiết kế cũ lởm nên phải replace
            if (filePath.StartsWith("KySo", StringComparison.OrdinalIgnoreCase))
            {
                filePath = filePath.Replace("KySo", "Storage_SignedFile");
            }
            
            // Chuẩn hóa đường dẫn thành backslash cho Windows
            return Path.Combine(relativeBasePath, filePath).Replace("\\","/");
        }
    }
}

