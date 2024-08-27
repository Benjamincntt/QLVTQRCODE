namespace ESPlatform.QRCode.IMS.Core.Services.TbDonViSuDungs;

public interface IDonViSuDungService
{
    Task<int> GetDonViSuDungAsync( string currentDomain);
}