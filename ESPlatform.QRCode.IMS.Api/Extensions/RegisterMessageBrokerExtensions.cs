// using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
// using ESPlatform.QRCode.IMS.Contracts.Extensions;
// using MassTransit;
//
// namespace ESPlatform.QRCode.IMS.Api.Extensions;
//
// public static class RegisterMessageBrokerExtensions {
// 	public static IServiceCollection RegisterMessageBroker(this IServiceCollection services) {
// 		services.AddMassTransit(x => {
// 			x.AddDelayedMessageScheduler();
// 			x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: AppConfig.Instance.RabbitMq.QueuePrefix, includeNamespace: false));
//
// 			x.UsingRabbitMq((ctx, cfg) => {
// 				cfg.Host(AppConfig.Instance.RabbitMq.Host,
// 						 AppConfig.Instance.RabbitMq.Port,
// 						 AppConfig.Instance.RabbitMq.VirtualHost,
// 						 h => {
// 							 h.Username(AppConfig.Instance.RabbitMq.Username);
// 							 h.Password(AppConfig.Instance.RabbitMq.Password);
// 						 });
//
// 				cfg.ConfigureMessageTopology();
// 				cfg.ConfigureEndpoints(ctx);
// 			});
// 		});
//
// 		return services;
// 	}
// }
