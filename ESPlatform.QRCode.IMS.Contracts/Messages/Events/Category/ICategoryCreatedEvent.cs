namespace ESPlatform.QRCode.IMS.Contracts.Messages.Events.Category;

public interface ICategoryCreatedEvent : IMessage {
	Guid CategoryId { get; set; }
}
