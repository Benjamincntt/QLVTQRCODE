using ESPlatform.QRCode.IMS.Core.Engine.Logging;
using ESPlatform.QRCode.IMS.Domain.Enums;

// ReSharper disable MemberCanBePrivate.Global

namespace ESPlatform.QRCode.IMS.Core.Engine;

public static partial class Constants {
	public static class Logging {
		private static readonly LogAction Unknown = new() { Action = "unknown", ObjectType = LogObjectType.Unknown, Message = string.Empty };

		private static readonly Dictionary<string, LogAction> LogActionDictionary = new();

		private static LogAction CreateLogAction(string action, LogObjectType objectType, string message) {
			var logAction = new LogAction { Action = action, ObjectType = objectType, Message = message };
			LogActionDictionary.Add(action, logAction);
			return logAction;
		}

		public static LogAction GetLogAction(string action) {
			return LogActionDictionary.GetValueOrDefault(action, Unknown);
		}

		#region Account logs

		public const string AccountCreateAction = "account.create";
		public const string AccountModifyAction = "account.modify";

		public static readonly LogAction AccountCreateLogAction = CreateLogAction(AccountCreateAction, LogObjectType.Account, "Tạo tài khoản mới");
		public static readonly LogAction AccountModifyLogAction = CreateLogAction(AccountModifyAction, LogObjectType.Account, "Chỉnh sửa tài khoản");

		#endregion

		#region Author logs

		public const string AuthorCreateAction = "author.create";
		public const string AuthorModifyAction = "author.modify";

		public static readonly LogAction AuthorCreateLogAction = CreateLogAction(AuthorCreateAction, LogObjectType.Author, "Tạo tác giả mới");
		public static readonly LogAction AuthorModifyLogAction = CreateLogAction(AuthorModifyAction, LogObjectType.Author, "Chỉnh sửa tác giả");

		#endregion

		#region Category logs

		public const string CategoryCreateAction = "category.create";
		public const string CategoryModifyAction = "category.modify";

		public static readonly LogAction CategoryCreateLogAction = CreateLogAction(CategoryCreateAction, LogObjectType.Content, "Tạo chuyên mục mới");
		public static readonly LogAction CategoryModifyLogAction = CreateLogAction(CategoryModifyAction, LogObjectType.Content, "Chỉnh sửa chuyên mục");

		#endregion

		#region Content logs

		public const string ContentCreateAction = "content.create";
		public const string ContentModifyAction = "content.modify";

		// Writing
		public const string ContentWritingDeleteAction = "content.writing.delete";
		public const string ContentWritingArchiveAction = "content.writing.archive";
		public const string ContentWritingRestoreAction = "content.writing.restore";
		public const string ContentWritingTransferAction = "content.writing.transfer";
		public const string ContentWritingSubmitAction = "content.writing.submit";
		public const string ContentWritingPulloutAction = "content.writing.pullout";

		// Editing
		public const string ContentEditingHandleAction = "content.editing.handle";
		public const string ContentEditingCancelAction = "content.editing.cancel";
		public const string ContentEditingReturnAction = "content.editing.return";
		public const string ContentEditingTransferAction = "content.editing.transfer";
		public const string ContentEditingSubmitAction = "content.editing.submit";
		public const string ContentEditingPulloutAction = "content.editing.pullout";

		// Approving
		public const string ContentApprovingHandleAction = "content.approving.handle";
		public const string ContentApprovingCancelAction = "content.approving.cancel";
		public const string ContentApprovingReturnAction = "content.approving.return";
		public const string ContentApprovingTransferAction = "content.approving.transfer";
		public const string ContentApprovingSubmitAction = "content.approving.submit";
		public const string ContentApprovingPulloutAction = "content.approving.pullout";

		// Publishing
		public const string ContentPublishingHandleAction = "content.publishing.handle";
		public const string ContentPublishingCancelAction = "content.publishing.cancel";
		public const string ContentPublishingReturnAction = "content.publishing.return";
		public const string ContentPublishingTransferAction = "content.publishing.transfer";
		public const string ContentPublishingPublishAction = "content.publishing.publish";
		public const string ContentPublishingRecallAction = "content.publishing.recall";
		public const string ContentPublishingRemoveAction = "content.publishing.remove";

		public static readonly LogAction ContentCreateLogAction = CreateLogAction(ContentCreateAction, LogObjectType.Content, "Tạo tin bài mới");
		public static readonly LogAction ContentModifyLogAction = CreateLogAction(ContentModifyAction, LogObjectType.Content, "Chỉnh sửa tin bài");

		public static readonly LogAction ContentWritingDeleteLogAction = CreateLogAction(ContentWritingDeleteAction, LogObjectType.Content, "Xóa tin bài");
		public static readonly LogAction ContentWritingArchiveLogAction = CreateLogAction(ContentWritingArchiveAction, LogObjectType.Content, "Lưu kho tin bài");
		public static readonly LogAction ContentWritingRestoreLogAction = CreateLogAction(ContentWritingRestoreAction, LogObjectType.Content, "Phục hồi tin bài");
		public static readonly LogAction ContentWritingTransferLogAction = CreateLogAction(ContentWritingTransferAction, LogObjectType.Content, "Chuyển tin bài đồng cấp");
		public static readonly LogAction ContentWritingSubmitLogAction = CreateLogAction(ContentWritingSubmitAction, LogObjectType.Content, "Gửi tin bài");
		public static readonly LogAction ContentWritingPulloutLogAction = CreateLogAction(ContentWritingPulloutAction, LogObjectType.Content, "Rút tin bài đã gửi");

