// using ESPlatform.QRCode.IMS.Core.Engine.Logging;
// using ESPlatform.QRCode.IMS.Core.Facades.Context;
// using ESPlatform.QRCode.IMS.Domain.Entities;
// using ESPlatform.QRCode.IMS.Domain.Enums;
// using ESPlatform.QRCode.IMS.Domain.Interfaces;
//
// namespace ESPlatform.QRCode.IMS.Core.Facades.Logs;
//
// public class LogFacade : ILogFacade {
// 	
// 	private readonly ILogRepository _logRepository;
//
// 	public LogFacade(ILogRepository logRepository, IAuthorizedContextFacade authorizedContextFacade) {
// 		_logRepository = logRepository;
// 		_authorizedContextFacade = authorizedContextFacade;
// 	}
//
// 	public async Task<Guid> LogAsync(string action, Guid objectId, LogObjectType objectType, string message, string jsonInfo = "{}") {
// 		var log = new Log {
// 			LogId = Guid.NewGuid(),
// 			AccountId = _authorizedContextFacade.AccountId,
// 			Time = DateTime.Now,
// 			Action = action,
// 			ObjectId = objectId,
// 			ObjectType = objectType,
// 			Message = message,
// 			Info = jsonInfo
// 		};
// 		return await _logRepository.InsertAsync(log) > 0 ? log.LogId : Guid.Empty;
// 	}
//
// 	public async Task<Guid> LogAsync(LogAction action, Guid objectId, string jsonInfo = "{}") {
// 		return await LogAsync(action.Action, objectId, action.ObjectType, action.Message, jsonInfo);
// 	}
// }
