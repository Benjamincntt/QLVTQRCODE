using ESPlatform.QRCode.IMS.Library.Extensions;
using Microsoft.Extensions.Hosting;

namespace ESPlatform.QRCode.IMS.Test.Workers;

public class Test3 : BackgroundService {
	private ClsA _a = null!;

	protected override Task ExecuteAsync(CancellationToken stoppingToken) {
		_a = new ClsA {
			B = {
				Name = "This is a test OK"
			}
		};
		Console.WriteLine($"Result: {_a.GetPropertyValueFromPath("B.Name")}");

		return Task.CompletedTask;
	}

	public class ClsA {
		public ClsB B { get; set; } = new();
	}

	public class ClsB {
		public string Name { get; set; } = "This is a test";
	}
}
