using ESPlatform.QRCode.IMS.Test.Engine.Configuration;
//using ESPlatform.QRCode.IMS.Test.Extensions;
using ESPlatform.QRCode.IMS.Test.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
AppConfig.Instance.Load(builder.Configuration);

//builder.Services.RegisterMessageBroker();

builder.Services.AddHostedService<TestWorker>();

var host = builder.Build();
host.Run();
