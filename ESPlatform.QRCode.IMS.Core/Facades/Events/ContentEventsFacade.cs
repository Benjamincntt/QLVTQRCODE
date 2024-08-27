// using ESPlatform.QRCode.IMS.Contracts.Messages.Events.Content;
// using MassTransit;
//
// namespace ESPlatform.QRCode.IMS.Core.Facades.Events;
//
// public class ContentEventsFacade : IContentEventsFacade {
// 	private readonly IPublishEndpoint _publishEndpoint;
//
// 	public ContentEventsFacade(IPublishEndpoint publishEndpoint) {
// 		_publishEndpoint = publishEndpoint;
// 	}
//
// 	public async Task RaisePublishedEvent(Guid contentId) {
// 		await _publishEndpoint.Publish<IContentPublishedEvent>(new {
// 			ContentId = contentId,
// 			OccurredTime = DateTimeOffset.Now
// 		});
// 	}
//
// 	public async Task RaiseRecalledEvent(Guid contentId) {
// 		await _publishEndpoint.Publish<IContentRecalledEvent>(new {
// 			ContentId = contentId,
// 			OccurredTime = DateTimeOffset.Now
// 		});
// 	}
//
// 	public async Task RaiseUpdatedEvent(Guid contentId) {
// 		await _publishEndpoint.Publish<IContentUpdatedEvent>(new {
// 			ContentId = contentId,
// 			OccurredTime = DateTimeOffset.Now
// 		});
// 	}
//
// 	public async Task RaiseCreatedEvent(Guid contentId) {
// 		await _publishEndpoint.Publish<IContentCreatedEvent>(new {
// 			ContentId = contentId,
// 			OccurredTime = DateTimeOffset.Now
// 		});
// 	}
// }
