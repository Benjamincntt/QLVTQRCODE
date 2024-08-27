namespace ESPlatform.QRCode.IMS.Contracts.Messages.Events.Content;

public interface IContentRecalledEvent {
	Guid ContentId { get; set; }
}
