namespace ESPlatform.QRCode.IMS.Contracts.Messages.Events.Content;

public interface IContentUpdatedEvent {
	Guid ContentId { get; set; }
}
