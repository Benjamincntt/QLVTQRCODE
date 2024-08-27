using System.Runtime.CompilerServices;

namespace ESPlatform.QRCode.IMS.Infra.Context;

#pragma warning disable CA2255

public static class SqlServerDateTimeUtcFixup {
	[ModuleInitializer]
	public static void Initialize() {
		AppContext.SetSwitch("SqlServer.EnableLegacyTimestampBehavior", true);

		Console.WriteLine("SqlServer.EnableLegacyTimestampBehavior = true");
	}
}

#pragma warning restore CA2255