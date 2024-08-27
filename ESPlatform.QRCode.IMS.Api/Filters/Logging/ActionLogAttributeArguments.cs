namespace ESPlatform.QRCode.IMS.Api.Filters.Logging;

public class ActionLogAttributeArguments {
	public ActionLogAttributeArguments(string logAction,
									   string objectIdParameterName = "",
									   string objectIdResultPropertyPath = "",
									   bool isLogParameters = false,
									   bool isLogResult = false) {
		LogAction = logAction;
		ObjectIdParameterName = objectIdParameterName;
		ObjectIdResultPropertyPath = objectIdResultPropertyPath;
		IsLogParameters = isLogParameters;
		IsLogResult = isLogResult;
	}

	public string LogAction { get; set; }

	public string ObjectIdParameterName { get; set; }

	public string ObjectIdResultPropertyPath { get; set; }

	public bool IsLogParameters { get; set; }

	public bool IsLogResult { get; set; }
}