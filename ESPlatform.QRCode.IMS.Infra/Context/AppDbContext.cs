using ESPlatform.QRCode.IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DonViHanhChinh> DonViHanhChinhs { get; set; }

    public virtual DbSet<GetNewId> GetNewIds { get; set; }

    public virtual DbSet<QlvtCauHinhVbKy> QlvtCauHinhVbKies { get; set; }

    public virtual DbSet<QlvtGioHang> QlvtGioHangs { get; set; }

    public virtual DbSet<QlvtKho> QlvtKhos { get; set; }

    public virtual DbSet<QlvtKhoPhu> QlvtKhoPhus { get; set; }

    public virtual DbSet<QlvtKyKiemKe> QlvtKyKiemKes { get; set; }

    public virtual DbSet<QlvtKyKiemKeBackup> QlvtKyKiemKeBackups { get; set; }

    public virtual DbSet<QlvtKyKiemKeChiTiet> QlvtKyKiemKeChiTiets { get; set; }

    public virtual DbSet<QlvtKyKiemKeChiTietBackup> QlvtKyKiemKeChiTietBackups { get; set; }

    public virtual DbSet<QlvtKyKiemKeChiTietDff> QlvtKyKiemKeChiTietDffs { get; set; }

    public virtual DbSet<QlvtKyKiemKeChiTietDffBackup> QlvtKyKiemKeChiTietDffBackups { get; set; }

    public virtual DbSet<QlvtKyKiemKeChiTietErpDemo> QlvtKyKiemKeChiTietErpDemos { get; set; }

    public virtual DbSet<QlvtKyKiemKeErpDemo> QlvtKyKiemKeErpDemos { get; set; }

    public virtual DbSet<QlvtMuaSamPdxKy> QlvtMuaSamPdxKies { get; set; }

    public virtual DbSet<QlvtMuaSamPhieuDeXuat> QlvtMuaSamPhieuDeXuats { get; set; }

    public virtual DbSet<QlvtMuaSamPhieuDeXuatDetail> QlvtMuaSamPhieuDeXuatDetails { get; set; }

    public virtual DbSet<QlvtMuaSamPhieuDeXuatLichSu> QlvtMuaSamPhieuDeXuatLichSus { get; set; }

    public virtual DbSet<QlvtMuaSamVatTuNew> QlvtMuaSamVatTuNews { get; set; }

    public virtual DbSet<QlvtVanBanKy> QlvtVanBanKies { get; set; }

    public virtual DbSet<QlvtVatTu> QlvtVatTus { get; set; }

    public virtual DbSet<QlvtVatTuBoMa> QlvtVatTuBoMas { get; set; }

    public virtual DbSet<QlvtVatTuTonKho> QlvtVatTuTonKhos { get; set; }

    public virtual DbSet<QlvtVatTuTonKhoDinhMuc> QlvtVatTuTonKhoDinhMucs { get; set; }

    public virtual DbSet<QlvtVatTuViTri> QlvtVatTuViTris { get; set; }

    public virtual DbSet<QlvtViTri> QlvtViTris { get; set; }

    public virtual DbSet<TbAudience> TbAudiences { get; set; }

    public virtual DbSet<TbCaiDatHeThong> TbCaiDatHeThongs { get; set; }

    public virtual DbSet<TbCauHinhDieuHuongModun> TbCauHinhDieuHuongModuns { get; set; }

    public virtual DbSet<TbCauHinhTrangThai> TbCauHinhTrangThais { get; set; }

    public virtual DbSet<TbChucNang> TbChucNangs { get; set; }

    public virtual DbSet<TbCoCauToChuc> TbCoCauToChucs { get; set; }

    public virtual DbSet<TbDieuHuongModule> TbDieuHuongModules { get; set; }

    public virtual DbSet<TbDonViSuDung> TbDonViSuDungs { get; set; }

    public virtual DbSet<TbDonViSuDungModun> TbDonViSuDungModuns { get; set; }

    public virtual DbSet<TbHinhNenVanBang> TbHinhNenVanBangs { get; set; }

    public virtual DbSet<TbHopThuDen> TbHopThuDens { get; set; }

    public virtual DbSet<TbItem> TbItems { get; set; }

    public virtual DbSet<TbItemMacDinh> TbItemMacDinhs { get; set; }

    public virtual DbSet<TbKichBanCapNhatHeThong> TbKichBanCapNhatHeThongs { get; set; }

    public virtual DbSet<TbKieuDangKyNd> TbKieuDangKyNds { get; set; }

    public virtual DbSet<TbKieuEmailAndMessage> TbKieuEmailAndMessages { get; set; }

    public virtual DbSet<TbKieuEmailAndMessageDonViSuDung> TbKieuEmailAndMessageDonViSuDungs { get; set; }

    public virtual DbSet<TbKieuNguoiDung> TbKieuNguoiDungs { get; set; }

    public virtual DbSet<TbKieuNguoiDungChucNangLoaiKieuNguoiDung> TbKieuNguoiDungChucNangLoaiKieuNguoiDungs { get; set; }

    public virtual DbSet<TbKieuNguoiDungModunLoaiKieuNguoiDung> TbKieuNguoiDungModunLoaiKieuNguoiDungs { get; set; }

    public virtual DbSet<TbKieuNoiDung> TbKieuNoiDungs { get; set; }

    public virtual DbSet<TbLoaiKieuNguoiDung> TbLoaiKieuNguoiDungs { get; set; }

    public virtual DbSet<TbLoaiTepTin> TbLoaiTepTins { get; set; }

    public virtual DbSet<TbLsNguoiDungCapNhatNguoiDung> TbLsNguoiDungCapNhatNguoiDungs { get; set; }

    public virtual DbSet<TbLsNguoiDungCoCauToChuc> TbLsNguoiDungCoCauToChucs { get; set; }

    public virtual DbSet<TbLsNguoiDungDonViSuDung> TbLsNguoiDungDonViSuDungs { get; set; }

    public virtual DbSet<TbLsNguoiDungTaoNguoiDung> TbLsNguoiDungTaoNguoiDungs { get; set; }

    public virtual DbSet<TbLsNguoiDungXoaModun> TbLsNguoiDungXoaModuns { get; set; }

    public virtual DbSet<TbLsNguoiDungXoaNguoiDung> TbLsNguoiDungXoaNguoiDungs { get; set; }

    public virtual DbSet<TbMayChuServerFile> TbMayChuServerFiles { get; set; }

    public virtual DbSet<TbModun> TbModuns { get; set; }

    public virtual DbSet<TbModunChaCon> TbModunChaCons { get; set; }

    public virtual DbSet<TbNgonNgu> TbNgonNgus { get; set; }

    public virtual DbSet<TbNguoiDung> TbNguoiDungs { get; set; }

    public virtual DbSet<TbNguoiDungCoCauToChucO> TbNguoiDungCoCauToChucOes { get; set; }

    public virtual DbSet<TbNhomModun> TbNhomModuns { get; set; }

    public virtual DbSet<TbPhanQuyenModun> TbPhanQuyenModuns { get; set; }

    public virtual DbSet<TbTemplate> TbTemplates { get; set; }

    public virtual DbSet<TbTepTin> TbTepTins { get; set; }

    public virtual DbSet<TbTheme> TbThemes { get; set; }

    public virtual DbSet<TbThemeMacDinh> TbThemeMacDinhs { get; set; }

    public virtual DbSet<TbThongTin> TbThongTins { get; set; }

    public virtual DbSet<TbThungRac> TbThungRacs { get; set; }

    public virtual DbSet<TbTimeZone> TbTimeZones { get; set; }

    public virtual DbSet<TbTinNhan> TbTinNhans { get; set; }

    public virtual DbSet<TbTinNhanTepTin> TbTinNhanTepTins { get; set; }

    public virtual DbSet<TbViTriCongViec> TbViTriCongViecs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DonViHanhChinh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EDM_DonViHanhChinh");

            entity.ToTable("DonViHanhChinh");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cap).HasComment("0: quốc gia, 1: tinh, 2: quan, 3: phuong, 4: phố, 5: tổ");
            entity.Property(e => e.MaDiaChi).HasMaxLength(255);
            entity.Property(e => e.NgaySua).HasMaxLength(50);
            entity.Property(e => e.NgayTao).HasMaxLength(50);
            entity.Property(e => e.TenDiaChi).HasMaxLength(255);
        });

        modelBuilder.Entity<GetNewId>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Get_NewID");

            entity.Property(e => e.MyNewId).HasColumnName("MyNewID");
        });

        modelBuilder.Entity<QlvtCauHinhVbKy>(entity =>
        {
            entity.ToTable("QLVT_CauHinh_Vb_Ky");

            entity.Property(e => e.CoTheBoQua).HasColumnName("CoThe_BoQua");
            entity.Property(e => e.IdDetect)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaDoiTuongKy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaLoaiVanBan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Stt).HasColumnName("STT");
            entity.Property(e => e.TypeSign)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("typeSign");
        });

        modelBuilder.Entity<QlvtGioHang>(entity =>
        {
            entity.HasKey(e => e.GioHangId);

            entity.ToTable("QLVT_GioHang");

            entity.Property(e => e.GioHangId).HasColumnName("GioHang_Id");
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.IsSystemSupply)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.SoLuong).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ThoiGianCapNhat).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao).HasColumnType("datetime");
            entity.Property(e => e.ThongSoKyThuat).HasComment("Thông số kỹ thuật");
            entity.Property(e => e.VatTuId).HasColumnName("VatTu_Id");
        });

        modelBuilder.Entity<QlvtKho>(entity =>
        {
            entity.HasKey(e => e.OrganizationId);

            entity.ToTable("QLVT_Kho");

            entity.Property(e => e.OrganizationId)
                .ValueGeneratedNever()
                .HasColumnName("Organization_Id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Người tạo")
                .HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasComment("Creation_Date")
                .HasColumnType("date")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.MasterOrganizationId)
                .HasDefaultValueSql("((0))")
                .HasComment("Id Tổng kho")
                .HasColumnName("Master_Organization_Id");
            entity.Property(e => e.OperatingUnit)
                .HasComment("Id đơn vị")
                .HasColumnName("Operating_Unit");
            entity.Property(e => e.OrganizationCode)
                .HasMaxLength(50)
                .HasComment("Mã Kho")
                .HasColumnName("Organization_Code");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(250)
                .HasComment("Tên Kho")
                .HasColumnName("Organization_Name");
            entity.Property(e => e.OrganizationType)
                .HasMaxLength(50)
                .HasComment("Loại kho")
                .HasColumnName("Organization_Type");
            entity.Property(e => e.PrimaryCostMethod)
                .HasMaxLength(50)
                .HasComment("Phương pháp tính giá")
                .HasColumnName("Primary_Cost_Method");
            entity.Property(e => e.SubInventoryCode)
                .HasMaxLength(50)
                .HasComment("Mã kho phụ")
                .HasColumnName("Subinventory_Code");
            entity.Property(e => e.SubInventoryName)
                .HasMaxLength(250)
                .HasComment("Tên kho phụ")
                .HasColumnName("Subinventory_Name");
        });

        modelBuilder.Entity<QlvtKhoPhu>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("QLVT_KhoPhu");

            entity.Property(e => e.GhiChu).HasMaxLength(1000);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.SubinventoryCode)
                .HasMaxLength(50)
                .HasColumnName("Subinventory_Code");
            entity.Property(e => e.SubinventoryName)
                .HasMaxLength(250)
                .HasColumnName("Subinventory_Name");
        });

        modelBuilder.Entity<QlvtKyKiemKe>(entity =>
        {
            entity.ToTable("QLVT_KyKiemKe");

            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasComment("Diễn giải")
                .HasColumnName("description");
            entity.Property(e => e.EndTag)
                .HasMaxLength(50)
                .HasComment("Số thẻ kết thúc")
                .HasColumnName("end_tag");
            entity.Property(e => e.FreezeDate)
                .HasComment("Ngày khó")
                .HasColumnName("freeze_date");
            entity.Property(e => e.OrganizationCode)
                .HasMaxLength(50)
                .HasComment("Mã kho")
                .HasColumnName("organization_code");
            entity.Property(e => e.OrganizationId)
                .HasComment("Id kho")
                .HasColumnName("organization_id");
            entity.Property(e => e.PhysicalInventoryDate)
                .HasComment("Ngày kiểm kê")
                .HasColumnName("physical_inventory_date");
            entity.Property(e => e.PhysicalInventoryId)
                .HasComment("Id kỳ kiểm kê")
                .HasColumnName("physical_inventory_id");
            entity.Property(e => e.PhysicalInventoryName)
                .HasMaxLength(250)
                .HasComment("Tên kỳ kiểm kê")
                .HasColumnName("physical_inventory_name");
            entity.Property(e => e.StartTag)
                .HasMaxLength(50)
                .HasComment("Số thẻ đầu tiên")
                .HasColumnName("start_tag");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasComment("Người tạo")
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<QlvtKyKiemKeBackup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QLVT_KyK__3214EC0782418870");

            entity.ToTable("QLVT_KyKiemKe_Backup");

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.EndTag)
                .HasMaxLength(255)
                .HasColumnName("end_tag");
            entity.Property(e => e.FreezeDate)
                .HasColumnType("datetime")
                .HasColumnName("freeze_date");
            entity.Property(e => e.KiemKeIdGoc).HasColumnName("KiemKe_Id_Goc");
            entity.Property(e => e.Kykiemkechinh).HasColumnName("kykiemkechinh");
            entity.Property(e => e.NgaySaoLuu).HasColumnType("datetime");
            entity.Property(e => e.OrganizationCode)
                .HasMaxLength(255)
                .HasColumnName("organization_code");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.PhysicalInventoryDate)
                .HasColumnType("datetime")
                .HasColumnName("physical_inventory_date");
            entity.Property(e => e.PhysicalInventoryId).HasColumnName("physical_inventory_id");
            entity.Property(e => e.PhysicalInventoryName)
                .HasMaxLength(255)
                .HasColumnName("physical_inventory_name");
            entity.Property(e => e.StartTag)
                .HasMaxLength(255)
                .HasColumnName("start_tag");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<QlvtKyKiemKeChiTiet>(entity =>
        {
            entity.HasKey(e => e.KyKiemKeChiTietId).HasName("PK_QLVT_TheKiemKe");

            entity.ToTable("QLVT_KyKiemKe_ChiTiet");

            entity.Property(e => e.KyKiemKeChiTietId).HasColumnName("KyKiemKe_ChiTiet_Id");
            entity.Property(e => e.KhoChinhId).HasColumnName("KhoChinh_Id");
            entity.Property(e => e.KhoPhuId).HasColumnName("KhoPhu_Id");
            entity.Property(e => e.KyKiemKeId).HasColumnName("KyKiemKe_Id");
            entity.Property(e => e.NgayKiemKe).HasColumnType("datetime");
            entity.Property(e => e.NguoiKiemKeId).HasColumnName("NguoiKiemKe_Id");
            entity.Property(e => e.NguoiKiemKeTen)
                .HasMaxLength(50)
                .HasColumnName("NguoiKiemKe_Ten");
            entity.Property(e => e.SoLuongChenhLech).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SoLuongKiemKe).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SoLuongSoSach).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SoThe)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VatTuId).HasColumnName("VatTu_Id");

            entity.HasOne(d => d.KyKiemKe).WithMany(p => p.QlvtKyKiemKeChiTiets)
                .HasForeignKey(d => d.KyKiemKeId)
                .HasConstraintName("FK_QLVT_KyKiemKe_ChiTiet_QLVT_KyKiemKe1");
        });

        modelBuilder.Entity<QlvtKyKiemKeChiTietBackup>(entity =>
        {
            entity.HasKey(e => e.KyKiemKeChiTietId).HasName("PK__QLVT_KyK__8C6FDB4E70940C27");

            entity.ToTable("QLVT_KyKiemKe_ChiTiet_Backup");

            entity.Property(e => e.KyKiemKeChiTietId).HasColumnName("KyKiemKe_ChiTiet_Id");
            entity.Property(e => e.KhoChinhId).HasColumnName("KhoChinh_Id");
            entity.Property(e => e.KhoPhuId).HasColumnName("KhoPhu_Id");
            entity.Property(e => e.KiemKeIdGoc).HasColumnName("KiemKe_Id_Goc");
            entity.Property(e => e.KyKiemKeBackupId).HasColumnName("KyKiemKe_Backup_Id");
            entity.Property(e => e.NgayKiemKe).HasColumnType("datetime");
            entity.Property(e => e.NguoiKiemKeId).HasColumnName("NguoiKiemKe_Id");
            entity.Property(e => e.NguoiKiemKeTen)
                .HasMaxLength(255)
                .HasColumnName("NguoiKiemKe_Ten");
            entity.Property(e => e.SoLuongChenhLech).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuongKiemKe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuongSoSach).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoThe).HasMaxLength(255);
            entity.Property(e => e.VatTuId).HasColumnName("VatTu_Id");

            entity.HasOne(d => d.KyKiemKeBackup).WithMany(p => p.QlvtKyKiemKeChiTietBackups)
                .HasForeignKey(d => d.KyKiemKeBackupId)
                .HasConstraintName("FK_QLVT_KyKiemKe_ChiTiet_Backup_QLVT_KyKiemKe_Backup");
        });

        modelBuilder.Entity<QlvtKyKiemKeChiTietDff>(entity =>
        {
            entity.HasKey(e => e.ChiTietDffId);

            entity.ToTable("QLVT_KyKiemKe_ChiTiet_DFF");

            entity.Property(e => e.ChiTietDffId).HasColumnName("ChiTiet_DFF_Id");
            entity.Property(e => e.KyKiemKeChiTietId).HasColumnName("KyKiemKe_ChiTiet_Id");
            entity.Property(e => e.PhanTramDong).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PhanTramKemPhamChat).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PhanTramMatPhamChat).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.SoLuongDeNghiThanhLy).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuongDong).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuongKemPhamChat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuongMatPhamChat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TsKemPcMatPc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TS_KemPc_MatPc");
            entity.Property(e => e.VatTuId).HasColumnName("VatTu_Id");

            entity.HasOne(d => d.KyKiemKeChiTiet).WithMany(p => p.QlvtKyKiemKeChiTietDffs)
                .HasForeignKey(d => d.KyKiemKeChiTietId)
                .HasConstraintName("FK_QLVT_KyKiemKe_ChiTiet_DFF_QLVT_KyKiemKe_ChiTiet");
        });

        modelBuilder.Entity<QlvtKyKiemKeChiTietDffBackup>(entity =>
        {
            entity.HasKey(e => e.ChiTietDffId).HasName("PK__QLVT_KyK__687F0FA9523F00C7");

            entity.ToTable("QLVT_KyKiemKe_ChiTiet_DFF_Backup");

            entity.Property(e => e.ChiTietDffId).HasColumnName("ChiTiet_DFF_Id");
            entity.Property(e => e.KyKiemKeChiTietBackupId).HasColumnName("KyKiemKe_ChiTiet_Backup_Id");
            entity.Property(e => e.KyKiemKeIdGoc).HasColumnName("KyKiemKe_Id_Goc");
            entity.Property(e => e.PhanTramDong).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PhanTramKemPhamChat).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PhanTramMatPhamChat).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.SoLuongDeNghiThanhLy).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuongDong).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuongKemPhamChat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuongMatPhamChat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TsKemPcMatPc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TS_KemPc_MatPc");
            entity.Property(e => e.VatTuId).HasColumnName("VatTu_Id");

            entity.HasOne(d => d.KyKiemKeChiTietBackup).WithMany(p => p.QlvtKyKiemKeChiTietDffBackups)
                .HasForeignKey(d => d.KyKiemKeChiTietBackupId)
                .HasConstraintName("FK_QLVT_KyKiemKe_ChiTiet_DFF_Backup_QLVT_KyKiemKe_ChiTiet_Backup");
        });

        modelBuilder.Entity<QlvtKyKiemKeChiTietErpDemo>(entity =>
        {
            entity.HasKey(e => e.KyKiemKeChiTietId);

            entity.ToTable("QLVT_KyKiemKe_ChiTiet_ERP_Demo");

            entity.Property(e => e.KyKiemKeChiTietId).HasColumnName("KyKiemKe_ChiTiet_Id");
            entity.Property(e => e.KhoChinhId).HasColumnName("KhoChinh_Id");
            entity.Property(e => e.KhoPhuId).HasColumnName("KhoPhu_Id");
            entity.Property(e => e.KyKiemKeId).HasColumnName("KyKiemKe_Id");
            entity.Property(e => e.NgayKiemKe).HasColumnType("datetime");
            entity.Property(e => e.NguoiKiemKeId).HasColumnName("NguoiKiemKe_Id");
            entity.Property(e => e.NguoiKiemKeTen)
                .HasMaxLength(50)
                .HasColumnName("NguoiKiemKe_Ten");
            entity.Property(e => e.SoLuongChenhLech).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SoLuongKiemKe).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SoLuongSoSach).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.SoThe)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VatTuId).HasColumnName("VatTu_Id");
        });

        modelBuilder.Entity<QlvtKyKiemKeErpDemo>(entity =>
        {
            entity.HasKey(e => e.KyKiemKeId);

            entity.ToTable("QLVT_KyKiemKe_ERP_Demo");

            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.EndTag)
                .HasMaxLength(50)
                .HasColumnName("end_tag");
            entity.Property(e => e.FreezeDate)
                .HasColumnType("datetime")
                .HasColumnName("freeze_date");
            entity.Property(e => e.OrganizationCode)
                .HasMaxLength(50)
                .HasColumnName("organization_code");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.PhysicalInventoryDate)
                .HasColumnType("datetime")
                .HasColumnName("physical_inventory_date");
            entity.Property(e => e.PhysicalInventoryId).HasColumnName("physical_inventory_id");
            entity.Property(e => e.PhysicalInventoryName)
                .HasMaxLength(250)
                .HasColumnName("physical_inventory_name");
            entity.Property(e => e.StartTag)
                .HasMaxLength(50)
                .HasColumnName("start_tag");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<QlvtMuaSamPdxKy>(entity =>
        {
            entity.ToTable("QLVT_MuaSam_PDX_Ky");

            entity.Property(e => e.LyDo).HasMaxLength(250);
            entity.Property(e => e.MaDoiTuongKy).HasMaxLength(50);
            entity.Property(e => e.NgayKy).HasColumnType("datetimeoffset");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.Page).HasColumnName("page");
            entity.Property(e => e.PageHeight).HasColumnName("pageHeight");
            entity.Property(e => e.ThuTuKy).HasColumnName("ThuTu_Ky");
            entity.Property(e => e.ToaDo).HasMaxLength(200);
            entity.Property(e => e.UsbSerial)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Usb_Serial");
            entity.Property(e => e.VanBanId).HasColumnName("VanBan_Id");

            entity.HasOne(d => d.PhieuDeXuat).WithMany(p => p.QlvtMuaSamPdxKies)
                .HasForeignKey(d => d.PhieuDeXuatId)
                .HasConstraintName("FK_QLVT_MuaSam_PDX_Ky_QLVT_MuaSam_PhieuDeXuat");
        });

        modelBuilder.Entity<QlvtMuaSamPhieuDeXuat>(entity =>
        {
            entity.ToTable("QLVT_MuaSam_PhieuDeXuat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GhiChu).HasMaxLength(2000);
            entity.Property(e => e.MaPhieu)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MoTa).HasMaxLength(2000);
            entity.Property(e => e.TenPhieu).HasMaxLength(500);
        });

        modelBuilder.Entity<QlvtMuaSamPhieuDeXuatDetail>(entity =>
        {
            entity.ToTable("QLVT_MuaSam_PhieuDeXuat_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonViTinh).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasMaxLength(1000);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.IsSystemSupply)
                .HasDefaultValueSql("((1))")
                .HasComment("0: Vật tư chưa có trong hệ thống, 1: vật tư đã có trong hệ thống");
            entity.Property(e => e.PhieuDeXuatId).HasComment("Id phiếu đề xuất");
            entity.Property(e => e.TenVatTu).HasMaxLength(250);
            entity.Property(e => e.VatTuId)
                .HasComment("Nếu IDVatTu tồn tại thì vật tư được lấy từ hệ thông ERP, nếu không tồn tại thì Vật tư đc thêm mới vào phiếu đề xuất")
                .HasColumnName("VatTu_Id");
            entity.Property(e => e.XuatXu).HasMaxLength(50);

            entity.HasOne(d => d.PhieuDeXuat).WithMany(p => p.QlvtMuaSamPhieuDeXuatDetails)
                .HasForeignKey(d => d.PhieuDeXuatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QLVT_MuaSam_PhieuDeXuat_Detail_QLVT_MuaSam_PhieuDeXuat");
        });

        modelBuilder.Entity<QlvtMuaSamPhieuDeXuatLichSu>(entity =>
        {
            entity.ToTable("QLVT_MuaSam_PhieuDeXuat_LichSu");

            entity.Property(e => e.IdphieuDeXuat).HasColumnName("IDPhieuDeXuat");
            entity.Property(e => e.LyDo).HasMaxLength(1000);
            entity.Property(e => e.NguoiThucHien)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TrangThai).HasComment("=0: Nháp; =1: tạo và gửi duyệt; =2: duyệt; =4 Hủy duyệt");
        });

        modelBuilder.Entity<QlvtMuaSamVatTuNew>(entity =>
        {
            entity.HasKey(e => e.VatTuNewId);

            entity.ToTable("QLVT_MuaSam_VatTu_New");

            entity.Property(e => e.DonGia).HasDefaultValueSql("((0))");
            entity.Property(e => e.DonViTinh).HasMaxLength(100);
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.MaVatTu)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TenVatTu).HasMaxLength(500);
            entity.Property(e => e.XuatXu).HasMaxLength(200);
        });

        modelBuilder.Entity<QlvtVanBanKy>(entity =>
        {
            entity.ToTable("QLVT_VanBan_Ky");

            entity.Property(e => e.FileName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("File_Name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("File_Path");
            entity.Property(e => e.MaLoaiVanBan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.PhieuId).HasColumnName("Phieu_Id");
            entity.Property(e => e.TrangThaiTaoFile).HasColumnName("TrangThai_TaoFile");

            entity.HasOne(d => d.Phieu).WithMany(p => p.QlvtVanBanKies)
                .HasForeignKey(d => d.PhieuId)
                .HasConstraintName("FK_QLVT_VanBan_Ky_QLVT_MuaSam_PhieuDeXuat");
        });

        modelBuilder.Entity<QlvtVatTu>(entity =>
        {
            entity.HasKey(e => e.VatTuId).HasName("PK_QL_VatTu");

            entity.ToTable("QLVT_VatTu");

            entity.Property(e => e.VatTuId)
                .ValueGeneratedNever()
                .HasColumnName("VatTu_Id");
            entity.Property(e => e.DonGia).HasDefaultValueSql("((0))");
            entity.Property(e => e.DonViTinh).HasMaxLength(100);
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .HasComment("Ảnh đại diện, mặc định ảnh đầu tiên sẽ là ảnh mặc định hiển thị ban đầu. Khi tồn tại ảnh này thì cần check folder tương ứng xem còn ảnh ko");
            entity.Property(e => e.KhoId).HasColumnName("Kho_Id");
            entity.Property(e => e.MaVatTu)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasComment("Thông số kỹ thuật");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NguoiTao).HasMaxLength(100);
            entity.Property(e => e.NguoiTaoId).HasColumnName("NguoiTao_Id");
            entity.Property(e => e.TenVatTu).HasMaxLength(500);
            entity.Property(e => e.TrangThaiInQr).HasColumnName("TrangThai_InQr");
        });

        modelBuilder.Entity<QlvtVatTuBoMa>(entity =>
        {
            entity.HasKey(e => e.MaNhom).HasName("QLVT_VatTu_BoMa_PK");

            entity.ToTable("QLVT_VatTu_BoMa");

            entity.Property(e => e.MaNhom)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Dvt)
                .HasMaxLength(100)
                .HasColumnName("DVT");
            entity.Property(e => e.TenNhom).HasMaxLength(200);
        });

        modelBuilder.Entity<QlvtVatTuTonKho>(entity =>
        {
            entity.HasKey(e => new { e.InventoryItemId, e.OrganizationId }).HasName("PK_QLVT_VatTu_Kho");

            entity.ToTable("QLVT_VatTu_TonKho");

            entity.Property(e => e.InventoryItemId)
                .HasComment("Id vật tư")
                .HasColumnName("Inventory_Item_Id");
            entity.Property(e => e.OrganizationId)
                .HasComment("ID kho")
                .HasColumnName("Organization_Id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Người tạo")
                .HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasComment("Ngày tạo")
                .HasColumnType("date")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.LotNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Số lô vật tư")
                .HasColumnName("Lot_Number");
            entity.Property(e => e.OnhandQuantity)
                .HasComment("Số lượng tồn")
                .HasColumnName("Onhand_Quantity");
            entity.Property(e => e.SubinventoryCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Mã kho phụ")
                .HasColumnName("Subinventory_Code");
        });

        modelBuilder.Entity<QlvtVatTuTonKhoDinhMuc>(entity =>
        {
            entity.HasKey(e => e.IdVatTu).HasName("PK__QLVT_Vat__D4A7C3D84CC39D1B");

            entity.ToTable("QLVT_VatTu_TonKho_DinhMuc");

            entity.Property(e => e.IdVatTu).ValueGeneratedNever();
            entity.Property(e => e.DinhMuc).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<QlvtVatTuViTri>(entity =>
        {
            entity.HasKey(e => e.IdViTri);

            entity.ToTable("QLVT_VatTu_ViTri");

            entity.Property(e => e.IdViTri).HasColumnName("IDViTri");
            entity.Property(e => e.IdGiaKe).HasColumnName("IDGiaKe");
            entity.Property(e => e.IdHop).HasColumnName("IDHop");
            entity.Property(e => e.IdKhoErp).HasColumnName("IDKhoERP");
            entity.Property(e => e.IdNgan).HasColumnName("IDNgan");
            entity.Property(e => e.IdToMay).HasColumnName("IDToMay");
            entity.Property(e => e.IdVatTu).HasColumnName("IDVatTu");
            entity.Property(e => e.MaVatTu).HasMaxLength(50);
            entity.Property(e => e.TenVatTu).HasMaxLength(250);
            entity.Property(e => e.ViTri).HasMaxLength(500);
        });

        modelBuilder.Entity<QlvtViTri>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_QLVT_VT_ToMay");

            entity.ToTable("QLVT_ViTri");
            
            entity.Property(e => e.IdKhoErp)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IDKhoERP");
            entity.Property(e => e.KichThuoc).HasMaxLength(255);
            entity.Property(e => e.Ma).HasMaxLength(30);
            entity.Property(e => e.MoTa).HasMaxLength(2000);
            entity.Property(e => e.ParentId)
                .HasComment("=0 đây là mức cha\r\n<>0 đây là ID cha")
                .HasColumnName("ParentID");
            entity.Property(e => e.Ten).HasMaxLength(2001);
        });

        modelBuilder.Entity<TbAudience>(entity =>
        {
            entity.HasKey(e => e.ClientId);

            entity.ToTable("Tb_Audiences");

            entity.Property(e => e.ClientId).HasMaxLength(50);
            entity.Property(e => e.Base64Secret).HasMaxLength(80);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbCaiDatHeThong>(entity =>
        {
            entity.HasKey(e => e.MaCaiDatHeThong).HasName("PK_Tb_CaiDatHeThongDonVi_1");

            entity.ToTable("Tb_CaiDatHeThong");

            entity.HasIndex(e => e.TuKhoa, "IX_Tb_CaiDatHeThongDonVi").IsUnique();

            entity.Property(e => e.GiaTri).HasMaxLength(50);
            entity.Property(e => e.KichHoaHienThi)
                .HasDefaultValueSql("((0))")
                .HasComment("Sử dụng trong đồng bộ, =1 là cho phép hiện thị tính năng đồng bộ tại giao diện quản lý");
            entity.Property(e => e.KichHoat)
                .HasDefaultValueSql("((0))")
                .HasComment("Sử dụng trong đồng bộ, =1 là kích hoạt đồng bộ tự động");
            entity.Property(e => e.TuKhoa).HasMaxLength(50);
        });

        modelBuilder.Entity<TbCauHinhDieuHuongModun>(entity =>
        {
            entity.HasKey(e => e.MaCauHinhDieuHuongModun);

            entity.ToTable("Tb_CauHinhDieuHuongModun");

            entity.Property(e => e.TenAction)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenActionThayThe)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbCauHinhTrangThai>(entity =>
        {
            entity.ToTable("Tb_CauHinhTrangThai");

            entity.Property(e => e.MaMau)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.TenTrangThai).HasMaxLength(100);
        });

        modelBuilder.Entity<TbChucNang>(entity =>
        {
            entity.HasKey(e => e.MaChucNang);

            entity.ToTable("Tb_ChucNang");

            entity.Property(e => e.GioiThieu).HasMaxLength(255);
            entity.Property(e => e.TenChucNang).HasMaxLength(50);
            entity.Property(e => e.TenHienThi).HasMaxLength(50);

            entity.HasOne(d => d.MaMoDunNavigation).WithMany(p => p.TbChucNangs)
                .HasForeignKey(d => d.MaMoDun)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_ChucNang_Tb_Modun");

            entity.HasMany(d => d.MaLoaiKieuNguoiDungs).WithMany(p => p.MaChucNangs)
                .UsingEntity<Dictionary<string, object>>(
                    "TbChucNangLoaiKieuNguoiDung",
                    r => r.HasOne<TbLoaiKieuNguoiDung>().WithMany()
                        .HasForeignKey("MaLoaiKieuNguoiDung")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_ChucNang_LoaiKieuNguoiDung_Tb_LoaiKieuNguoiDung"),
                    l => l.HasOne<TbChucNang>().WithMany()
                        .HasForeignKey("MaChucNang")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_ChucNang_LoaiKieuNguoiDung_Tb_ChucNang"),
                    j =>
                    {
                        j.HasKey("MaChucNang", "MaLoaiKieuNguoiDung");
                        j.ToTable("Tb_ChucNang_LoaiKieuNguoiDung");
                    });
        });

        modelBuilder.Entity<TbCoCauToChuc>(entity =>
        {
            entity.HasKey(e => e.MaCoCauToChuc).HasName("PK_Oes_Tb_DonViNguoiDung");

            entity.ToTable("Tb_CoCauToChuc");

            entity.Property(e => e.MaCoCauToChuc).HasComment("Mã đơn vị người dùng. Khóa chính");
            entity.Property(e => e.DaXoa).HasDefaultValueSql("((0))");
            entity.Property(e => e.DiaChiLienHe)
                .HasMaxLength(255)
                .HasComment("Địa chỉ liên hệ");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasComment("Email");
            entity.Property(e => e.Lft).HasComment("Thuộc tính đánh dấu cạnh trái của 1 node trong kỹ thuật lưu trữ và truy vấn dữ liệu dạng cây");
            entity.Property(e => e.MaDonViSuDung).HasComment("Mã đơn vị sử dụng");
            entity.Property(e => e.MaNguoiQuanLy).HasComment("Mã người dùng của người quản lý đơn vị người dùng");
            entity.Property(e => e.NgayCapNhap).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.ParentId)
                .HasComment("Thuộc tính đánh dấu ID của node cha của 1 node trong kỹ thuật lưu trữ và truy vấn dữ liệu dạng cây")
                .HasColumnName("ParentID");
            entity.Property(e => e.Rght).HasComment("Thuộc tính đánh dấu cạnh phải của 1 node trong kỹ thuật lưu trữ và truy vấn dữ liệu dạng cây");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .HasComment("Số điện thoại");
            entity.Property(e => e.TenCoCauToChuc)
                .HasMaxLength(255)
                .HasComment("Tên đơn vị người dùng");
        });

        modelBuilder.Entity<TbDieuHuongModule>(entity =>
        {
            entity.HasKey(e => e.MaDieuHuongModule).HasName("PK_Tb_DonViSuDung_Controller_1");

            entity.ToTable("Tb_DieuHuongModule");

            entity.Property(e => e.TenController)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenControllerThayThe)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbDonViSuDung>(entity =>
        {
            entity.HasKey(e => e.MaDonViSuDung);

            entity.ToTable("Tb_DonViSuDung");

            entity.Property(e => e.Banner).HasMaxLength(255);
            entity.Property(e => e.BiDanh).HasMaxLength(255);
            entity.Property(e => e.CapHoc).HasComment("0: sở; 1: tiểu học; 2: thcs; 3: thpt; 4: mầm non; 5: phòng");
            entity.Property(e => e.ChoPhepNhieuTaiKhoanDangNhap).HasDefaultValueSql("((1))");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.DungLuongToiDa).HasDefaultValueSql("((1))");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Favicon).HasMaxLength(255);
            entity.Property(e => e.KichHoat)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.KieuMatKhau).HasComment("1: kiểu mặc định; 2: kiểu mặc định đủ 6 ký tự trở lên; 3: kiểu gồm cả chữ và số; 4: kiểu gồm cả chữ và số đủ 6 ký tự trở lên");
            entity.Property(e => e.Logo).HasMaxLength(255);
            entity.Property(e => e.LogoKhoaHoc).HasMaxLength(255);
            entity.Property(e => e.LoiThongCaoBenNgoai).HasColumnType("ntext");
            entity.Property(e => e.LoiThongCaoBenTrong).HasColumnType("ntext");
            entity.Property(e => e.MaKieuDangKyNd).HasColumnName("MaKieuDangKyND");
            entity.Property(e => e.NgayBatDau)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.NgayKetThuc)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.NgayKichHoat).HasColumnType("date");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoiDungBanner).HasMaxLength(255);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.SoLuongToiDaNguoiTruyCap).HasDefaultValueSql("((20))");
            entity.Property(e => e.TenDonViSuDung).HasMaxLength(255);
            entity.Property(e => e.TenMienCon).HasMaxLength(50);
            entity.Property(e => e.TenMienRieng).HasMaxLength(50);
            entity.Property(e => e.ThongBao).HasColumnType("ntext");
            entity.Property(e => e.TieuDeWeb).HasMaxLength(50);
            entity.Property(e => e.TrangChuNdlaTrangDangNhap)
                .HasDefaultValueSql("((1))")
                .HasColumnName("TrangChuNDLaTrangDangNhap");

            entity.HasOne(d => d.MaKieuDangKyNdNavigation).WithMany(p => p.TbDonViSuDungs)
                .HasForeignKey(d => d.MaKieuDangKyNd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_DonViSuDung_Tb_KieuDangKyND");

            entity.HasOne(d => d.MaNgonNguNavigation).WithMany(p => p.TbDonViSuDungs)
                .HasForeignKey(d => d.MaNgonNgu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_DonViSuDung_Tb_NgonNgu");

            entity.HasOne(d => d.MaTemplateNavigation).WithMany(p => p.TbDonViSuDungs)
                .HasForeignKey(d => d.MaTemplate)
                .HasConstraintName("FK_Tb_DonViSuDung_Tb_Template");

            entity.HasOne(d => d.MaTimeZoneNavigation).WithMany(p => p.TbDonViSuDungs)
                .HasForeignKey(d => d.MaTimeZone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_DonViSuDung_Tb_TimeZone");
        });

        modelBuilder.Entity<TbDonViSuDungModun>(entity =>
        {
            entity.HasKey(e => new { e.MaDonViSuDung, e.MaModun });

            entity.ToTable("Tb_DonViSuDung_Modun");

            entity.Property(e => e.KichHoat)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.MaDonViSuDungNavigation).WithMany(p => p.TbDonViSuDungModuns)
                .HasForeignKey(d => d.MaDonViSuDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_DonViSuDung_Modun_Tb_DonViSuDung");

            entity.HasOne(d => d.MaModunNavigation).WithMany(p => p.TbDonViSuDungModuns)
                .HasForeignKey(d => d.MaModun)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_DonViSuDung_Modun_Tb_Modun");
        });

        modelBuilder.Entity<TbHinhNenVanBang>(entity =>
        {
            entity.HasKey(e => e.MaHinhNenVanBang);

            entity.ToTable("Tb_HinhNenVanBang");

            entity.Property(e => e.HinhNen).HasColumnType("ntext");
            entity.Property(e => e.Template).HasColumnType("ntext");

            entity.HasOne(d => d.MaDonViSuDungNavigation).WithMany(p => p.TbHinhNenVanBangs)
                .HasForeignKey(d => d.MaDonViSuDung)
                .HasConstraintName("FK_Tb_HinhNenVanBang_Tb_HinhNenVanBang");
        });

        modelBuilder.Entity<TbHopThuDen>(entity =>
        {
            entity.HasKey(e => new { e.MaTinNhan, e.MaNguoiDung }).HasName("PK_Tb_HopThuDen_1");

            entity.ToTable("Tb_HopThuDen");

            entity.Property(e => e.NgayXem).HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.TbHopThuDens)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_HopThuDen_Tb_NguoiDung");

            entity.HasOne(d => d.MaTinNhanNavigation).WithMany(p => p.TbHopThuDens)
                .HasForeignKey(d => d.MaTinNhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_HopThuDen_Tb_TinNhan");
        });

        modelBuilder.Entity<TbItem>(entity =>
        {
            entity.HasKey(e => e.MaItem);

            entity.ToTable("Tb_Item");

            entity.Property(e => e.Mau).HasMaxLength(50);
            entity.Property(e => e.TenItem).HasMaxLength(100);

            entity.HasOne(d => d.MaItemMacDinhNavigation).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.MaItemMacDinh)
                .HasConstraintName("FK_Tb_Item_Tb_ItemMacDinh");

            entity.HasOne(d => d.MaThemeNavigation).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.MaTheme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_Item_Tb_Theme");
        });

        modelBuilder.Entity<TbItemMacDinh>(entity =>
        {
            entity.HasKey(e => e.MaItemMacDinh);

            entity.ToTable("Tb_ItemMacDinh");

            entity.Property(e => e.Mau).HasMaxLength(100);
            entity.Property(e => e.TenItemMacDinh).HasMaxLength(100);

            entity.HasOne(d => d.MaThemeMacDinhNavigation).WithMany(p => p.TbItemMacDinhs)
                .HasForeignKey(d => d.MaThemeMacDinh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_ItemMacDinh_Tb_ThemeMacDinh");
        });

        modelBuilder.Entity<TbKichBanCapNhatHeThong>(entity =>
        {
            entity.HasKey(e => e.MaKichBanCapNhatHeThong);

            entity.ToTable("Tb_KichBanCapNhatHeThong");

            entity.Property(e => e.DuongDanCopy).HasMaxLength(300);
            entity.Property(e => e.DuongDanPaste).HasMaxLength(300);
            entity.Property(e => e.LoaiFile)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbKieuDangKyNd>(entity =>
        {
            entity.HasKey(e => e.MaKieuDangKyNd);

            entity.ToTable("Tb_KieuDangKyND");

            entity.Property(e => e.MaKieuDangKyNd).HasColumnName("MaKieuDangKyND");
            entity.Property(e => e.TenHienThi).HasMaxLength(100);
            entity.Property(e => e.TenKieuDangKyNd)
                .HasMaxLength(100)
                .HasColumnName("TenKieuDangKyND");
        });

        modelBuilder.Entity<TbKieuEmailAndMessage>(entity =>
        {
            entity.HasKey(e => e.MaKieuEmailAndMessage).HasName("PK_Tb_KieuEmailAndMessage_1");

            entity.ToTable("Tb_KieuEmailAndMessage");

            entity.Property(e => e.DuongDanEn)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DuongDanEN");
            entity.Property(e => e.DuongDanVi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DuongDanVI");
            entity.Property(e => e.NoiDungTempleteEn)
                .HasColumnType("ntext")
                .HasColumnName("NoiDungTempleteEN");
            entity.Property(e => e.NoiDungTempleteVi)
                .HasColumnType("ntext")
                .HasColumnName("NoiDungTempleteVI");
            entity.Property(e => e.TenKieuEmailAndMessage)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbKieuEmailAndMessageDonViSuDung>(entity =>
        {
            entity.HasKey(e => e.MaKieuEmailAndMessageDonViSuDung).HasName("PK_Tb_KieuEmailAndMessage_DonViSuDung_1");

            entity.ToTable("Tb_KieuEmailAndMessage_DonViSuDung");

            entity.Property(e => e.MaKieuEmailAndMessageDonViSuDung).HasColumnName("MaKieuEmailAndMessage_DonViSuDung");
            entity.Property(e => e.En)
                .HasColumnType("ntext")
                .HasColumnName("en");
            entity.Property(e => e.TenKieuEmailAndMessage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Vi)
                .HasColumnType("ntext")
                .HasColumnName("vi");
        });

        modelBuilder.Entity<TbKieuNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaKieuNguoiDung);

            entity.ToTable("Tb_KieuNguoiDung");

            entity.Property(e => e.TenKieuNguoiDung).HasMaxLength(255);

            entity.HasOne(d => d.MaDonViSuDungNavigation).WithMany(p => p.TbKieuNguoiDungs)
                .HasForeignKey(d => d.MaDonViSuDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_KieuNguoiDung_Tb_DonViSuDung");

            entity.HasOne(d => d.MaLoaiKieuNguoiDungNavigation).WithMany(p => p.TbKieuNguoiDungs)
                .HasForeignKey(d => d.MaLoaiKieuNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_KieuNguoiDung_Tb_LoaiKieuNguoiDung");
        });

        modelBuilder.Entity<TbKieuNguoiDungChucNangLoaiKieuNguoiDung>(entity =>
        {
            entity.HasKey(e => new { e.MaKieuNguoiDung, e.MaChucNang, e.MaLoaiKieuNguoiDung }).HasName("PK_Tb_ChucVu_ChucNang");

            entity.ToTable("Tb_KieuNguoiDung_ChucNang_LoaiKieuNguoiDung");

            entity.HasOne(d => d.MaChucNangNavigation).WithMany(p => p.TbKieuNguoiDungChucNangLoaiKieuNguoiDungs)
                .HasForeignKey(d => d.MaChucNang)
                .HasConstraintName("FK_Tb_KieuNguoiDung_ChucNang_Tb_ChucNang");

            entity.HasOne(d => d.MaKieuNguoiDungNavigation).WithMany(p => p.TbKieuNguoiDungChucNangLoaiKieuNguoiDungs)
                .HasForeignKey(d => d.MaKieuNguoiDung)
                .HasConstraintName("FK_Tb_KieuNguoiDung_ChucNang_Tb_KieuNguoiDung");

            entity.HasOne(d => d.MaLoaiKieuNguoiDungNavigation).WithMany(p => p.TbKieuNguoiDungChucNangLoaiKieuNguoiDungs)
                .HasForeignKey(d => d.MaLoaiKieuNguoiDung)
                .HasConstraintName("FK_Tb_KieuNguoiDung_ChucNang_LoaiKieuNguoiDung_Tb_LoaiKieuNguoiDung");
        });

        modelBuilder.Entity<TbKieuNguoiDungModunLoaiKieuNguoiDung>(entity =>
        {
            entity.HasKey(e => new { e.MaKieuNguoiDung, e.MaModun, e.MaLoaiKieuNguoiDung }).HasName("PK_Tb_ChucVu_Modun");

            entity.ToTable("Tb_KieuNguoiDung_Modun_LoaiKieuNguoiDung");

            entity.Property(e => e.ViTri)
                .HasDefaultValueSql("((0))")
                .HasComment("0");

            entity.HasOne(d => d.MaKieuNguoiDungNavigation).WithMany(p => p.TbKieuNguoiDungModunLoaiKieuNguoiDungs)
                .HasForeignKey(d => d.MaKieuNguoiDung)
                .HasConstraintName("FK_Tb_KieuNguoiDung_Modun_Tb_KieuNguoiDung");

            entity.HasOne(d => d.MaLoaiKieuNguoiDungNavigation).WithMany(p => p.TbKieuNguoiDungModunLoaiKieuNguoiDungs)
                .HasForeignKey(d => d.MaLoaiKieuNguoiDung)
                .HasConstraintName("FK_Tb_KieuNguoiDung_Modun_LoaiKieuNguoiDung_Tb_LoaiKieuNguoiDung");

            entity.HasOne(d => d.MaModunNavigation).WithMany(p => p.TbKieuNguoiDungModunLoaiKieuNguoiDungs)
                .HasForeignKey(d => d.MaModun)
                .HasConstraintName("FK_Tb_KieuNguoiDung_Modun_Tb_Modun");
        });

        modelBuilder.Entity<TbKieuNoiDung>(entity =>
        {
            entity.HasKey(e => e.MaKieuNoiDung);

            entity.ToTable("Tb_KieuNoiDung");

            entity.Property(e => e.KieuNhanDien).HasMaxLength(50);
            entity.Property(e => e.TenKieuNoiDung).HasMaxLength(100);
        });

        modelBuilder.Entity<TbLoaiKieuNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaLoaiKieuNguoiDung);

            entity.ToTable("Tb_LoaiKieuNguoiDung");

            entity.Property(e => e.TenLoaiKieuNguoiDung).HasMaxLength(100);
            entity.Property(e => e.TenNhanDang).HasMaxLength(50);
        });

        modelBuilder.Entity<TbLoaiTepTin>(entity =>
        {
            entity.HasKey(e => e.MaLoaiTepTin);

            entity.ToTable("Tb_LoaiTepTin");

            entity.Property(e => e.TenLoaiTepTin).HasMaxLength(50);
        });

        modelBuilder.Entity<TbLsNguoiDungCapNhatNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaLsNguoiDungCapNhatNguoiDung);

            entity.ToTable("Tb_LS_NguoiDung_CapNhat_NguoiDung");

            entity.Property(e => e.MaLsNguoiDungCapNhatNguoiDung).HasColumnName("MaLS_NguoiDung_CapNhat_NguoiDung");
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiBiTacDongNavigation).WithMany(p => p.TbLsNguoiDungCapNhatNguoiDungMaNguoiBiTacDongNavigations)
                .HasForeignKey(d => d.MaNguoiBiTacDong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_LS_NguoiDung_CapNhat_NguoiDung_Tb_NguoiDung1");

            entity.HasOne(d => d.MaNguoiTacDongNavigation).WithMany(p => p.TbLsNguoiDungCapNhatNguoiDungMaNguoiTacDongNavigations)
                .HasForeignKey(d => d.MaNguoiTacDong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_LS_NguoiDung_CapNhat_NguoiDung_Tb_NguoiDung");
        });

        modelBuilder.Entity<TbLsNguoiDungCoCauToChuc>(entity =>
        {
            entity.HasKey(e => e.MaLsNguoiDungCoCauToChucOes).HasName("PK_Tb_LS_NguoiDung_CoCauToChuc_Oes");

            entity.ToTable("Tb_LS_NguoiDung_CoCauToChuc");

            entity.Property(e => e.MaLsNguoiDungCoCauToChucOes).HasColumnName("MaLS_NguoiDung_CoCauToChuc_Oes");
            entity.Property(e => e.NgayThaoTac).HasColumnType("datetime");
            entity.Property(e => e.ThaoTac)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbLsNguoiDungDonViSuDung>(entity =>
        {
            entity.HasKey(e => e.MaLsNguoiDungDonViSuDung).HasName("PK_Tb_LichSu_NguoiDung_DonViSuDung");

            entity.ToTable("Tb_LS_NguoiDung_DonViSuDung");

            entity.Property(e => e.MaLsNguoiDungDonViSuDung).HasColumnName("MaLS_NguoiDung_DonViSuDung");
            entity.Property(e => e.ThoiGianRa).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianVao).HasColumnType("datetime");

            entity.HasOne(d => d.MaDonViSuDungNavigation).WithMany(p => p.TbLsNguoiDungDonViSuDungs)
                .HasForeignKey(d => d.MaDonViSuDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_LichSu_NguoiDung_DonViSuDung_Tb_DonViSuDung");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.TbLsNguoiDungDonViSuDungs)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_LichSu_NguoiDung_DonViSuDung_Tb_NguoiDung");
        });

        modelBuilder.Entity<TbLsNguoiDungTaoNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaLsNguoiDungTaoNguoiDung);

            entity.ToTable("Tb_LS_NguoiDung_Tao_NguoiDung");

            entity.Property(e => e.MaLsNguoiDungTaoNguoiDung).HasColumnName("MaLS_NguoiDung_Tao_NguoiDung");
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiBiTacDongNavigation).WithMany(p => p.TbLsNguoiDungTaoNguoiDungMaNguoiBiTacDongNavigations)
                .HasForeignKey(d => d.MaNguoiBiTacDong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_LS_NguoiDung_Tao_NguoiDung_Tb_NguoiDung1");

            entity.HasOne(d => d.MaNguoiTacDongNavigation).WithMany(p => p.TbLsNguoiDungTaoNguoiDungMaNguoiTacDongNavigations)
                .HasForeignKey(d => d.MaNguoiTacDong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_LS_NguoiDung_Tao_NguoiDung_Tb_NguoiDung");
        });

        modelBuilder.Entity<TbLsNguoiDungXoaModun>(entity =>
        {
            entity.HasKey(e => e.MaLsNguoiDungXoaModun);

            entity.ToTable("Tb_LS_NguoiDung_Xoa_Modun");

            entity.Property(e => e.MaLsNguoiDungXoaModun).HasColumnName("MaLS_NguoiDung_Xoa_Modun");
            entity.Property(e => e.TenModun)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbLsNguoiDungXoaNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaLsNguoiDungXoaNguoiDung);

            entity.ToTable("Tb_LS_NguoiDung_Xoa_NguoiDung");

            entity.Property(e => e.MaLsNguoiDungXoaNguoiDung).HasColumnName("MaLS_NguoiDung_Xoa_NguoiDung");
            entity.Property(e => e.MaNguoiThucHienKhoiPhuc).HasComment("Mã người dùng thực hiện khôi phục 1 bản ghi hay xóa vĩnh viễn 1 bản ghi khỏi thùng rác");
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianThucHien)
                .HasComment("Thời gian khôi phục hay xóa vĩnh viễ 1 bản ghi trong thùng rác")
                .HasColumnType("datetime");
            entity.Property(e => e.TranngThai).HasComment("Trạng thái: =1 hiển thị;=2 đã khôi phục (không hiển thị);= 10 xóa vĩnh viễn (không hiển thị)");

            entity.HasOne(d => d.MaNguoiBiTacDongNavigation).WithMany(p => p.TbLsNguoiDungXoaNguoiDungMaNguoiBiTacDongNavigations)
                .HasForeignKey(d => d.MaNguoiBiTacDong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_LS_NguoiDung_Xoa_NguoiDung_Tb_NguoiDung1");

            entity.HasOne(d => d.MaNguoiTacDongNavigation).WithMany(p => p.TbLsNguoiDungXoaNguoiDungMaNguoiTacDongNavigations)
                .HasForeignKey(d => d.MaNguoiTacDong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_LS_NguoiDung_Xoa_NguoiDung_Tb_NguoiDung");
        });

        modelBuilder.Entity<TbMayChuServerFile>(entity =>
        {
            entity.HasKey(e => e.MaMayChuServerFile);

            entity.ToTable("Tb_MayChuServerFile");

            entity.Property(e => e.DiaChiMayChuServerFile).HasMaxLength(50);
        });

        modelBuilder.Entity<TbModun>(entity =>
        {
            entity.HasKey(e => e.MaModun).HasName("PK_Tb_MoDun");

            entity.ToTable("Tb_Modun");

            entity.Property(e => e.CoDungChung)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.GioiThieu).HasMaxLength(255);
            entity.Property(e => e.Icon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.OchiNhanh)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("OChiNhanh");
            entity.Property(e => e.PhienBan).HasMaxLength(50);
            entity.Property(e => e.TenAction).HasMaxLength(50);
            entity.Property(e => e.TenController).HasMaxLength(50);
            entity.Property(e => e.TenControllerTuongDuong).HasMaxLength(200);
            entity.Property(e => e.TenHienThi).HasMaxLength(50);
            entity.Property(e => e.TenModun).HasMaxLength(50);
            entity.Property(e => e.ThayTheModule)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MaNhomModunNavigation).WithMany(p => p.TbModuns)
                .HasForeignKey(d => d.MaNhomModun)
                .HasConstraintName("FK_Tb_Modun_Tb_NhomModun");

            entity.HasMany(d => d.MaLoaiKieuNguoiDungs).WithMany(p => p.MaModuns)
                .UsingEntity<Dictionary<string, object>>(
                    "TbModunLoaiKieuNguoiDung",
                    r => r.HasOne<TbLoaiKieuNguoiDung>().WithMany()
                        .HasForeignKey("MaLoaiKieuNguoiDung")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_Modun_LoaiKieuNguoiDung_Tb_LoaiKieuNguoiDung"),
                    l => l.HasOne<TbModun>().WithMany()
                        .HasForeignKey("MaModun")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_Modun_LoaiKieuNguoiDung_Tb_Modun"),
                    j =>
                    {
                        j.HasKey("MaModun", "MaLoaiKieuNguoiDung");
                        j.ToTable("Tb_Modun_LoaiKieuNguoiDung");
                    });
        });

        modelBuilder.Entity<TbModunChaCon>(entity =>
        {
            entity.HasKey(e => new { e.MaModunCha, e.MaModunCon, e.MaLoaiKieuNguoiDung });

            entity.ToTable("Tb_ModunChaCon");

            entity.HasOne(d => d.MaLoaiKieuNguoiDungNavigation).WithMany(p => p.TbModunChaCons)
                .HasForeignKey(d => d.MaLoaiKieuNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_ModunChaCon_Tb_LoaiKieuNguoiDung");

            entity.HasOne(d => d.MaModunChaNavigation).WithMany(p => p.TbModunChaConMaModunChaNavigations)
                .HasForeignKey(d => d.MaModunCha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_ModunChaCon_Tb_Modun");

            entity.HasOne(d => d.MaModunConNavigation).WithMany(p => p.TbModunChaConMaModunConNavigations)
                .HasForeignKey(d => d.MaModunCon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_ModunChaCon_Tb_Modun1");
        });

        modelBuilder.Entity<TbNgonNgu>(entity =>
        {
            entity.HasKey(e => e.MaNgonNgu);

            entity.ToTable("Tb_NgonNgu");

            entity.Property(e => e.NhanDien).HasMaxLength(10);
            entity.Property(e => e.TenNgonNgu).HasMaxLength(50);
        });

        modelBuilder.Entity<TbNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung);

            entity.ToTable("Tb_NguoiDung");

            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(255)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.CoChoPhepHienThi)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Ho).HasMaxLength(50);
            entity.Property(e => e.HocHam).HasMaxLength(50);
            entity.Property(e => e.HocVi).HasMaxLength(50);
            entity.Property(e => e.IddonVi)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IDDonVi");
            entity.Property(e => e.IdluongXuLy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IDLuongXuLy");
            entity.Property(e => e.Info)
                .HasDefaultValueSql("('{}')")
                .HasColumnName("info");
            entity.Property(e => e.KichHoat)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.MaDinhDanh).HasMaxLength(20);
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.NgaySinh)
                .HasDefaultValueSql("('1900-01-01')")
                .HasColumnType("date");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoiCongTac).HasMaxLength(255);
            entity.Property(e => e.OtpSecret)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("otp_secret");
            entity.Property(e => e.Salt).HasMaxLength(10);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.SoNguoiDungThich).HasDefaultValueSql("((0))");
            entity.Property(e => e.Ten).HasMaxLength(50);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.ThoiGianMatKhau).HasColumnType("datetime");
            entity.Property(e => e.TienTrongTaiKhoan).HasDefaultValueSql("((0))");
            entity.Property(e => e.TieuSu).HasMaxLength(3000);
            entity.Property(e => e.ViTri)
                .HasDefaultValueSql("((0))")
                .HasComment("Lưu giá trị theo bảng Tb_ViTriCongViec");

            entity.HasOne(d => d.MaDonViSuDungNavigation).WithMany(p => p.TbNguoiDungs)
                .HasForeignKey(d => d.MaDonViSuDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_NguoiDung_Tb_NguoiDung");

            entity.HasOne(d => d.MaKieuNguoiDungNavigation).WithMany(p => p.TbNguoiDungs)
                .HasForeignKey(d => d.MaKieuNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_NguoiDung_Tb_KieuNguoiDung");

            entity.HasOne(d => d.MaTimeZoneNavigation).WithMany(p => p.TbNguoiDungs)
                .HasForeignKey(d => d.MaTimeZone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_NguoiDung_Tb_TimeZone");

            entity.HasMany(d => d.MaChucNangs).WithMany(p => p.MaNguoiDungs)
                .UsingEntity<Dictionary<string, object>>(
                    "TbNguoiDungChucNang",
                    r => r.HasOne<TbChucNang>().WithMany()
                        .HasForeignKey("MaChucNang")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_NguoiDung_ChucNang_Tb_ChucNang"),
                    l => l.HasOne<TbNguoiDung>().WithMany()
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_NguoiDung_ChucNang_Tb_NguoiDung"),
                    j =>
                    {
                        j.HasKey("MaNguoiDung", "MaChucNang");
                        j.ToTable("Tb_NguoiDung_ChucNang");
                    });

            entity.HasMany(d => d.MaModuns).WithMany(p => p.MaNguoiDungs)
                .UsingEntity<Dictionary<string, object>>(
                    "TbNguoiDungModun",
                    r => r.HasOne<TbModun>().WithMany()
                        .HasForeignKey("MaModun")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_NguoiDung_Modun_Tb_Modun"),
                    l => l.HasOne<TbNguoiDung>().WithMany()
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_NguoiDung_Modun_Tb_NguoiDung"),
                    j =>
                    {
                        j.HasKey("MaNguoiDung", "MaModun");
                        j.ToTable("Tb_NguoiDung_Modun");
                    });
        });

        modelBuilder.Entity<TbNguoiDungCoCauToChucO>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDungCoCauToChuc).HasName("PK_Oes_Tb_NguoiDung_DonViNguoiDung");

            entity.ToTable("Tb_NguoiDung_CoCauToChuc_Oes");

            entity.Property(e => e.MaNguoiDungCoCauToChuc)
                .HasComment("Mã người dùng _ đơn vị người dùng. Khóa chính")
                .HasColumnName("MaNguoiDung_CoCauToChuc");
            entity.Property(e => e.ChucDanh)
                .HasMaxLength(255)
                .HasComment("Chức danh của người dùng trong đơn vị đó");
            entity.Property(e => e.DenNgay)
                .HasComment("Ngày người dùng hết thuộc đơn vị")
                .HasColumnType("datetime");
            entity.Property(e => e.HienTai)
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu người dùng có đang thuộc đơn vị hay không. Mặc định là 0 (False)");
            entity.Property(e => e.MaCoCauToChuc).HasComment("Mã đơn vị người dùng");
            entity.Property(e => e.MaNguoiDung).HasComment("Mã người dùng");
            entity.Property(e => e.TuNgay)
                .HasComment("Ngày người dùng bắt đầu thuộc đơn vị")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaCoCauToChucNavigation).WithMany(p => p.TbNguoiDungCoCauToChucOs)
                .HasForeignKey(d => d.MaCoCauToChuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_NguoiDung_DonViNguoiDung_Tb_DonViNguoiDung_Oes");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.TbNguoiDungCoCauToChucOs)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tb_NguoiD__MaNgu__176F17C2");
        });

        modelBuilder.Entity<TbNhomModun>(entity =>
        {
            entity.HasKey(e => e.MaNhomModun).HasName("PK_NhomModun");

            entity.ToTable("Tb_NhomModun");

            entity.Property(e => e.MoTa).HasColumnType("ntext");
            entity.Property(e => e.TenNhomModun).HasMaxLength(50);
        });

        modelBuilder.Entity<TbPhanQuyenModun>(entity =>
        {
            entity.HasKey(e => e.MaModunKhongPhanQuyen).HasName("PK_Tb_ModunKhongPhanQuyen");

            entity.ToTable("Tb_PhanQuyenModun");

            entity.Property(e => e.Icon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenAction)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenController)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenHienThi)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbTemplate>(entity =>
        {
            entity.HasKey(e => e.MaTemplate).HasName("PK_Tb_Theme");

            entity.ToTable("Tb_Template");

            entity.HasIndex(e => e.TenTemplate, "IX_Tb_Theme").IsUnique();

            entity.Property(e => e.AnhLayout).HasMaxLength(255);
            entity.Property(e => e.DuongDan)
                .HasMaxLength(255)
                .HasDefaultValueSql("(N'/Views/Themes/MacDinh/Theme1/')");
            entity.Property(e => e.PhienBan).HasMaxLength(50);
            entity.Property(e => e.TenHienThi).HasMaxLength(255);
            entity.Property(e => e.TenTemplate).HasMaxLength(50);

            entity.HasMany(d => d.MaDonViSuDungs).WithMany(p => p.MaTemplates)
                .UsingEntity<Dictionary<string, object>>(
                    "TbTemplateDonViSuDung",
                    r => r.HasOne<TbDonViSuDung>().WithMany()
                        .HasForeignKey("MaDonViSuDung")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_Template_DonViSuDung_Tb_DonViSuDung"),
                    l => l.HasOne<TbTemplate>().WithMany()
                        .HasForeignKey("MaTemplate")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_Template_DonViSuDung_Tb_Template"),
                    j =>
                    {
                        j.HasKey("MaTemplate", "MaDonViSuDung");
                        j.ToTable("Tb_Template_DonViSuDung");
                    });
        });

        modelBuilder.Entity<TbTepTin>(entity =>
        {
            entity.HasKey(e => e.MaTepTin);

            entity.ToTable("Tb_TepTin");

            entity.Property(e => e.DoPhanGiai).HasMaxLength(50);
            entity.Property(e => e.DuongDan).HasColumnType("ntext");
            entity.Property(e => e.DuongDanFileConvert).HasColumnType("ntext");
            entity.Property(e => e.DuongDanFileGoc).HasColumnType("ntext");
            entity.Property(e => e.DuongDanM3u8)
                .HasColumnType("ntext")
                .HasColumnName("DuongDanM3U8");
            entity.Property(e => e.TenTepTin).HasMaxLength(100);

            entity.HasOne(d => d.MaDonViSuDungNavigation).WithMany(p => p.TbTepTins)
                .HasForeignKey(d => d.MaDonViSuDung)
                .HasConstraintName("FK_Tb_TepTin_Tb_DonViSuDung");

            entity.HasOne(d => d.MaLoaiTepTinNavigation).WithMany(p => p.TbTepTins)
                .HasForeignKey(d => d.MaLoaiTepTin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_TepTin_Tb_LoaiTepTin");

            entity.HasOne(d => d.MaMayChuServerFileNavigation).WithMany(p => p.TbTepTins)
                .HasForeignKey(d => d.MaMayChuServerFile)
                .HasConstraintName("FK_Tb_TepTin_Tb_MayChuServerFile");
        });

        modelBuilder.Entity<TbTheme>(entity =>
        {
            entity.HasKey(e => e.MaTheme).HasName("PK_Tb_Theme_1");

            entity.ToTable("Tb_Theme");

            entity.Property(e => e.TenTheme).HasMaxLength(100);

            entity.HasOne(d => d.MaDonViSuDungNavigation).WithMany(p => p.TbThemes)
                .HasForeignKey(d => d.MaDonViSuDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_Theme_Tb_DonViSuDung");

            entity.HasOne(d => d.MaTemplateNavigation).WithMany(p => p.TbThemes)
                .HasForeignKey(d => d.MaTemplate)
                .HasConstraintName("FK_Tb_Theme_Tb_Template");

            entity.HasOne(d => d.MaThemeMacDinhNavigation).WithMany(p => p.TbThemes)
                .HasForeignKey(d => d.MaThemeMacDinh)
                .HasConstraintName("FK_Tb_Theme_Tb_ThemeMacDinh");
        });

        modelBuilder.Entity<TbThemeMacDinh>(entity =>
        {
            entity.HasKey(e => e.MaThemeMacDinh);

            entity.ToTable("Tb_ThemeMacDinh");

            entity.Property(e => e.TenThemeMacDinh).HasMaxLength(100);

            entity.HasOne(d => d.MaTemplateNavigation).WithMany(p => p.TbThemeMacDinhs)
                .HasForeignKey(d => d.MaTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_ThemeMacDinh_Tb_Template");
        });

        modelBuilder.Entity<TbThongTin>(entity =>
        {
            entity.HasKey(e => e.MaThongTin).HasName("PK_Tb_ThongTin_1");

            entity.ToTable("Tb_ThongTin");

            entity.Property(e => e.Anh).HasMaxLength(255);
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
            entity.Property(e => e.TieuDe).HasMaxLength(255);

            entity.HasOne(d => d.MaDonViSuDungNavigation).WithMany(p => p.TbThongTins)
                .HasForeignKey(d => d.MaDonViSuDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_ThongTin_Tb_DonViSuDung");
        });

        modelBuilder.Entity<TbThungRac>(entity =>
        {
            entity.HasKey(e => e.MaThungRac);

            entity.ToTable("Tb_ThungRac");

            entity.Property(e => e.DuongDan).HasComment("vị trí xóa (xóa trong bảng nào để tiện hoàn tác)");
            entity.Property(e => e.MaNoiDung).HasComment("Mã nội dung xóa");
            entity.Property(e => e.NgaySua).HasColumnType("date");
            entity.Property(e => e.NgayXoa).HasColumnType("date");
        });

        modelBuilder.Entity<TbTimeZone>(entity =>
        {
            entity.HasKey(e => e.MaTimeZone);

            entity.ToTable("Tb_TimeZone");

            entity.Property(e => e.TenTimeZone).HasMaxLength(255);
        });

        modelBuilder.Entity<TbTinNhan>(entity =>
        {
            entity.HasKey(e => e.MaTinNhan);

            entity.ToTable("Tb_TinNhan");

            entity.Property(e => e.ChuDe).HasMaxLength(255);
            entity.Property(e => e.NgayGui).HasColumnType("datetime");
            entity.Property(e => e.TepDinhKem).HasMaxLength(255);

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.TbTinNhans)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_TinNhan_Tb_NguoiDung");

            entity.HasOne(d => d.MaTinNhanPhanHoiNavigation).WithMany(p => p.InverseMaTinNhanPhanHoiNavigation)
                .HasForeignKey(d => d.MaTinNhanPhanHoi)
                .HasConstraintName("FK_Tb_TinNhan_Tb_TinNhan");

            entity.HasMany(d => d.MaNguoiDungs).WithMany(p => p.MaTinNhans)
                .UsingEntity<Dictionary<string, object>>(
                    "TbTinNhanNguoiDung",
                    r => r.HasOne<TbNguoiDung>().WithMany()
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_TinNhan_NguoiDung_Tb_NguoiDung"),
                    l => l.HasOne<TbTinNhan>().WithMany()
                        .HasForeignKey("MaTinNhan")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Tb_TinNhan_NguoiDung_Tb_TinNhan"),
                    j =>
                    {
                        j.HasKey("MaTinNhan", "MaNguoiDung");
                        j.ToTable("Tb_TinNhan_NguoiDung");
                    });
        });

        modelBuilder.Entity<TbTinNhanTepTin>(entity =>
        {
            entity.HasKey(e => new { e.MaTinNhan, e.MaTepTin });

            entity.ToTable("Tb_TinNhan_TepTin");
        });

        modelBuilder.Entity<TbViTriCongViec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EDM_ViTriCongViec");

            entity.ToTable("Tb_ViTriCongViec");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsGuiDanhGia).HasComment("0: Không gửi đánh giá/ 1: Gửi đánh ra lên đơn vị cấp trên");
            entity.Property(e => e.MaDoiTuongKy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayThem).HasColumnType("datetime");
            entity.Property(e => e.TenViTriCongViec).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
