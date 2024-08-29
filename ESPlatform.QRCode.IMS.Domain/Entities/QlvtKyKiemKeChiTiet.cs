﻿namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtKyKiemKeChiTiet
{
    public int TheId { get; set; }

    public int? KyKiemKeId { get; set; }

    public int? VatTuId { get; set; }

    public int? KhoChinhId { get; set; }

    public int? KhoPhuId { get; set; }

    public decimal? SoLuongSoSach { get; set; }

    public decimal? SoLuongKiemKe { get; set; }

    public decimal? SoLuongChenhLech { get; set; }

    public string? SoThe { get; set; }
}
