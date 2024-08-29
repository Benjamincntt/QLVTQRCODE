namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtAttachment
{
    public int Id { get; set; }

    public string AttachmentUrl { get; set; } = null!;

    public string? ThumbnailUrl { get; set; }

    public string FileName { get; set; } = null!;

    public long FileSize { get; set; }

    public string ContentType { get; set; } = null!;

    public bool IsTemporary { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }
}
