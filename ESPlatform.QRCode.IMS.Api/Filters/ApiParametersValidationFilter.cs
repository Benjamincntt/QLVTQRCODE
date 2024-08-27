using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESPlatform.QRCode.IMS.Api.Filters;

public class ApiParametersValidationFilter : IAsyncActionFilter {
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
		if (!context.ModelState.IsValid) {
			throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidApiParameters,
										  context.ModelState.SelectMany(x => x.Value?.Errors.Select(y => y.ErrorMessage) ?? Array.Empty<string>()));
		}

		await next();
	}
}
