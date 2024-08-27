using ESPlatform.QRCode.IMS.Contracts.Messages.Events.Content;
using MassTransit;

namespace ESPlatform.QRCode.IMS.Test.Consumers;

public class ContentPublishedEventConsumer : IConsumer<IContentPublishedEvent> {
	public async Task Consume(ConsumeContext<IContentPublishedEvent> context) {
		await Task.Run(() => { Console.WriteLine($"EVENT RECEIVED: Content with Id: '{context.Message.ContentId}' PUBLISHED!"); });
	}

	/* Sample:
	 * public async Task Consume(ConsumeContext<IContentPublishedEvent> context) {
	 *	await...
	 * }
	 */
}
