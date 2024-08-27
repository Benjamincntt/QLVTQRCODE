using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ESPlatform.QRCode.IMS.Test.Workers;

public class TestWorker : BackgroundService {
	private readonly ILogger<TestWorker> _logger;

	public TestWorker(ILogger<TestWorker> logger) {
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		while (!stoppingToken.IsCancellationRequested) {
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			await Task.Delay(50000, stoppingToken);
		}
	}
}
