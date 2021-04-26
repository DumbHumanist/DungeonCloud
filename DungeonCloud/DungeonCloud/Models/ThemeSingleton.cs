using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Models
{
    class ThemeSingleton : PropertyChangedBase
    {
        static ThemeSingleton instance;
        Theme currentTheme = new Theme();

        public Theme CurrentTheme
        {
            set
            {
                currentTheme = value;
                currentTheme.SetColors();
                NotifyOfPropertyChange();
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

        string Dir;
        public static ThemeSingleton Instance => instance ?? (instance = new ThemeSingleton());

        private ThemeSingleton()
        {
            LoadThemes();
        }


        public void SaveThemes()
        {
            File.Delete($"{Dir}\\Themes\\currentTheme.txt");
            using (StreamWriter sw = new StreamWriter($"{Dir}\\Themes\\currentTheme.txt", true))
            {
                sw.Write($"{currentTheme.Name};{currentTheme.NavigationButtonColor};{currentTheme.NavigationButtonPressedColor};{currentTheme.SlideBarBackground};{currentTheme.WindowBackgroundColor1};{CurrentTheme.WindowBackgroundColor2}");
            }
            File.Delete($"{Dir}\\Themes\\themes.txt");
            using (StreamWriter sw = new StreamWriter($"{Dir}\\Themes\\themes.txt", true))
            {
                foreach(Theme t in Themes)
                    sw.Write($"{t.Name};{t.NavigationButtonColor};{t.NavigationButtonPressedColor};{t.SlideBarBackground};{t.WindowBackgroundColor1};{t.WindowBackgroundColor2};");
            }
        }
        public void LoadThemes()
        {
            Dir = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + @"\Icons";
            string[] currentThemeLoad = File.ReadAllText($"{Dir}\\Themes\\currentTheme.txt").Split(';');
            currentTheme = new Theme(currentThemeLoad[0], currentThemeLoad[1], currentThemeLoad[2], currentThemeLoad[3], currentThemeLoad[4], currentThemeLoad[5], Dir);

            string[] themesLoad = File.ReadAllText($"{Dir}\\Themes\\themes.txt").Split(';');
            for (int i = 0; i < themesLoad.Length - 1;)
            {
                Themes.Add(new Theme(themesLoad[i++], themesLoad[i++], themesLoad[i++], themesLoad[i++], themesLoad[i++], themesLoad[i++], Dir));
            }
        }
    }
}
