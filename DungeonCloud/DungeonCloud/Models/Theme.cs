using Caliburn.Micro;
using DungeonCloud.Infrastructure;
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
    class Theme : PropertyChangedBase
    {

        private string name = "Default";
        private string Dir;
        public string Name
        {
            set
            {
                name = value;
                NotifyOfPropertyChange();
            }
            get => name;
        }

        private string navigationButtonColor = "White";
        public string NavigationButtonColor
        {
            set
            {
                navigationButtonColor = value;
                NotifyOfPropertyChange();
            }
            get => navigationButtonColor;
        }

        private string navigationButtonPressedColor = "Black";
        public string NavigationButtonPressedColor
        {
            set
            {
                navigationButtonPressedColor = value;
                NotifyOfPropertyChange();
            }
            get => navigationButtonPressedColor;
        }

        private string slideBarBackground = "#2a2930";
        public string SlideBarBackground
        {
            set
            {
                slideBarBackground = value;
                NotifyOfPropertyChange();
            }
            get => slideBarBackground;
        }

        private string windowBackgroundColor1 = "";
        public string WindowBackgroundColor1
        {
            set
            {
                windowBackgroundColor1 = value;
                NotifyOfPropertyChange();
            }
            get => windowBackgroundColor1;
        }
        private string windowBackgroundColor2 = "";
        public string WindowBackgroundColor2
        {
            set
            {
                windowBackgroundColor2 = value;
                NotifyOfPropertyChange();
            }
            get => windowBackgroundColor2;
        }

        public Theme()
        {

        }
        public Theme(string name, string navButtonColor, string navButtonPressedColor, string slideBarBackground, string winBgColor1, string winBgColor2, string dir)
        {
            Name = name;
            NavigationButtonColor = navButtonColor;
            NavigationButtonPressedColor = navButtonPressedColor;
            SlideBarBackground = slideBarBackground;
            WindowBackgroundColor1 = winBgColor1;
            WindowBackgroundColor2 = winBgColor2;
            Dir = dir;
        }
        public void SetColors()
        {
            ButtonColoring("main.png");
            ButtonColoring("settings.png");
            ButtonColoring("reg.png");
        }


        public void ButtonColoring(string path)
        {
            Bitmap bitmap = new Bitmap(Dir + "\\Default\\" + path);

            Color c = HexToRGB.HexToColor(NavigationButtonColor);

            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, Color.FromArgb(color.A, c.R, c.G, c.B));
                }

            bitmap.Save(Dir + "\\Processed\\" + path, ImageFormat.Png);

            bitmap = new Bitmap(Dir + "\\Default\\" + path);

            c = HexToRGB.HexToColor(NavigationButtonPressedColor);

            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, Color.FromArgb(color.A, c.R, c.G, c.B));
                }

            bitmap.Save(Dir + "\\Processed\\" + path.Substring(0, path.LastIndexOf('.')) + "-pressed.png", ImageFormat.Png);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
