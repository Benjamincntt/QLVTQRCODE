﻿using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Core.Services.Accounts;
using ESPlatform.QRCode.IMS.Core.Services.Authentication;
using ESPlatform.QRCode.IMS.Core.Services.Common;
using ESPlatform.QRCode.IMS.Core.Services.GioHang;
using ESPlatform.QRCode.IMS.Core.Services.KiemKe;
using ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;
using ESPlatform.QRCode.IMS.Core.Services.PhieuKy;
using ESPlatform.QRCode.IMS.Core.Services.TbNguoiDungs;
using ESPlatform.QRCode.IMS.Core.Services.TraCuu;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ESPlatform.QRCode.IMS.Api.Extensions;

public static class RegisterAppCoreServicesExtensions
{
    public static IServiceCollection RegisterAppCoreServices(this IServiceCollection services)
    {
        services
            .AddDbContextPool<AppDbContext>(
                options =>
                {
                    options
                        .UseSqlServer(AppConfig.Instance.ConnectionStrings.Default)
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .EnableSensitiveDataLogging(true);
#if DEBUG
                    options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information);
                    options.LogTo(Log.Logger.Information, LogLevel.Information);
#endif
                }
            )
            .AddHttpContextAccessor()
            .AddMemoryCache()
            // Repositories
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<IDonViSuDungRepository, DonViSuDungRepository>()
            .AddScoped<INguoiDungRepository, NguoiDungRepository>()
            .AddScoped<IVatTuRepository, VatTuRepository>()
            .AddScoped<IVatTuViTriRepository, VatTuViTriRepository>()
            .AddScoped<IKyKiemKeChiTietDffRepository, KyKiemKeChiTietDffRepository>()
            .AddScoped<IKhoRepository, KhoRepository>()
            .AddScoped<IViTriRepository, ViTriRepository>()
            .AddScoped<IKyKiemKeChiTietRepository, KyKiemKeChiTietRepository>()
            .AddScoped<IKyKiemKeRepository, KyKiemKeRepository>()
            .AddScoped<IMuaSamPhieuDeXuatRepository, MuaSamPhieuDeXuatRepository>()
            .AddScoped<IMuaSamVatTuNewRepository, MuaSamVatTuNewRepository>()
            .AddScoped<IMuaSamPhieuDeXuatDetailRepository, MuaSamPhieuDeXuatDetailRepository>()
            .AddScoped<IGioHangRepository, GioHangRepository>()
            .AddScoped<IVanBanKyRepository, VanBanKyRepository>()
            .AddScoped<IVatTuBoMaRepository, VatTuBoMaRepository>()
            .AddScoped<IPhieuKyRepository, PhieuKyRepository>()
            .AddScoped<IMuaSamPdxKyRepository, MuaSamPdxKyRepository>()
            .AddScoped<ICauHinhVanBanKyRepository, CauHinhVanBanKyRepository>()
            .AddScoped<IViTriCongViecRepository, ViTriCongViecRepository>()
            .AddScoped<IMuaSamPdxKyRepository, MuaSamPdxKyRepository>()
            .AddScoped<IVatTuTonKhoRepository, VatTuTonKhoRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            // Facades
            .AddScoped<IAuthorizedContextFacade, AuthorizedContextFacade>()

            // Services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<INguoiDungService, NguoiDungService>()
            .AddScoped<IKiemKeService, KiemKeService>()
            .AddScoped<ITraCuuService, TraCuuService>()
            .AddScoped<ICommonService, CommonService>()
            .AddScoped<IMuaSamVatTuService, MuaSamVatTuService>()
            .AddScoped<IGioHangService, GioHangService>()
            .AddScoped<IPhieuKyService, PhieuKyService>()
            ;

        MobileCA.Infrastructure.DependencyInjection.AddInfrastructureServices(services);


        return services;
    }
}