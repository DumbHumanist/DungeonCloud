using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Models
{
    class ImageColoring
    {

        public enum RGB { Blue, Green, Red };

        public byte[] SetColor()
        {
            byte[] bitmap = BmsEngine.GetRgbData();
            for (int i = 0; i < BmsEngine.dataLength; i += 4)
            {
                bitmap[i + (int)RGB.Blue] = 1;    //blue
                bitmap[i + (int)RGB.Green] = 1;   //green;
            }
            return bitmap;
            /*
            using (Image image = Image.FromStream(new MemoryStream(bitmap)))
            {
                image.Save("output.png", ImageFormat.Png);  // Or Png
            }*/
        }
    }
}
