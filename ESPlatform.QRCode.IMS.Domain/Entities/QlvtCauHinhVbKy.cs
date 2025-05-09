﻿namespace ESPlatform.QRCode.IMS.Domain.Entities;

public class QlvtCauHinhVbKy
{
    public short? Stt { get; set; }

    public int Id { get; set; }

    public string? MaDoiTuongKy { get; set; }

    public string? MaLoaiVanBan { get; set; }

    public int? SoLuongChuKy { get; set; }

    public bool? CoTheBoQua { get; set; }

    public string? IdDetect { get; set; }

    public string? TypeSign { get; set; }
}