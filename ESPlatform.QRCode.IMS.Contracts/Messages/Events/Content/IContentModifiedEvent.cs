namespace ESPlatform.QRCode.IMS.Contracts.Messages.Events.Content;

public interface IContentModifiedEvent {
	Guid ContentId { get; set; }
}