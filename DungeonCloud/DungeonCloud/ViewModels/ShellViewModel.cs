using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
using DungeonCloud.Views;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DungeonCloud.ViewModels {
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell 
    {

       
        private int navigationBarWidth = 40;
        public int NavigationBarWidth
        {
            set
            {
                navigationBarWidth = value;
                try
                {
                    NotifyOfPropertyChange();
                }
                catch (Exception) { }
            }
            get => navigationBarWidth;
        }
        private bool navigationBarEntered = false;


        string Dir;

        

        bool canNavigation = true;
        public bool CanNavigation
        {
            set
            {
                canNavigation = value;
                NotifyOfPropertyChange();
            }
            get => canNavigation;
        }

        string color1;
        public string Color1
        {
            set
            {
                color1 = value;
                NotifyOfPropertyChange();
            }
            get => color1;
        }

        string color2;
        public string Color2
        {
            set
            {
                color2 = value;
                NotifyOfPropertyChange();
            }
            get => color2;
        }
        string color3;
        public string Color3
        {
            set
            {
                color3 = value;
                NotifyOfPropertyChange();
            }
            get => color3;
        }

        string buttonColor;
        public string ButtonColor
        {
            set
            {
                buttonColor = value;
                NotifyOfPropertyChange();
            }
            get => buttonColor;
        }
        string buttonColorPressed;
        public string ButtonColorPressed
        {
            set
            {
                buttonColorPressed = value;
                NotifyOfPropertyChange();
            }
            get => buttonColorPressed;
        }

        public ShellViewModel()
        {
            Dir = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + @"\Icons";
            ViewSingleton.Instance.currentView = ViewSingleton.Instance.registrationView;

            ThemeSingleton.Instance.LoadThemes();

            
            ThemeSingleton.Instance.MainImage = ThemeSingleton.Instance.MainImagePressed;
            ThemeSingleton.Instance.SettingsImage = ThemeSingleton.Instance.SettingsImageDefault;
            ThemeSingleton.Instance.RegImage = ThemeSingleton.Instance.RegImageDefault;

        }

        //
        


        //navigation buttons

        public void MainButtonPressed()
        {
            ViewSingleton.Instance.CurrentView = ViewSingleton.Instance.mainView;
            ThemeSingleton.Instance.MainImage = ThemeSingleton.Instance.MainImagePressed;
            ThemeSingleton.Instance.SettingsImage = ThemeSingleton.Instance.SettingsImageDefault;
            ThemeSingleton.Instance.RegImage = ThemeSingleton.Instance.RegImageDefault;
        }
        public void SettingsButtonPressed()
        {
            ViewSingleton.Instance.CurrentView = ViewSingleton.Instance.settingsView;
            ThemeSingleton.Instance.MainImage = ThemeSingleton.Instance.MainImageDefault;
            ThemeSingleton.Instance.SettingsImage = ThemeSingleton.Instance.SettingsImagePressed;
            ThemeSingleton.Instance.RegImage = ThemeSingleton.Instance.RegImageDefault;
        }
        public void RegButtonPressed()
        {
            ViewSingleton.Instance.CurrentView = ViewSingleton.Instance.registrationView;
            ThemeSingleton.Instance.MainImage = ThemeSingleton.Instance.MainImageDefault;
            ThemeSingleton.Instance.SettingsImage = ThemeSingleton.Instance.SettingsImageDefault;
            ThemeSingleton.Instance.RegImage = ThemeSingleton.Instance.RegImagePressed;
        }

        //

        public void MouseEnterNavigationBar()
        {
            if (!navigationBarEntered)
            {
                navigationBarEntered = true;
                BarSlide();
            }
        }
        public void MouseLeaveNavigationBar()
        {
            if (navigationBarEntered)
            {
                navigationBarEntered = false;
                BarSlide();
            }
        }

        void BarSlideOut()
        {
            while (navigationBarWidth < 100)
            {
                if (navigationBarEntered)
                {
                    NavigationBarWidth += 3;
                    Thread.Sleep(10);
                }
                else
                    break;
            }
        }
        void BarSlideIn()
        {
            while (navigationBarWidth > 40)
            {
                if (!navigationBarEntered)
                {
                    NavigationBarWidth -= 3;
                    Thread.Sleep(10);
                }
                else
                    break;
            }
        }
        async void BarSlide()
        {
            if (navigationBarEntered)
                await Task.Run(() => BarSlideOut());
            else
                await Task.Run(() => BarSlideIn());
        }
    }
}