using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.DTOs.Viettel
{
    public class SignFileImgDto
    {
        public int numberPageSign { get; set; }
        public float coorX { get; set; }
        public float coorY { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public string? pathImage { get; set; }
        public string? MSSFormat { get; set; }
    }
}
