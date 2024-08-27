using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Domain.Models.Accounts;

public class AccountListRequest : PhraseAndPagingFilter {
	public AccountStatus Status { get; set; }
}
