using Microsoft.Extensions.Hosting;

namespace ESPlatform.QRCode.IMS.Test.Workers;

public class Test2 : BackgroundService {
	private List<A> _listA = null!;

	private List<B> _listB = null!;

	protected override Task ExecuteAsync(CancellationToken stoppingToken) {
		_listA = new List<A> {
			new() { Id = 1, AValue = "A1" },
			new() { Id = 2, AValue = "A2" },
			new() { Id = 3, AValue = "A3" }
		};

		_listB = new List<B> {
			new() { Id = 1, BValue = "B1" },
			new() { Id = 1, BValue = "B11" },
			new() { Id = 2, BValue = "B2" },
			new() { Id = 2, BValue = "B22" }
		};

		var result = _listA.Join(_listB, a => a.Id, b => b.Id, (a, b) => new { a.AValue, b.BValue });
		foreach (var item in result) {
			Console.WriteLine($"{item.AValue} - {item.BValue}");
		}

		return Task.CompletedTask;
	}

	private class A {
		public int Id { get; set; }

		public string AValue { get; set; } = string.Empty;
	}

	private class B {
		public int Id { get; set; }

		public string BValue { get; set; } = string.Empty;
	}
}
