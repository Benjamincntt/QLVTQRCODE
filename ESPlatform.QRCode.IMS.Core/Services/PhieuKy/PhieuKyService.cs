using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Infra.Repositories;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Mapster;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.Services.PhieuKy
{
    public class PhieuKyService : IPhieuKyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhieuKyRepository _phieuKyRepository;
        private readonly IMuaSamPdxKyRepository _deXuatKyRepository;
        private readonly ICauHinhVanBanKyRepository _cauHinhVanBanKyRepository;
        private readonly IVanBanKyRepository _vanBanKyRepository;
        private readonly IAuthorizedContextFacade _authorizedContextFacade;
        private readonly IConfiguration _configuration;
        
        public PhieuKyService(IPhieuKyRepository phieuKyRepository, IAuthorizedContextFacade authorizedContextFacade
                , IMuaSamPdxKyRepository deXuatKyRepository
                , ICauHinhVanBanKyRepository cauHinhVanBanKyRepository
                , IVanBanKyRepository vanBanKyRepository
                , IConfiguration configuration
                , IUnitOfWork unitOfWork
               )
        {
            _phieuKyRepository = phieuKyRepository;
            _authorizedContextFacade = authorizedContextFacade;
            _unitOfWork = unitOfWork;
            _deXuatKyRepository = deXuatKyRepository;
            _cauHinhVanBanKyRepository = cauHinhVanBanKyRepository;
            _vanBanKyRepository = vanBanKyRepository;
            _configuration = configuration;

        }

        public async Task<List<PhieuKyModel>> GetDanhSachPhieuKyAsync(DanhSachPhieuKyFilter requests)
        {

            var userId = _authorizedContextFacade.AccountId;

            var lstPhieuKy = (await _phieuKyRepository.DanhSachPhieuDeXuatKy(requests, userId))
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
                throw new BadRequestException(Constants.Exceptions.Messages.KyCungUng.EmptyPhieuIds);
            }

            var userId = _authorizedContextFacade.AccountId;
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
                        var phieuMuaSamKy = await _deXuatKyRepository.GetAsync(x => x.Id == phieuKy.ChuKyId
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
                        await _deXuatKyRepository.UpdateManyAsync(lstMuaSamPdxKy);
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
            var userId = _authorizedContextFacade.AccountId;
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

                    var phieuMuaSamKy = await _deXuatKyRepository.GetAsync(x => x.Id == request.ChuKyId
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

                    await _deXuatKyRepository.UpdateAsync(phieuMuaSamKy);

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

                    var phieuMuaSamKy = await _deXuatKyRepository.GetAsync(x => x.Id == request.ChuKyId);

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
                    await _deXuatKyRepository.UpdateAsync(phieuMuaSamKy);

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
                var phieuMuaSamKy = await _deXuatKyRepository.GetAsync(x => x.Id == request.ChuKyId);

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
                phieuMuaSamKy.NguoiKyId = request.SignUserId;
                phieuMuaSamKy.UsbSerial = request.SignType;
                phieuMuaSamKy.TrangThai = Constants.Exceptions.Messages.KyCungUng.DaKy;

                await _deXuatKyRepository.UpdateAsync(phieuMuaSamKy);

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

    }
}

