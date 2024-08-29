using ESPlatform.QRCode.IMS.Api.Extensions;
using ESPlatform.QRCode.IMS.Api.Middlewares;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using Microsoft.AspNetCore.Authentication;
using Serilog;
using AuthenticationMiddleware = ESPlatform.QRCode.IMS.Api.Middlewares.AuthenticationMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Load AppConfig
AppConfig.Instance.Load(builder.Configuration);

// Configure Serilog
builder.RegisterSerilog();

// Prepare services
builder.Services
	   .RegisterAppCoreServices()
	   .RegisterAuthenticationServices()
	  // .RegisterMessageBroker()
	   .RegisterOtherServices();

// Configure services
builder.Services
	   .ConfigureControllers()
	   .ConfigureSwaggerOptions()
	   .ConfigureNetworkOptions();

/*** BUILD APP ***/
// Order: Exception handlers => HSTS => HttpsRedirection => Static => Routing => CORS => Authentication => Authorization => Custom middlewares => Controllers
// Keep your app configuration code in this order to avoid unexpected errors

var app = builder.Build();
ServiceProviderHelper.ServiceProvider = app.Services;

// Configure Exception handlers
app.HandleAppExceptions();

// Configure HTTP request pipeline
app.UseHttpsRedirection()
   .UseRouting();

// Configure CORS
app.UseCors(Constants.Http.CorsPolicyName);

// Configure Authentication
app.UseAuthentication()
   .UseAuthorization();

// Configure Swagger middleware
//if (app.Environment.IsDevelopment()) {
app.UseSwagger()
   .UseSwaggerUI();
//}

// Configure Custom middlewares
app.UseMiddleware<PerformanceMiddleware>()
   .UseMiddleware<AuthenticationMiddleware>();

// Configure Controllers
app.MapControllers();

app.UseStaticFiles(); 

try {
	Log.Information("STARTED");
	await app.RunAsync();
}
catch (Exception exc) {
	Log.Fatal(exc, "App terminated unexpectedly");
}
finally {
	Log.Information("STOPPED");
	await Log.CloseAndFlushAsync();
}
