using Microsoft.Extensions.Configuration;

namespace ESPlatform.QRCode.IMS.Test.Engine.Configuration;

public class AppConfig {
	/* Settings */

	public IEnumerable<string> AllowedDomains { get; set; } = Enumerable.Empty<string>();

	//public RabbitMqSettings RabbitMq { get; set; } = new();

	/* END Settings */

	#region Init and Bind configurations

	public static readonly AppConfig Instance = new();

	private IConfiguration? _config;

	private AppConfig() { }

	public void Load(IConfiguration config) {
		_config = config;
		_config.Bind(this);
	}

	#endregion
}
