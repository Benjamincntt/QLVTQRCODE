// using Microsoft.AspNetCore.Mvc;
//
// namespace ESPlatform.QRCode.IMS.Api.Filters.Logging;
//
// [AttributeUsage(AttributeTargets.Method)]
// public class ActionLogAttribute : TypeFilterAttribute {
// 	public ActionLogAttribute(string logAction,
// 							  string objectIdParameterName = "",
// 							  string objectIdResultPropertyPath = "",
// 							  bool isLogParameters = false,
// 							  bool isLogResult = false) : base(typeof(ActionLogFilter)) {
// 		Arguments = new object[] { new ActionLogAttributeArguments(logAction, objectIdParameterName, objectIdResultPropertyPath, isLogParameters, isLogResult) };
// 	}
// }