namespace ESPlatform.QRCode.IMS.Library.Exceptions;

public interface IMultiErrorsException {
	IEnumerable<string> Errors { get; set; }
}
