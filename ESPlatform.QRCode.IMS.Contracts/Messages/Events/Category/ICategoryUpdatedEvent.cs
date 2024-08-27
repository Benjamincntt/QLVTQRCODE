namespace ESPlatform.QRCode.IMS.Contracts.Messages.Events.Category;

public interface ICategoryUpdatedEvent : IMessage {
	Guid CategoryId { get; set; }
}
