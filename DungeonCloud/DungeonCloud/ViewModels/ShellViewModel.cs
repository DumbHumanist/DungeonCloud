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

        IUserView mainView = new UserSpaceView();
        IUserView settingsView = new SettingsView();
        IUserView registrationView = new RegistrationView();
        IUserView currentView;
        public IUserView CurrentView
        {
            set
            {
                currentView = value;
                NotifyOfPropertyChange();
            }
            get => currentView;
        }
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

        string settingsImage;
        public string SettingsImage
        {
            set
            {
                settingsImage = value;
                NotifyOfPropertyChange();
            }
            get => settingsImage;
        }

        string mainImage;
        public string MainImage
        {
            set
            {
                mainImage = value;
                NotifyOfPropertyChange();
            }
            get => mainImage;
        }
        string regImage;
        public string RegImage
        {
            set
            {
                regImage = value;
                NotifyOfPropertyChange();
            }
            get => regImage;
        }

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
            currentView = registrationView;

            ThemeSingleton.Instance.LoadThemes();

            MainImage = Dir + "\\Processed\\main.png";
            SettingsImage = Dir + "\\Processed\\settings.png";
            RegImage = Dir + "\\Processed\\reg.png";

        }

        //
        


        //navigation buttons

        public void MainButtonPressed()
        {
            CurrentView = mainView;
            MainImage = Dir + "\\Processed\\main-pressed.png";
            SettingsImage = Dir + "\\Processed\\settings.png";
            RegImage = Dir + "\\Processed\\reg.png";
        }
        public void SettingsButtonPressed()
        {
            CurrentView = settingsView;
            MainImage = Dir + "\\Processed\\main.png";
            SettingsImage = Dir + "\\Processed\\settings-pressed.png";
            RegImage = Dir + "\\Processed\\reg.png";
        }
        public void RegButtonPressed()
        {
            CurrentView = registrationView;
            MainImage = Dir + "\\Processed\\main.png";
            SettingsImage = Dir + "\\Processed\\settings.png";
            RegImage = Dir + "\\Processed\\reg-pressed.png";
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