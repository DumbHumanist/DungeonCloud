using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
using System;
using System.Collections.Generic;
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

        public void ChangeThemeList()
        {
            ThemeSingleton.Instance.CurrentTheme = ThemeSingleton.Instance.Themes[ThemeIndex];
        }
        public void RemoveTheme()
        {
            
        }
    }
}
