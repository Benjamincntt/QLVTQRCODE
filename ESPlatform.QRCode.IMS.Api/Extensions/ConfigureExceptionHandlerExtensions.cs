using System.Net;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace ESPlatform.QRCode.IMS.Api.Extensions;

public static class ConfigureExceptionHandlerExtensions {
	public static IApplicationBuilder HandleAppExceptions(this IApplicationBuilder app) {
		RequestDelegate handler = async context => {
			var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
			if (contextFeature == null) {
				return;
			}

			var exception = contextFeature.Error;
			var exceptionInfo = new ExceptionInfo {
				Message = exception.Message,
				StatusCode = exception switch {
					ArgumentException
					 or ValidationException
					 or FluentValidationException
					 or UnauthorizedAccessException
					 or BadRequestException
						=> HttpStatusCode.BadRequest,
					NotFoundException
						=> HttpStatusCode.NotFound,
					_
						=> HttpStatusCode.ServiceUnavailable
				},
				TraceId = context.TraceIdentifier
			};

			if (exception is IMultiErrorsException multiErrorsException) {
				exceptionInfo.Errors = multiErrorsException.Errors;
			}

			context.Response.ContentType = "application/json; charset=utf-8";
			context.Response.StatusCode = (int)exceptionInfo.StatusCode;

			await context.Response.WriteAsync(exceptionInfo.ToString());
		};

		return app.UseExceptionHandler(builder => builder.Run(handler));
	}
}

public class ExceptionInfo {
	public HttpStatusCode StatusCode { get; set; }

	public string Message { get; set; } = string.Empty;

	public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();

	public string? TraceId { get; set; }

	public override string ToString() {
		return this.ToJson();
	}
}
