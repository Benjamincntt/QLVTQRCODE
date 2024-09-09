using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Domain.Entities;
using Mapster;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KiemKe;

public class MappingRegister : IRegister {
	public void Register(TypeAdapterConfig config) {
		//config.NewConfig<SupplyListResponseItem, QlvtVatTu>().Compile();
		config.NewConfig<QlvtVatTu, SupplyListResponseItem>().Compile();
	}
}
