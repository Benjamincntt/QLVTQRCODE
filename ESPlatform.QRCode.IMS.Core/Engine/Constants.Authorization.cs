namespace ESPlatform.QRCode.IMS.Core.Engine;

public static partial class Constants {
	public static class Authorization {
		public static class Roles {
			public const string Admin = "cdbe09c2-1017-4076-a153-5fbd670d0c8d";
			public const string Approver = "7b150633-a743-40e5-a2ac-d5e6f86fb37b";
			public const string Editor = "d42ec4cb-89f4-4081-a710-e95928645933";
			public const string Publisher = "bdbc4888-7637-4747-a9ce-1b650086acf4";
			public const string Writer = "fe1234e4-8a48-4f5e-bb70-b325524f985d";
		}

		public static class GuidRoles {
			public static readonly Guid Admin = new(Roles.Admin);
			public static readonly Guid Approver = new(Roles.Approver);
			public static readonly Guid Editor = new(Roles.Editor);
			public static readonly Guid Publisher = new(Roles.Publisher);
			public static readonly Guid Writer = new(Roles.Writer);
		}
	}
}
