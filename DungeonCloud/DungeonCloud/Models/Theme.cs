using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Models
{
    class Theme : PropertyChangedBase
    {

        private string name = "Default";
        public string Name
        {
            set
            {
                name = value;
                NotifyOfPropertyChange();
            }
            get => name;
        }

        private string navigationButtonBackground = "White";
        public string NavigationButtonBackground
        {
            set
            {
                navigationButtonBackground = value;
                NotifyOfPropertyChange();
            }
            get => navigationButtonBackground;
        }

        private string navigationButtonForeground = "Black";
        public string NavigationButtonForeground
        {
            set
            {
                navigationButtonForeground = value;
                NotifyOfPropertyChange();
            }
            get => navigationButtonForeground;
        }
        
        private string navigationButtonBorder = "LightGray";
        public string NavigationButtonBorder
        {
            set
            {
                navigationButtonBorder = value;
                NotifyOfPropertyChange();
            }
            get => navigationButtonBorder;
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

        private string windowBackgroundColor = "";
        public string WindowBackgroundColor
        {
            set
            {
                windowBackgroundColor = value;
                NotifyOfPropertyChange();
            }
            get => windowBackgroundColor;
        }

        public Theme()
        {

        }
        public Theme(string name, string navigationButtonBackground, string navigationButtonForeground, string navigationButtonBorder, string slideBarBackground, string windowBackgroundColor)
        {
            Name = name;
            NavigationButtonBackground = navigationButtonBackground;
            NavigationButtonForeground = navigationButtonForeground;
            NavigationButtonBorder = navigationButtonBorder;
            SlideBarBackground = slideBarBackground;
            WindowBackgroundColor = windowBackgroundColor;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
