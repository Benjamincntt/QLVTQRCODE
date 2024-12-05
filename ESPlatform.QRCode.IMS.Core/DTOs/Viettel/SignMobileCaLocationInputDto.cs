using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.DTOs.Viettel
{
    public class SignMobileCaLocationInputDto
    {
        public int numberPageSign { get; set; }
        public float coorX { get; set; }
        public float coorY { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public string displayText { get; set; } = null!;
        public string formatRectangleText { get; set; } = null!;
        public string? reason { get; set; }
        public string? location { get; set; }
        public string dateFormatstring { get; set; } = null!;
        public string? MSSFormat { get; set; }
    }
}
