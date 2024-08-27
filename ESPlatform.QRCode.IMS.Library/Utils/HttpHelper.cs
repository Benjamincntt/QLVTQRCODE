using System.Net;
using System.Net.Http.Headers;

// ReSharper disable MemberCanBePrivate.Global

namespace ESPlatform.QRCode.IMS.Library.Utils;

public static class HttpHelper {
	private static readonly HttpClient Client;

	static HttpHelper() {
		var cookies = new CookieContainer();

		var clientHandler = new HttpClientHandler {
			UseCookies = false,
			CookieContainer = cookies,
			AllowAutoRedirect = true,
			MaxAutomaticRedirections = 5,
			UseProxy = false,
			AutomaticDecompression = DecompressionMethods.All
		};

		Client = new HttpClient(clientHandler) {
			DefaultRequestVersion = HttpVersion.Version20,
			DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
			Timeout = TimeSpan.FromSeconds(5)
		};
		Client.DefaultRequestHeaders.UserAgent.TryParseAdd(HeaderUserAgent);
		Client.DefaultRequestHeaders.Accept.TryParseAdd(HeaderAccept);
		Client.DefaultRequestHeaders.AcceptEncoding.TryParseAdd(HeaderAcceptEncoding);
		Client.DefaultRequestHeaders.AcceptLanguage.TryParseAdd(HeaderAcceptLanguage);
		Client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
	}

	public static string HeaderUserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36";

	public static string HeaderAccept { get; set; } =
		"text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";

	public static string HeaderAcceptEncoding { get; set; } = "gzip, deflate, br";

	public static string HeaderAcceptLanguage { get; set; } = "en-US,en;q=0.9,vi;q=0.8";

	public static HttpRequestMessage CreateRequestMessage(HttpMethod method, string url, string referer, string cookies) {
		var request = new HttpRequestMessage(method, url);
		request.Headers.Referrer = new Uri(string.IsNullOrEmpty(referer) ? url : referer);
		request.Headers.TryAddWithoutValidation("Cookie", cookies);
		return request;
	}

	public static async Task<string> Get(HttpRequestMessage requestMessage) {
		using var response = await Client.SendAsync(requestMessage);
		if (!response.IsSuccessStatusCode) {
			return string.Empty;
		}

		return await response.Content.ReadAsStringAsync();
	}

	public static async Task<string> Get(string url, string referer = "", string cookies = "") {
		using var requestMessage = CreateRequestMessage(HttpMethod.Get, url, referer, cookies);
		return await Get(requestMessage);
	}

	public static async Task<byte[]> GetBytes(HttpRequestMessage requestMessage) {
		using var response = await Client.SendAsync(requestMessage);
		if (!response.IsSuccessStatusCode) {
			return Array.Empty<byte>();
		}

		return await response.Content.ReadAsByteArrayAsync();
	}

	public static async Task<byte[]> GetBytes(string url, string referer = "", string cookies = "") {
		using var requestMessage = CreateRequestMessage(HttpMethod.Get, url, referer, cookies);
		return await GetBytes(requestMessage);
	}
}