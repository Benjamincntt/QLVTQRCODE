using ESPlatform.QRCode.IMS.Domain.Interfaces;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Http;

namespace ESPlatform.QRCode.IMS.Core.Services.TbDonViSuDungs;

public class DonViSuDungService : IDonViSuDungService
{
    private readonly IDonViSuDungRepository _donViSuDungRepository;

    public DonViSuDungService(IDonViSuDungRepository donViSuDungRepository)
    {
        _donViSuDungRepository = donViSuDungRepository;
    }

    public async Task<int> GetDonViSuDungAsync( string currentDomain)
    { 
        var db = await _donViSuDungRepository.GetAsync(x => x.MaDonViSuDung == 182);
        return db?.MaDonViSuDung ?? 0;
    }
}