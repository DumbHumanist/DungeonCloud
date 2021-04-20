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
        public static ThemeSingleton Instance => instance ?? (instance = new ThemeSingleton());

        private ThemeSingleton()
        {
            LoadThemes();
        }


        public void SaveThemes()
        {
            File.Delete("currentTheme.txt");
            using (StreamWriter sw = new StreamWriter("currentTheme.txt", true))
            {
                sw.Write($"{currentTheme.Name};{currentTheme.NavigationButtonBackground};{currentTheme.NavigationButtonForeground};{currentTheme.NavigationButtonBorder};{currentTheme.SlideBarBackground};{CurrentTheme.WindowBackgroundColor}");
            }
            File.Delete("themes.txt");
            using (StreamWriter sw = new StreamWriter("themes.txt", true))
            {
                foreach(Theme t in Themes)
                    sw.Write($"{t.Name};{t.NavigationButtonBackground};{t.NavigationButtonForeground};{t.NavigationButtonBorder};{t.SlideBarBackground};{t.WindowBackgroundColor};");
            }
        }
        public void LoadThemes()
        {
            string[] currentThemeLoad = File.ReadAllText("currentTheme.txt").Split(';');
            currentTheme = new Theme(currentThemeLoad[0], currentThemeLoad[1], currentThemeLoad[2], currentThemeLoad[3], currentThemeLoad[4], currentThemeLoad[5]);

            string[] themesLoad = File.ReadAllText("themes.txt").Split(';');
            for (int i = 0; i < themesLoad.Length - 1;)
            {
                Themes.Add(new Theme(themesLoad[i++], themesLoad[i++], themesLoad[i++], themesLoad[i++], themesLoad[i++], themesLoad[i++]));
            }
        }
    }
}
