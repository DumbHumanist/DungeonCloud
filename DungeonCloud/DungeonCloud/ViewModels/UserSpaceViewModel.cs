using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
using DungeonCloud.Models.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syroot.Windows.IO;
using System.Windows;

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
                UserDirectorySingletone.Instance.CurrentDirectory = UserDirectorySingletone.Instance.CurrentDirectory.GetParent();
                
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
                if (Path.GetExtension(SelectedItem.FSI.Path) == "")
                {
                    DungeonDirectoryInfo SubDir = UserDirectorySingletone.Instance.CurrentDirectory.GetChildByName(SelectedItem.FSI.Name);
                    UserDirectorySingletone.Instance.CurrentDirectory = SubDir;
                   
                }
                else //if(Path.GetExtension(SelectedItem.FSI.FullName) == ".txt")
                {
                    //ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
                    //startInfo.WindowStyle = ProcessWindowStyle.Minimized;

                    Process.Start(SelectedItem.FSI.Path);
                }

            }
            catch
            {
                return;
            }
        }

        public async void DownloadButtonClick()
        {
            if (SelectedItem != null)            
            {
                string downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;
                byte[] fileBytes = await Task<byte[]>.Run(() =>
                SessionSingleton.Instance.NM.DownloadFile(UserDirectorySingletone.Instance.UD,
                SelectedItem.FSI.Path.Substring(SelectedItem.FSI.Path.IndexOf('\\')),
                Convert.ToInt32(SelectedItem.FSI.GetParent().ChildrenFiles.Where(a => a.Path == SelectedItem.FSI.Path).FirstOrDefault().FileSize)));

                await Task.Factory.StartNew(() => File.WriteAllBytes(downloadsPath + '\\' + SelectedItem.FSI.Name, fileBytes));
            }
        }

        public void UploadButtonClick()
        {

        }
    }
}
