using MassTransit;

namespace ESPlatform.QRCode.IMS.Contracts.Extensions;

public static class ConfigureMessageTopologyExtensions {
	public static void ConfigureMessageTopology(this IRabbitMqBusFactoryConfigurator cfg) {
		/* Set the entity name formatter here if needed
		 * cfg.MessageTopology.SetEntityNameFormatter();
		 */

		/* Configure for DIRECT exchange here if needed
		 * cfg.Message<IBarCommand>(x => x.SetEntityName("bar")); // this line should be commented out
		 * cfg.Send<IBarCommand>(x => {
		 * 	x.UseCorrelationId(msg => msg.BarId);
		 * 	x.UseRoutingKeyFormatter(msgCtx => msgCtx.Message.Command);
		 * });
		 * cfg.Publish<IBarCommand>(x => { x.ExchangeType = ExchangeType.Direct; });
		 */
	}
}
