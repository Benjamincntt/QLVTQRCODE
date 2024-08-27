using System.Text.RegularExpressions;

namespace ESPlatform.QRCode.IMS.Library.Extensions;

public static class HtmlExtensions {
	public static string RemoveScripts(this string str) {
		return Regex.Replace(str, @"<script[^>]*>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
	}

	public static string RemoveAllHtmlTags(this string str) {
		return Regex.Replace(str, "<.*?>", string.Empty);
	}
}
