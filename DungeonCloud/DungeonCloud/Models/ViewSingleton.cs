using Caliburn.Micro;
using DungeonCloud.Infrastructure;
using DungeonCloud.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DungeonCloud.Models
{
    class ViewSingleton : PropertyChangedBase
    {
        private static ViewSingleton instance;

        public IUserView mainView = new UserSpaceView();
        public IUserView settingsView = new SettingsView();
        public IUserView registrationView = new RegistrationView();



        public IUserView currentView;
        public IUserView CurrentView
        {
            set
            {
                currentView = value;
                NotifyOfPropertyChange();
            }
            get => currentView;
        }

        private ImageSource image;

        public ImageSource Image
        {
            set
            {
                image = value;
                NotifyOfPropertyChange();
            }
            get => image;
        }


        private ViewSingleton() { }

        public static ViewSingleton Instance => instance ?? (instance = new ViewSingleton()); 
    }
}
