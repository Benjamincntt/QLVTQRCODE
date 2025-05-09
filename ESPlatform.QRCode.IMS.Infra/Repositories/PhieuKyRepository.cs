﻿using Azure.Core;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Infra.Repositories
{
    public class PhieuKyRepository : EfCoreRepositoryBase<QlvtMuaSamPhieuDeXuat, AppDbContext>, IPhieuKyRepository
    {
        private readonly AppDbContext _dbContext;
        public PhieuKyRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<dynamic>> DanhSachPhieuDeXuatKy(DanhSachPhieuKyFilter requests, int userId)
        {
            var keywords = string.IsNullOrWhiteSpace(requests.Keywords) ? string.Empty : requests.Keywords.ToLower();
            DateTime fromDate = DateTime.Now.AddDays(-1); // Mặc định là ngày hiện tại
            DateTime toDate = DateTime.Now.AddDays(1); // Mặc định là ngày hiện tại + 1 ngày

            if (!string.IsNullOrWhiteSpace(requests.FromDate))
            {
                fromDate = DateTime.ParseExact(requests.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(requests.ToDate))
            {
                toDate = DateTime.ParseExact(requests.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1); // Cộng thêm 1 ngày
            }
            // lấy MaDoiTuongKy trong bảng Tb_ViTriCongViec
            var viTriMaDoiTuongKy = GetViTriVaMaDoiTuongKy(userId);
            string? MaDoiTuongKy = "";
             MaDoiTuongKy = viTriMaDoiTuongKy.FirstOrDefault()?.MaDoiTuongKy;
            var dsCauHinhVbKy = GetCauHinhVbKy();

            // Kiểm tra nếu MaDoiTuongKy tồn tại trong dsCauHinhVbKy
            var matchingCauHinh = dsCauHinhVbKy.FirstOrDefault(c => c.MaDoiTuongKy == MaDoiTuongKy);
            var query = DbContext.QlvtMuaSamPhieuDeXuats.AsQueryable();

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query.Where(p =>
                    (p.TenPhieu != null && p.TenPhieu.Contains(keywords)) ||
                    (p.MaPhieu != null && p.MaPhieu.Contains(keywords))
                );
            }
            if(fromDate != DateTime.MinValue && toDate != DateTime.MaxValue)
            {
                if(fromDate <= toDate) {
                    query = query.Where(p => p.NgayThem >= fromDate && p.NgayThem < toDate);
                }
            }

            List<int> phieuDeXuatIds = new List<int>();

            phieuDeXuatIds = GetPhieuDeXuatIds(MaDoiTuongKy);


            List<int?> phieuDeXuatOtherIds = new List<int?>();
            if (!phieuDeXuatIds.Any())
            {
                return Enumerable.Empty<dynamic>();
            }


            if (matchingCauHinh != null)
            {
                switch (MaDoiTuongKy)
                {
                    case MaDoiTuongKyConstants.NguoiLap:
                        query = query.Where(x => x.MaNguoiThem == userId && x.TrangThai == 1);

                        break;
                    case MaDoiTuongKyConstants.KiemSoatAT:
                    case MaDoiTuongKyConstants.TruongDonVi:
                    case MaDoiTuongKyConstants.Ph_KTAT:
                    case MaDoiTuongKyConstants.Ph_KHVT:
                    case MaDoiTuongKyConstants.TongGiamDoc:
                        var previousMaDoiTuongKy = MaDoiTuongKy switch
                        {
                            MaDoiTuongKyConstants.KiemSoatAT => MaDoiTuongKyConstants.NguoiLap, 
                            MaDoiTuongKyConstants.TruongDonVi => MaDoiTuongKyConstants.KiemSoatAT,
                            MaDoiTuongKyConstants.Ph_KTAT => MaDoiTuongKyConstants.TruongDonVi,
                            MaDoiTuongKyConstants.Ph_KHVT => MaDoiTuongKyConstants.Ph_KTAT,
                            MaDoiTuongKyConstants.TongGiamDoc => MaDoiTuongKyConstants.Ph_KHVT,
                            _ => null
                        };

                        if (previousMaDoiTuongKy != null)
                        {
                            phieuDeXuatOtherIds = await DbContext.QlvtMuaSamPdxKies
                                .Where(k => k.MaDoiTuongKy == previousMaDoiTuongKy && k.TrangThai >= 1 && k.PhieuDeXuatId.HasValue
                                                                                                       && phieuDeXuatIds.Contains(k.PhieuDeXuatId.Value))
                                .Select(k => k.PhieuDeXuatId)
                                .ToListAsync();

                            if (!phieuDeXuatOtherIds.Any())
                            {
                                return Enumerable.Empty<dynamic>();
                            }
                        }
                        
                        break;

                    //case MaDoiTuongKyConstants.Ph_KTAT:
                    //    // Lấy số lượng chữ ký từ cấu hình
                    //    var slChuKyDx = await DbContext.QlvtCauHinhVbKies
                    //        .Where(vb => vb.MaLoaiVanBan == LoaiPhieu.PhieuDeXuat)
                    //        .Select(vb => vb.SoLuongChuKy)
                    //        .FirstOrDefaultAsync();

                    //    // Kiểm tra số lượng chữ ký trong bảng QLVT_MuaSam_PDX_Ky
                    //    var countKy = await (from k in DbContext.QlvtMuaSamPdxKies
                    //                         join vb in DbContext.QlvtVanBanKies
                    //                         on k.VanBanId equals vb.Id
                    //                         where (vb.MaLoaiVanBan == LoaiPhieu.PhieuDeXuat && k.TrangThai >= 1)
                    //                         select k)
                    //                        .CountAsync();

                    //    if (slChuKyDx == countKy)
                    //    {

                    //        phieuDeXuatOtherIds = await DbContext.QlvtMuaSamPdxKies
                    //       .Where(k => k.MaDoiTuongKy == MaDoiTuongKyConstants.Ph_KTAT && k.TrangThai == null && k.PhieuDeXuatId.HasValue
                    //                                                                              && phieuDeXuatIds.Contains(k.PhieuDeXuatId.Value))
                    //       .Select(k => k.PhieuDeXuatId)
                    //       .ToListAsync();

                    //        if (!phieuDeXuatOtherIds.Any())
                    //        {
                    //            return Enumerable.Empty<dynamic>();
                    //        }

                    //    }
                    //    else
                    //    {
                    //        return Enumerable.Empty<dynamic>();
                    //    }
                    //    break;
                    default:
                        break;
                }
            }

            if (phieuDeXuatOtherIds.Any())
            {
                phieuDeXuatOtherIds = phieuDeXuatOtherIds.Where(id => id.HasValue).ToList();
                if (phieuDeXuatOtherIds != null && phieuDeXuatOtherIds.Any())
                {
                    query = query.Where(x => phieuDeXuatOtherIds.Contains(x.Id));
                }
            }

            // Xác định giá trị của MaLoaiVanBan dựa trên MaDoiTuongKyConstants
            string maLoaiVanBan = MaDoiTuongKy switch
            {
                MaDoiTuongKyConstants.NguoiLap or
                MaDoiTuongKyConstants.KiemSoatAT or
                MaDoiTuongKyConstants.TruongDonVi => LoaiPhieu.PhieuDeXuat,

                MaDoiTuongKyConstants.Ph_KHVT or
                MaDoiTuongKyConstants.Ph_KTAT or
                MaDoiTuongKyConstants.TongGiamDoc => LoaiPhieu.PhieuDuyet,

                _ => throw new ArgumentException("Mã đối tượng kỳ không hợp lệ")
            };

            // Sắp xếp và chọn dữ liệu


           
            var result = await (
                                from p in query
                                join v in DbContext.QlvtVanBanKies.Where(v => v.MaLoaiVanBan == maLoaiVanBan)
                                    on p.Id equals v.PhieuId


                                join k in DbContext.QlvtMuaSamPdxKies.Where(k => k.MaDoiTuongKy == MaDoiTuongKy)
                                     on new { PhieuDeXuatId = (int?)p.Id, VanBan_Id = (int?)v.Id }
                                     equals new { PhieuDeXuatId = (int?)k.PhieuDeXuatId, VanBan_Id = (int?)k.VanBanId }

                                     // Join với bảng người dùng
                                join nd in DbContext.TbNguoiDungs
                                    on p.MaNguoiThem equals nd.MaNguoiDung

                                // Join với bảng đơn vị sử dụng
                                join dv in DbContext.TbDonViSuDungs
                                    on nd.MaDonViSuDung equals dv.MaDonViSuDung

                                orderby p.NgayThem descending, p.TenPhieu
                                select new PhieuKyModel
                                {
                                    Id = p.Id,
                                    MaPhieu = p.MaPhieu,
                                    TenPhieu = p.TenPhieu,
                                    MoTa = p.MoTa,
                                    MaDonViSuDung = p.MaDonViSuDung,
                                    NgayThem = p.NgayThem,
                                    MaNguoiThem = p.MaNguoiThem,
                                    TenDonViSuDung = dv.TenDonViSuDung,
                                    TenNguoiThem = nd.Ho + " " + nd.Ten,
                                    TrangThai = p.TrangThai ?? 0,
                                    VanBanKy = new VanBanKyModel
                                    {
                                        Id = v.Id,
                                        TrangThaiTaoFile = v.TrangThaiTaoFile,
                                        MaLoaiVanBan = v.MaLoaiVanBan,
                                        FileName = v.FileName,
                                        FilePath = v.FilePath,
                                    },
                                    ChuKy = new ChuKyModel
                                    {
                                        ChuKyId = k.Id,
                                        ToaDo = k.ToaDo,
                                        MaDoiTuongKy = k.MaDoiTuongKy,
                                        NgayKy = k.NgayKy,
                                        ThuTuKy = k.ThuTuKy,
                                        VanBanId= k.VanBanId,
                                        page = k.Page,
                                        pageHeight = (float?)k.PageHeight,
                                        TrangThai = k.TrangThai ?? 0,
                                    }
                                })
                                .ToListAsync();
            return result;

        }

        public List<dynamic> GetViTriVaMaDoiTuongKy(int userId)
        {
            var query = from v in DbContext.TbViTriCongViecs
                        join u in DbContext.TbNguoiDungs on v.Id equals u.ViTri
                        where u.ViTri > 0 && u.MaNguoiDung == userId
                        select new
                        {
                            v.TenViTriCongViec,
                            v.MaDoiTuongKy
                        };

            return query.AsNoTracking().ToList<dynamic>(); // Hoặc sử dụng List<object>
        }

        public List<dynamic> GetCauHinhVbKy()
        {
            var query = from c in DbContext.QlvtCauHinhVbKies
                        select new
                        {
                            c.Id,
                            c.Stt,
                            c.MaDoiTuongKy,
                            c.MaLoaiVanBan,
                            c.CoTheBoQua,
                        };

            return query.AsNoTracking().ToList<dynamic>(); // Hoặc sử dụng List<object>
        }

        public List<dynamic> GetDanhSachPhieuDx(string MaDoiTuongKy)
        {
            var query = from c in DbContext.QlvtMuaSamPdxKies
                        .Where(k => (k.TrangThai == null || k.TrangThai != 1) && k.MaDoiTuongKy == MaDoiTuongKy) // Đảm bảo điều kiện đúng
                        select new
                        {
                            c.Id,
                            c.ThuTuKy,
                            c.PhieuDeXuatId,
                            c.MaDoiTuongKy,
                            c.TrangThai,
                        };

            return query.AsNoTracking().ToList<dynamic>(); // Hoặc sử dụng List<object>
        }

        public List<int> GetPhieuDeXuatIds(string MaDoiTuongKy)
        {
            List<int> phieuDeXuatIds = DbContext.QlvtMuaSamPdxKies
                .Where(k => (k.TrangThai == null) && k.MaDoiTuongKy == MaDoiTuongKy)
                .Select(k => k.PhieuDeXuatId.Value) // Lấy Id của các phiếu đề xuất
                .ToList(); // Chuyển đổi thành danh sách

            return phieuDeXuatIds;
        }

    }
}
