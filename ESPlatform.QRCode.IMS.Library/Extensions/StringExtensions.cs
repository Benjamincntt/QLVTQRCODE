using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ESPlatform.QRCode.IMS.Library.Extensions;

public static class StringExtensions {
	private static readonly Regex RxUrlSlugWhitespaceToDash = new(@"[^\w\d]+", RegexOptions.Compiled);

	private static readonly Regex RxIsValidEmail = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

	private static readonly Regex RxWordCount = new(@"[^\s]+", RegexOptions.Multiline | RegexOptions.Compiled);

	public static DateTime ToDate(this string input) {
		return DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime) ? dateTime : DateTime.Today;
	}

	public static string ToBase64(this string input) {
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
	}

	public static string FromBase64(this string input) {
		return Encoding.UTF8.GetString(Convert.FromBase64String(input));
	}

	public static int ToInt(this string input, int defaultValue = 0) {
		return int.TryParse(input, out var result) ? result : defaultValue;
	}

	public static double ToDouble(this string input, double defaultValue = 0.0) {
		return double.TryParse(input, out var result) ? result : defaultValue;
	}

	public static float ToFloat(this string input, float defaultValue = 0.0f) {
		return float.TryParse(input, out var result) ? result : defaultValue;
	}

	public static long ToLong(this string input, long defaultValue = 0) {
		return long.TryParse(input, out var result) ? result : defaultValue;
	}

	public static bool ToBool(this string input, bool defaultValue = false) {
		return bool.TryParse(input, out var result) ? result : defaultValue;
	}

	public static Guid ToGuid(this string input, Guid defaultValue = default) {
		return Guid.TryParse(input, out var result) ? result : defaultValue;
	}

	public static string RegexReplace(this string input, Regex regex, string replacement) {
		return regex.Replace(input, replacement);
	}

	public static string RegexReplace(this string input, string pattern, RegexOptions options, string replacement) {
		return new Regex(pattern, options).Replace(input, replacement);
	}

	public static bool RegexMatch(this string input, Regex regex) {
		return regex.IsMatch(input);
	}

	public static bool RegexMatch(this string input, string pattern, RegexOptions options) {
		return new Regex(pattern, options).IsMatch(input);
	}

	public static string ToUrlSlug(this string input) {
		return input.ToAscii()
					.RegexReplace(RxUrlSlugWhitespaceToDash, "-")
					.Trim('-')
					.ToLower();
	}

	public static bool IsValidEmail(this string input) {
		return RxIsValidEmail.IsMatch(input);
	}

	public static int WordCount(this string input) {
		return new Regex(@"[^\\s]+").Matches(input)
									.Count;
	}
}
