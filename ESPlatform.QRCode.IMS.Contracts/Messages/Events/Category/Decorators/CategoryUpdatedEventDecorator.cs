namespace ESPlatform.QRCode.IMS.Contracts.Messages.Events.Category.Decorators;

public class CategoryUpdatedEventDecorator : ICategoryUpdatedEvent {
	public Guid CategoryId { get; set; }

	public DateTimeOffset OccurredTime { get; set; } = DateTimeOffset.Now;
}
