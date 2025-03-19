using Microsoft.Extensions.Configuration;

namespace ESPlatform.QRCode.IMS.Core.Engine.Configuration;

public class AppConfig {
	/* Settings */

	public ConnectionStrings ConnectionStrings { get; set; } = new();

	public IEnumerable<string> AllowedDomains { get; set; } = Enumerable.Empty<string>();
    

	public JwtSettings JwtSettings { get; set; } = new();

	public OtpSettings OtpSettings { get; set; } = new();
	
	public ImagePath ImagePath { get; set; }
    
    public KySoPathVersion2 KySoPathVersion2 { get; set; }

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
