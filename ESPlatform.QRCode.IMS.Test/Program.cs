
//using ESPlatform.QRCode.IMS.Test.Extensions;
using ESPlatform.QRCode.IMS.Test.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);


//builder.Services.RegisterMessageBroker();

builder.Services.AddHostedService<TestWorker>();

var host = builder.Build();
host.Run();
