using ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Responses;
using ESPlatform.QRCode.IMS.Domain.Entities;
using Mapster;

namespace ESPlatform.QRCode.IMS.Core.DTOs.Accounts;

public class MappingRegister : IRegister {
	public void Register(TypeAdapterConfig config) {
		config.NewConfig<AccountCreatedRequest, Account>().Compile();
		config.NewConfig<AccountListResponseItem, Account>().Compile();
		config.NewConfig<AccountResponse, Account>().Compile();
		config.NewConfig<AccountAuthorizedResponseItem, Account>().Compile();
	}
}
