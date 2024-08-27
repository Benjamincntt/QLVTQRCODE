// using ESPlatform.QRCode.IMS.Core.Engine;
// using ESPlatform.QRCode.IMS.Core.Facades.Logs;
// using ESPlatform.QRCode.IMS.Library.Extensions;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
//
// namespace ESPlatform.QRCode.IMS.Api.Filters.Logging;
//
// public class ActionLogFilter : ActionLogFilterBase {
// 	private readonly ActionLogAttributeArguments _arguments;
//
// 	public ActionLogFilter(ILogFacade logFacade, ActionLogAttributeArguments arguments) : base(logFacade) {
// 		_arguments = arguments;
// 	}
//
// 	public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
// 		var objectId = !string.IsNullOrEmpty(_arguments.ObjectIdParameterName)
// 					&& context.ActionArguments.TryGetValue(_arguments.ObjectIdParameterName, out var objId)
// 						   ? objId as Guid? ?? Guid.Empty
// 						   : Guid.Empty;
//
// 		var info = new Dictionary<string, object?>();
//
// 		if (_arguments.IsLogParameters) {
// 			foreach (var (key, value) in context.ActionArguments) {
// 				info.Add(key, value);
// 			}
// 		}
//
// 		await next();
//
// 		var result = context.Result is OkObjectResult okObjResult ? okObjResult.Value : null;
// 		if (objectId == Guid.Empty && !string.IsNullOrWhiteSpace(_arguments.ObjectIdResultPropertyPath)) {
// 			objectId = _arguments.ObjectIdResultPropertyPath == "."
// 						   ? result as Guid? ?? Guid.Empty
// 						   : result.GetPropertyValueFromPath(_arguments.ObjectIdResultPropertyPath) as Guid? ?? Guid.Empty;
// 		}
//
// 		if (_arguments.IsLogResult) {
// 			info.Add("$result", result);
// 		}
//
// 		await LogFacade.LogAsync(Constants.Logging.GetLogAction(_arguments.LogAction), objectId, info.ToJson());
// 	}
// }
