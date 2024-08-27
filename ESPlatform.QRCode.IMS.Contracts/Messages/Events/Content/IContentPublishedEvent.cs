namespace ESPlatform.QRCode.IMS.Contracts.Messages.Events.Content;

public interface IContentPublishedEvent {
	Guid ContentId { get; set; }
}
