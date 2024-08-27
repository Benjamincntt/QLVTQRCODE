namespace ESPlatform.QRCode.IMS.Contracts.Messages.Events.Content;

public interface IContentCreatedEvent {
	Guid ContentId { get; set; }
}