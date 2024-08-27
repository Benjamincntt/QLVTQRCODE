// using ESPlatform.QRCode.IMS.Contracts.Messages.Events.Category;
// using MassTransit;
//
// namespace ESPlatform.QRCode.IMS.Core.Facades.Events.Categories;
//
// public class CategoryEventsFacade : ICategoryEventsFacade {
// 	private readonly IPublishEndpoint _publishEndpoint;
//
// 	public CategoryEventsFacade(IPublishEndpoint publishEndpoint) {
// 		_publishEndpoint = publishEndpoint;
// 	}
//
// 	public async Task RaiseCreatedEvent(Guid categoryId) {
// 		await _publishEndpoint.Publish<ICategoryCreatedEvent>(new {
// 			CategoryId = categoryId,
// 			OccurredTime = DateTimeOffset.Now
// 		});
// 	}
//
// 	public async Task RaiseModifiedEvent(Guid contentId) {
// 		await _publishEndpoint.Publish<ICategoryUpdatedEvent>(new {
// 			ContentId = contentId,
// 			OccurredTime = DateTimeOffset.Now
// 		});
// 	}
// 	
// }