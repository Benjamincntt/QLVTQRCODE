using System.Diagnostics;

namespace ESPlatform.QRCode.IMS.Api.Middlewares;

public class PerformanceMiddleware {
	private readonly RequestDelegate _next;

	public PerformanceMiddleware(RequestDelegate next) {
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context) {
		var stopWatch = Stopwatch.StartNew();

		context.Response.OnStarting(state => {
			var elapsed = stopWatch.ElapsedMilliseconds;
			stopWatch.Stop();

			var httpContext = (HttpContext)state;
			httpContext.Response.Headers.Add("X-Response-Time-Milliseconds", new[] { elapsed.ToString() });

			return Task.CompletedTask;
		}, context);

		await _next(context);
	}
}
