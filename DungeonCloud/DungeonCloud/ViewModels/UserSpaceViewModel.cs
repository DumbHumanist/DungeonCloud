using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.ViewModels
{
    class UserSpaceViewModel : Caliburn.Micro.PropertyChangedBase
    {

        private FileSystemInfoExt selectedItem;

        public FileSystemInfoExt SelectedItem 
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange();
            }
        }


        public UserSpaceViewModel()
        {

        }

        public void BackButtonClick()
        {
            try
            {
                UserDirectorySingletone.Instance.CurrentDirectory = UserDirectorySingletone.Instance.CurrentDirectory.Parent;
            }
            catch
            {
                return;
            }
        }

        public void SelectedItemDoubleClick()
        {
            try
            {
                if (Path.GetExtension(SelectedItem.FSI.FullName) == "")
                {
                    DirectoryInfo SubDir = new DirectoryInfo(SelectedItem.FSI.FullName);
                    UserDirectorySingletone.Instance.CurrentDirectory = SubDir;
                }
                else //if(Path.GetExtension(SelectedItem.FSI.FullName) == ".txt")
                {
                    //ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
                    //startInfo.WindowStyle = ProcessWindowStyle.Minimized;

                    Process.Start(SelectedItem.FSI.FullName);
                }

            }
            catch
            {
                return;
            }
        }
    }
}
