## EfCore Scaffold

```
 dotnet ef dbcontext scaffold "Data Source=192.168.68.249;User ID=dev.nmd_tm_24_qrcode_dev;Password=dev.nmd_tm_24_qrcode_dev@123;Initial Catalog=NMD_THACMO_24_QRCODE_DEV;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o _Scaffold -n ESPlatform.QRCode.IMS.Test._Scaffold -c AppDbContext --context-dir _Scaffold --context-namespace ESPlatform.QRCode.IMS.Test._Scaffold -f --no-onconfiguring --project E:\ESPlatform-qrcode-web-api\web-api\ESPlatform.QRCode.IMS.Test\ESPlatform.QRCode.IMS.Test.csproj

```

## Sample: appsettings.Development.json

```
{
	"JwtSettings:ExpireMinutes": 14400,
	"JwtSettings:RefreshExpireMinutes": 14400,
	"Logging": {
		"LogLevel": {
			"Default": "Information",
			"Microsoft.AspNetCore": "Warning"
		}
	}
}
```

## Sample: http-client.private.env.json

```
{
	"dev": {
		"host": "https://localhost:7273/api/v1/",
		"token": "YOUR_TOKEN_HERE",
		"siteId": "8f39e1c8-9b2b-499b-be9d-133b786c3ec8",
		"contentId": "728d7d52-3652-4ef6-8044-af449bf6db20"
	}
}
```
