using DungeonCloud.Infrastructure;
using DungeonCloud.Views;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        public ShellViewModel()
        {
            currentView = registrationView;
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

        private double mouseX;



       

        //navigation buttons

        public void MainButtonPressed()
        {
            CurrentView = mainView;
        }
        public void SettingsButtonPressed()
        {
            CurrentView = settingsView;
        }
        public void RegButtonPressed()
        {
            CurrentView = registrationView;
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