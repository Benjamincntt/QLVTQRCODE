namespace ESPlatform.QRCode.IMS.Core.Engine;

public static partial class Constants {
	public static class Validation {
		public const int StringDefaultMaximumLength = 1000;
		public const int MinPageSize = 1;
		public const int MaxPageSize = 100;
		public const int StrDateMaxLength = 25;
		public static class Messages {
			public const string MustEqual = "{PropertyName}: Giá trị phải bằng {ComparisonValue}";
			public const string MustGreaterThan = "{PropertyName}: phải lớn hơn {ComparisonValue}";
			public const string MustGreaterThanOrEqual = "{PropertyName}: phải lớn hơn hoặc bằng {ComparisonValue}";
			public const string MustLessThan = "{PropertyName}: phải nhỏ hơn {PropertyValue}";
			public const string MustLessThanOrEqual = "{PropertyName}: phải nhỏ hơn hoặc bằng {ComparisonValue}";
			public const string MustNotEmpty = "{PropertyName}: không được để trống";
			public const string MustNotNull = "{PropertyName}: Không chấp nhận giá trị null";
			public const string RequireMaximumLength = "{PropertyName}: độ dài tối đa là {MaxLength}";
			public const string RequireMinimumLength = "{PropertyName}: phải có ít nhất {MinLength} ký tự";
			public const string Invalid = "{PropertyName}: không hợp lệ";
			public const string MustWithinValueRange = "{PropertyName}: phải nằm trong khoảng từ {From} đến {To}";
		}
	}
}
