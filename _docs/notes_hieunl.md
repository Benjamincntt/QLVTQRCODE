## Sample: http-client.private.env.json
{
"dev": {
"host": "https://localhost:7001/api/v1/",

    "siteId": "8f39e1c8-9b2b-499b-be9d-133b786c3ec8",
    "vatTuId": "5",
    "maVatTu": "010",
    "idViTri": "3",
    "kikiemkeId": "11",
    "parentId": 1,
    "kyKiemKeChiTietId": 1,
    
    "username": "hieunl",
    "password": "123456",

    "securityKey": "$2a$11$AqolUcwA9/UldCRDxWt/Juf20wLRnWA/YDp1fdqwln3F.UF8BHG1u",
    "otp": "795734",

    "accessToken": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyMzc0OCIsInVuaXF1ZV9uYW1lIjoiaGlldW5sIiwibmJmIjoxNzI0ODMwMjUwLCJleHAiOjE3MjkxNTAyNTAsImlhdCI6MTcyNDgzMDI1MCwiaXNzIjoiaHR0cHM6Ly9oZW1lcmEudm4vIiwiYXVkIjoiaHR0cHM6Ly90aWVucGhvbmcudm4vIn0.JXY3dueTsSZZYOWa3BqFO-PIxsuEh9Gnxj0LUwUs1SG6qj7RbwnViMByIPvMk2V3TICKTluEms85pTwT--wo8A",
    "refreshToken": "4WkKc0vD5CaiBv5bzyOnc5M+ipqOiSHRs77dcOV2J1kvX1HHudPxB82INFsCBkW74FKQwxQe5d6kgjcaB1mm9g=="

    }
}
## code gen db from terminal
dotnet ef dbcontext scaffold "Data Source=192.168.68.249;User ID=dev.nmd_tm_24_qrcode_dev;Password=dev.nmd_tm_24_qrcode_dev@123;Initial Catalog=NMD_THACMO_24_QRCODE_DEV;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o _Scaffold -n ESPlatform.QRCode.IMS.Test._Scaffold -c AppDbContext --context-dir _Scaffold --context-namespace ESPlatform.QRCode.IMS.Test._Scaffold -f --no-onconfiguring --project E:\ESPlatform-qrcode-web-api\web-api\ESPlatform.QRCode.IMS.Test\ESPlatform.QRCode.IMS.Test.csproj
