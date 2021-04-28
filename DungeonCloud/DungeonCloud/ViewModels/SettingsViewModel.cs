using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DungeonCloud.ViewModels
{
    class SettingsViewModel : Caliburn.Micro.PropertyChangedBase
    {
        private int themeIndex;

        public int ThemeIndex
        {
            set
            {
                themeIndex = value;
                NotifyOfPropertyChange();
            }
            get => themeIndex;
        }

        private string name;
        public string Name
        {
            set
            {
                name = value;
                NotifyOfPropertyChange();
            }
            get => name;
        }

        private string navigationButtonColor = "";
        public string NavigationButtonColor
        {
            set
            {
                navigationButtonColor = value;
                NotifyOfPropertyChange();
            }
            get => navigationButtonColor;
        }

        private string navigationButtonPressedColor = "";
        public string NavigationButtonPressedColor
        {
            set
            {
                navigationButtonPressedColor = value;
                NotifyOfPropertyChange();
            }
            get => navigationButtonPressedColor;
        }

        private string slideBarBackground = "";
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



        public void ChangeThemeList()
        {
            if (themeIndex != -1)
                ThemeSingleton.Instance.CurrentTheme = ThemeSingleton.Instance.Themes[ThemeIndex];
        }
        public void RemoveTheme()
        {
            if (themeIndex != -1)
            if (ThemeSingleton.Instance.Themes.Count > 1)
            {
                 ThemeSingleton.Instance.Themes.Remove(ThemeSingleton.Instance.Themes[ThemeIndex]);
                ThemeSingleton.Instance.CurrentTheme = ThemeSingleton.Instance.Themes[0];
            }
        }
        public void AddTheme()
        {
            string Dir = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + @"\Icons";
            Theme newTheme = new Theme(Name, SlideBarBackground, WindowBackgroundColor1, WindowBackgroundColor2, NavigationButtonColor, NavigationButtonPressedColor, Dir);
            ThemeSingleton.Instance.Themes.Add(newTheme);
        }
    }
}
