using System.Security.Cryptography;
using System.Text;

namespace ESPlatform.QRCode.IMS.Library.Utils.Validation;

public class GetPassword
{
    [Obsolete("Obsolete")]
    public static string GetMD5(string input)
    {
        MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
        cryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(input));
        var hash = cryptoServiceProvider.Hash;
        var stringBuilder = new StringBuilder();
        if (hash == null) return stringBuilder.ToString();
        foreach (var t in hash)
            stringBuilder.Append(t.ToString("x2"));

        return stringBuilder.ToString();
    }
    public static string GetRandomLetters(int count)
    {
        const string text = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
        var stringBuilder = new StringBuilder();
        var random = new Random();
        for (var i = 0; i < count; i++)
        {
            stringBuilder.Append(random.Next(0, text.Length - 1));
        }

        return stringBuilder.ToString();
    }
    
}