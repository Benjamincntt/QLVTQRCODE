using MassTransit;

namespace ESPlatform.QRCode.IMS.Test.Consumers.Categories;

public class CategoryCreatedEventConsumerDefiniton : ConsumerDefinition<CategoryCreatedEventConsumer> {
	public CategoryCreatedEventConsumerDefiniton() {
		
	}
	protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CategoryCreatedEventConsumer> consumerConfigurator, IRegistrationContext context) {
		if (endpointConfigurator is not IRabbitMqReceiveEndpointConfigurator rmq) {
			return;
		}

		rmq.AutoDelete = false;
		rmq.Durable = true;
	}
}