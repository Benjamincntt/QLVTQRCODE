using ESPlatform.QRCode.IMS.Contracts.Messages.Events.Category;
using MassTransit;

namespace ESPlatform.QRCode.IMS.Test.Consumers.Categories;

public class CategoryCreatedEventConsumer : IConsumer<ICategoryCreatedEvent> {
	public async Task Consume(ConsumeContext<ICategoryCreatedEvent> context) {
		await Task.Run(() => {
			Console.WriteLine($"EVENT RECEIVED: Category with Id: '{context.Message.CategoryId}' CREATED!");
		});
	}
}