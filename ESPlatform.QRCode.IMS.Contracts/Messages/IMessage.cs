using MassTransit;

namespace ESPlatform.QRCode.IMS.Contracts.Messages;

[ExcludeFromTopology]
public interface IMessage {
	DateTimeOffset OccurredTime { get; set; }
}
