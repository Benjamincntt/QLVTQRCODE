using ESPlatform.QRCode.IMS.Library.Utils.Mappers;
using Microsoft.Extensions.Hosting;

namespace ESPlatform.QRCode.IMS.Test.Workers;

public class Test1 : BackgroundService {
	protected override Task ExecuteAsync(CancellationToken stoppingToken) {
		var a = new A {
			TestVal1 = Guid.NewGuid(),
			TestVal2 = "something here",
			TestVal5 = 10
		};

		var b = new B();
		SimpleMapper.Map(a, b);

		Console.WriteLine(b.TestVal1);
		Console.WriteLine(b.TestVal2);

		return Task.CompletedTask;
	}

	private class A {
		public Guid TestVal1 { get; set; }

		public string TestVal2 { get; set; } = string.Empty;

		public int TestVal5 { get; set; }
	}

	private class B {
		public Guid TestVal1 { get; set; }

		public string TestVal2 { get; set; } = string.Empty;

		public int TestVal3 { get; set; }
	}
}
