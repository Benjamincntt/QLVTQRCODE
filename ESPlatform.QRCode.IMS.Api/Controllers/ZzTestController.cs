using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Infra.Context;
using Microsoft.AspNetCore.Mvc;

namespace ESPlatform.QRCode.IMS.Api.Controllers;

[Route("/api/v1/test")]
public class ZzTestController : ApiControllerBase {
	private AppDbContext DbContext { get; }

	public ZzTestController(AppDbContext dbContext) {
		DbContext = dbContext;
	}

	// [HttpGet("gen-secret-key")]
	// [AllowAnonymous]
	// public Task<IActionResult> GetSecretKeyAsync() {
	// 	return Task.FromResult<IActionResult>(Ok(OtpFacade.GenerateSecretKey()));
	// }

	// [HttpGet("test1")]
	// [AllowAnonymous]
	// public async Task<PagedList<dynamic>> RunTest1Async() {
	// 	var query = DbContext
	// 			   .Categories
	// 			   .Join(DbContext.Sites,
	// 					 x => x.SiteId,
	// 					 y => y.SiteId,
	// 					 (x, y) => new { Category = x, Site = y })
	// 			   .Where(x => x.Category.Name != string.Empty)
	// 			   .Where(x => x.Site.Name != string.Empty)
	// 			   .Select(x => new {
	// 					x.Category.CategoryId,
	// 					x.Category.Name,
	// 					x.Category.SiteId,
	// 					SiteName = x.Site.Name,
	// 					x.Category.ParentId
	// 				});
	//
	// 	return await query.ToPagedListAsync<dynamic>(1, 20);
	// }

	// [HttpGet("test2")]
	// [AllowAnonymous]
	// public IActionResult RunTest2Async([FromQuery] Test2Request request) {
	// 	var name = request.Name ?? "NULL";
	// 	var age = request.Age == null ? "NULL" : request.Age.ToString();
	//
	// 	return Ok($"Name {name} - Age {age}");
	// }
	//
	// [HttpGet("test3")]
	// [AllowAnonymous]
	// public IActionResult RunTest3Async() {
	// 	Thread.Sleep(1000);
	// 	return Ok();
	// }
	//
	// [HttpGet("{testId:guid}")]
	// [AllowAnonymous]
	// public IActionResult RunTest4Async(Guid testId) {
	// 	return Ok($"testId = {testId}");
	// }
	//
	// [HttpGet("test5")]
	// [AllowAnonymous]
	// public IActionResult RunTest5Async([FromQuery] Test5Enum[] items) {
	// 	return Ok($"test5 = {string.Join(",", items)}");
	// }
	//
	// [HttpGet("test6")]
	// [AllowAnonymous]
	// public IActionResult RunTest6Async([FromQuery] Test5Enum items) {
	// 	return Ok($"test6 = {items}");
	// }
	//
	// [HttpGet("test7")]
	// [AllowAnonymous]
	// public IActionResult RunTest7Async([FromQuery] DateTimeOffset dtofs) {
	// 	return Ok(new { Result = dtofs });
	// }
}

[Flags]
public enum Test5Enum {
	Unknown = 0,
	Known = 1,
	UnknownButKnown = 2,
	KnownButUnknown = 4
}

public class Test2Request {
	public string? Name { get; set; } = string.Empty;

	public int? Age { get; set; } = 10;
}
