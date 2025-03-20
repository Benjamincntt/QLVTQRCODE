using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using ESPlatform.QRCode.IMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories
{
    public class MuaSamPdxKyRepository: EfCoreRepositoryBase<QlvtMuaSamPdxKy, AppDbContext>, IMuaSamPdxKyRepository
    {
        public MuaSamPdxKyRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        // Lấy những phiếu chưa ký theo MaDoiTuongKy(1-6) hiện tại
        public async Task<IEnumerable<int>> ListPhieuDeXuatIdsAsync(string maDoiTuongKy)
        {
            var query = DbContext.QlvtMuaSamPdxKies
                .Where(x => x.PhieuDeXuatId != null)
                .Where(x => x.MaDoiTuongKy == maDoiTuongKy)
                .Select(x => x.PhieuDeXuatId.Value); 
            return await query.ToListAsync();
        }
        // Các phiếu của người trước ký phải có trạng thái chữ ký đã ký hoặc bỏ qua
        public async Task<IEnumerable<int>> ListPhieuDeXuatOtherIdsAsync(string previousMaDoiTuongKy, List<int> listPhieuDeXuatIds)
        {
            var query = DbContext.QlvtMuaSamPdxKies
                .Where(x => x.PhieuDeXuatId != null)
                .Where(x => x.MaDoiTuongKy == previousMaDoiTuongKy)
                .Where(x => x.TrangThai >= (byte)TrangThaiChuKy.Signed)
                .Where(x => x.PhieuDeXuatId.HasValue && listPhieuDeXuatIds.Contains(x.PhieuDeXuatId.Value))
                .Select(x => x.PhieuDeXuatId.Value); 
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> GetSignHistoryAsync(int phieuId)
        {
            var query = DbContext.QlvtMuaSamPdxKies
                .GroupJoin( DbContext.TbNguoiDungs,
                        x => x.NguoiKyId,
                        y => y.MaNguoiDung,
                        (x, y) => new { QlvtMuaSamPdxKies = x, TbNguoiDungs = y })
                .SelectMany
                        (x => x.TbNguoiDungs.DefaultIfEmpty(),
                        (x, y) => new { x.QlvtMuaSamPdxKies, TbNguoiDungs = y }) 
                .Join(DbContext.TbViTriCongViecs,
                    x => x.TbNguoiDungs.ViTri,
                    y => y.Id,
                    (x, y) => new { x.QlvtMuaSamPdxKies, x.TbNguoiDungs, TbViTriCongViecs = y })
                .Where(x => x.QlvtMuaSamPdxKies.PhieuDeXuatId == phieuId)
                .Where(x => x.QlvtMuaSamPdxKies.TrangThai >= (byte)TrangThaiChuKy.Signed)
                .Select(x => new
                {
                    UserName = x.TbNguoiDungs.Ho + " " + x.TbNguoiDungs.Ten,
                    x.TbViTriCongViecs.TenViTriCongViec,
                    x.QlvtMuaSamPdxKies.NgayKy,
                    x.QlvtMuaSamPdxKies.TrangThai,
                });
            return await query.ToListAsync();
        }
    }
}
