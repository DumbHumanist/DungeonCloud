using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DungeonCloud.Models
{
    class ThemeSingleton : PropertyChangedBase
    {
        static ThemeSingleton instance;
        public Theme currentTheme = new Theme();

        public Theme CurrentTheme
        {
            set
            {
                currentTheme = value;

                currentTheme.SetColors();
                NotifyOfPropertyChange();

                MainImage = MainImageDefault;
                SettingsImage = SettingsImageDefault;
                RegImage = RegImageDefault;
            }
            get => currentTheme;
        }

        ObservableCollection<Theme> themes = new ObservableCollection<Theme>();
        public ObservableCollection<Theme> Themes
        {
            set
            {
                themes = value;
                NotifyOfPropertyChange();
            }
            get => themes;
        }

        public ImageSource MainImageDefault;
        public ImageSource MainImagePressed;

        ImageSource mainImage;
        public ImageSource MainImage
        {
            set
            {
                mainImage = value;
                NotifyOfPropertyChange();
            }
            get => mainImage;
        }

        public ImageSource SettingsImageDefault;
        public ImageSource SettingsImagePressed;

        ImageSource settingsImage;
        public ImageSource SettingsImage
        {
            set
            {
                settingsImage = value;
                NotifyOfPropertyChange();
            }
            get => settingsImage;
        }

        public ImageSource RegImageDefault;
        public ImageSource RegImagePressed;

        ImageSource regImage;
        public ImageSource RegImage
        {
            set
            {
                regImage = value;
                NotifyOfPropertyChange();
            }
            get => regImage;
        }

        string Dir;
        public static ThemeSingleton Instance => instance ?? (instance = new ThemeSingleton());

        private ThemeSingleton()
        {
        }

        ~ThemeSingleton()
        {
            SaveThemes();
        }


        public void SaveThemes()
        {
            File.Delete($"{Dir}\\Themes\\currentTheme.txt");
            using (StreamWriter sw = new StreamWriter($"{Dir}\\Themes\\currentTheme.txt", true))
            {
                sw.Write($"{currentTheme.Name};{currentTheme.SlideBarBackground};{currentTheme.WindowBackgroundColor1};{currentTheme.WindowBackgroundColor2};{currentTheme.NavigationButtonColor};{CurrentTheme.NavigationButtonPressedColor}");
            }
            File.Delete($"{Dir}\\Themes\\themes.txt");
            using (StreamWriter sw = new StreamWriter($"{Dir}\\Themes\\themes.txt", true))
            {
                foreach(Theme t in Themes)
                    sw.WriteLine($"{t.Name};{t.SlideBarBackground};{t.WindowBackgroundColor1};{t.WindowBackgroundColor2};{t.NavigationButtonColor};{t.NavigationButtonPressedColor}");
            }
        }
        public void LoadThemes()
        {
            Dir = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + @"\Icons";

            
            string[] currentThemeLoad = File.ReadAllText($"{Dir}\\Themes\\currentTheme.txt").Split(';');
            CurrentTheme = new Theme(currentThemeLoad[0], currentThemeLoad[1], currentThemeLoad[2], currentThemeLoad[3], currentThemeLoad[4], currentThemeLoad[5], Dir);

            string[] themesLoad = File.ReadAllLines($"{Dir}\\Themes\\themes.txt");
            for (int i = 0; i < themesLoad.Length; i++)
            {
                string[] theme = themesLoad[i].Split(';');
                if (theme.Length == 6)
                {
                    Themes.Add(new Theme(theme[0], theme[1], theme[2], theme[3], theme[4], theme[5], Dir));
                }
            }
        }
    }
}
