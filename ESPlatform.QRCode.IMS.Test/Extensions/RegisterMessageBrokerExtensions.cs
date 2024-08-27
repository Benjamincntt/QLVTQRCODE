// using System.Reflection;
// using ESPlatform.QRCode.IMS.Contracts.Extensions;
// using ESPlatform.QRCode.IMS.Test.Engine.Configuration;
// using ESPlatform.QRCode.IMS.Test.Engine;
// using MassTransit;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace ESPlatform.QRCode.IMS.Test.Extensions;
//
// public static class RegisterMessageBrokerExtensions {
// 	public static IServiceCollection RegisterMessageBroker(this IServiceCollection services) {
// 		services.AddMassTransit(x => {
// 			x.AddDelayedMessageScheduler();
// 			x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: AppConfig.Instance.RabbitMq.QueuePrefix, includeNamespace: false));
//
// 			x.AddConsumers(Assembly.GetExecutingAssembly());
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
