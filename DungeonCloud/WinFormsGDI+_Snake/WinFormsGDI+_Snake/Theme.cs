
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Color = System.Drawing.Color;

namespace WinFormsGDI__Snake
{
    class Theme
    {

        public string Name;
        public Color Segment;
        public Color Head;
        public Color Background;
        public Color WindowBackgroundColor1;
        public Color Food;
        public Theme()
        {

        }
        public Theme(string name, string background, string winBgColor1, string food, string segment, string head)
        {
            ColorConverter cc = new ColorConverter();
            Name = name;
            Segment = (Color)cc.ConvertFromString(segment);
            Head = (Color)cc.ConvertFromString(head);
            Background = (Color)cc.ConvertFromString(background);
            WindowBackgroundColor1 = (Color)cc.ConvertFromString(winBgColor1);
            Food = (Color)cc.ConvertFromString(food);
        }


        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