		public static readonly LogAction ContentEditingHandleLogAction = CreateLogAction(ContentEditingHandleAction, LogObjectType.Content, "Nhận biên tập tin bài");
		public static readonly LogAction ContentEditingCancelLogAction = CreateLogAction(ContentEditingCancelAction, LogObjectType.Content, "Dừng biên tập tin bài");
		public static readonly LogAction ContentEditingReturnLogAction = CreateLogAction(ContentEditingReturnAction, LogObjectType.Content, "Trả lại tin bài cho người viết");
		public static readonly LogAction ContentEditingTransferLogAction = CreateLogAction(ContentEditingTransferAction, LogObjectType.Content, "Chuyển tin bài đồng cấp");
		public static readonly LogAction ContentEditingSubmitLogAction = CreateLogAction(ContentEditingSubmitAction, LogObjectType.Content, "Gửi tin bài biên tập");
		public static readonly LogAction ContentEditingPulloutLogAction = CreateLogAction(ContentEditingPulloutAction, LogObjectType.Content, "Rút tin bài đã biên tập");

		public static readonly LogAction ContentApprovingHandleLogAction = CreateLogAction(ContentApprovingHandleAction, LogObjectType.Content, "Nhận phê duyệt tin bài");
		public static readonly LogAction ContentApprovingCancelLogAction = CreateLogAction(ContentApprovingCancelAction, LogObjectType.Content, "Dừng phê duyệt tin bài");
		public static readonly LogAction ContentApprovingReturnLogAction = CreateLogAction(ContentApprovingReturnAction, LogObjectType.Content, "Trả lại tin bài cho người biên tập");
		public static readonly LogAction ContentApprovingTransferLogAction = CreateLogAction(ContentApprovingTransferAction, LogObjectType.Content, "Chuyển tin bài đồng cấp");
		public static readonly LogAction ContentApprovingSubmitLogAction = CreateLogAction(ContentApprovingSubmitAction, LogObjectType.Content, "Gửi tin bài phê duyệt");
		public static readonly LogAction ContentApprovingPulloutLogAction = CreateLogAction(ContentApprovingPulloutAction, LogObjectType.Content, "Rút bài đã phê duyệt");

		public static readonly LogAction ContentPublishingHandleLogAction = CreateLogAction(ContentPublishingHandleAction, LogObjectType.Content, "Nhận phê duyệt tin bài");
		public static readonly LogAction ContentPublishingCancelLogAction = CreateLogAction(ContentPublishingCancelAction, LogObjectType.Content, "Dừng xuất bản tin bài");
		public static readonly LogAction ContentPublishingReturnLogAction = CreateLogAction(ContentPublishingReturnAction, LogObjectType.Content, "Trả lại tin bài cho người phê duyệt");
		public static readonly LogAction ContentPublishingTransferLogAction = CreateLogAction(ContentPublishingTransferAction, LogObjectType.Content, "Chuyển tin bài đồng cấp");
		public static readonly LogAction ContentPublishingPublishLogAction = CreateLogAction(ContentPublishingPublishAction, LogObjectType.Content, "Xuất bản tin bài");
		public static readonly LogAction ContentPublishingRecallLogAction = CreateLogAction(ContentPublishingRecallAction, LogObjectType.Content, "Gỡ tin bài đã xuất bản");
		public static readonly LogAction ContentPublishingRemoveLogAction = CreateLogAction(ContentPublishingRemoveAction, LogObjectType.Content, "Xóa tin bài");

		#endregion

		#region Lineup logs

		public const string LineupCreateAction = "lineup.create";
		public const string LineupModifyAction = "lineup.modify";
		public const string LineupItemAddAction = "lineup.item.add";
		public const string LineupItemModifyAction = "lineup.item.modify";

		public static readonly LogAction LineupCreateLogAction = CreateLogAction(LineupCreateAction, LogObjectType.Lineup, "Tạo danh sách mới");
		public static readonly LogAction LineupModifyLogAction = CreateLogAction(LineupModifyAction, LogObjectType.Lineup, "Chỉnh sửa danh sách");
		public static readonly LogAction LineupItemAddLogAction = CreateLogAction(LineupItemAddAction, LogObjectType.Lineup, "Thêm phần tử vào danh sách");
		public static readonly LogAction LineupItemModifyLogAction = CreateLogAction(LineupItemModifyAction, LogObjectType.Lineup, "Chỉnh sửa phần tử trong danh sách");

		#endregion

		#region Topic logs

		public const string TopicCreateAction = "topic.create";
		public const string TopicModifyAction = "topic.modify";
		public const string TopicDeleteAction = "topic.delete";
		public const string TopicContentAddAction = "topic.content.add";
		public const string TopicContentModifyAction = "topic.content.modify";
		public const string TopicContentRemoveAction = "topic.content.delete";

		public static readonly LogAction TopicCreateLogAction = CreateLogAction(TopicCreateAction, LogObjectType.Topic, "Tạo chủ đề mới");
		public static readonly LogAction TopicModifyLogAction = CreateLogAction(TopicModifyAction, LogObjectType.Topic, "Sửa chủ đề");
		public static readonly LogAction TopicDeleteLogAction = CreateLogAction(TopicDeleteAction, LogObjectType.Topic, "Xóa chủ đề");
		public static readonly LogAction TopicContentAddLogAction = CreateLogAction(TopicContentAddAction, LogObjectType.Topic, "Thêm tin bài vào chủ đề");
		public static readonly LogAction TopicContentModifyLogAction = CreateLogAction(TopicContentModifyAction, LogObjectType.Topic, "Chỉnh sửa tin bài trong chủ đề");
		public static readonly LogAction TopicContentRemoveLogAction = CreateLogAction(TopicContentRemoveAction, LogObjectType.Topic, "Loại bỏ tin bài trong chủ đề");

		#endregion
	}
}
