using MassTransit;

namespace ESPlatform.QRCode.IMS.Test.Consumers;

public class ContentPublishedEventConsumerDefinition : ConsumerDefinition<ContentPublishedEventConsumer> {
	public ContentPublishedEventConsumerDefinition() {
		/* Configure Endpoint Name here if needed
		 * EndpointName = "test-app--foo-created-event"; // OR
		 * Endpoint(x => x.Name = "test-app--foo-created-event");
		 */
	}

	protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ContentPublishedEventConsumer> consumerConfigurator, IRegistrationContext context) {
		if (endpointConfigurator is not IRabbitMqReceiveEndpointConfigurator rmq) {
			return;
		}

		rmq.AutoDelete = false;
		rmq.Durable = true;
	}
}
