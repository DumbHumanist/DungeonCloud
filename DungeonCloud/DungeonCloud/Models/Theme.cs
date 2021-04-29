using Caliburn.Micro;
using DungeonCloud.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

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
        public Theme(string name, string slideBarBackground, string winBgColor1, string winBgColor2, string navButtonColor, string navButtonPressedColor, string dir)
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
            ThemeSingleton.Instance.MainImageDefault = ToBitmapImage(ButtonColoring("main.png", NavigationButtonColor));
            ThemeSingleton.Instance.SettingsImageDefault = ToBitmapImage(ButtonColoring("settings.png", NavigationButtonColor));
            ThemeSingleton.Instance.RegImageDefault = ToBitmapImage(ButtonColoring("reg.png", NavigationButtonColor));
            ThemeSingleton.Instance.MainImagePressed = ToBitmapImage(ButtonBgColoring("main.png"));
            ThemeSingleton.Instance.SettingsImagePressed = ToBitmapImage(ButtonBgColoring("settings.png"));
            ThemeSingleton.Instance.RegImagePressed = ToBitmapImage(ButtonBgColoring("reg.png"));

            ThemeSingleton.Instance.BackImage = ToBitmapImage(ButtonColoring("back.png", SlideBarBackground));
            ThemeSingleton.Instance.DownloadImage = ToBitmapImage(ButtonColoring("download.png", SlideBarBackground));
            ThemeSingleton.Instance.UploadImage = ToBitmapImage(ButtonColoring("upload.png", SlideBarBackground));
            ThemeSingleton.Instance.DeleteImage = ToBitmapImage(ButtonColoring("delete.png", SlideBarBackground));
            ThemeSingleton.Instance.FolderImage = ToBitmapImage(ButtonColoring("folder.png", SlideBarBackground));

        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }


        public Bitmap ButtonColoring(string path, string clr)
        {
            Bitmap bitmap = new Bitmap(Dir + "\\Default\\" + path);

            Color c = HexToRGB.HexToColor(clr);

            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, Color.FromArgb(color.A, c.R, c.G, c.B));
                }

            return bitmap;
        }


        public Bitmap ButtonBgColoring(string path)
        {

            Bitmap bitmap = new Bitmap(Dir + "\\Default\\" + path);

            Color c = HexToRGB.HexToColor(NavigationButtonPressedColor);

            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, Color.FromArgb(color.A, c.R, c.G, c.B));
                }

            return bitmap;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
