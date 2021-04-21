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
        private DirectoryInfo currentDirectory;

        public DirectoryInfo CurrentDirectory
        {
            get { return currentDirectory; }
            set
            {
                currentDirectory = value;
                CollectionRefresh();
                NotifyOfPropertyChange();
            }
        }

        private ObservableCollection<FileSystemInfoExt> dirAndFileCollection;

        public ObservableCollection<FileSystemInfoExt> DirAndFileCollection
        {
            get { return dirAndFileCollection; }
            set 
            {
                dirAndFileCollection = value;
                NotifyOfPropertyChange();
            }
        }

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
            //CurrentDirectory = ud.Dir;
            //>получаем UserDirectory текущего пользователя с сервера
        }

        private void CollectionRefresh()
        {
            DirAndFileCollection.Clear();
            DirAndFileCollection = new ObservableCollection<FileSystemInfoExt>();
            Environment.CurrentDirectory =
                Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + @"\Icons";

            foreach (FileSystemInfo fsi in CurrentDirectory.GetFileSystemInfos())
            {
                if (Path.GetExtension(fsi.FullName) == "")
                    DirAndFileCollection.Add(new FileSystemInfoExt()
                    {
                        ImageSource = Environment.CurrentDirectory + @"\folder-128.png",
                        FSI = fsi
                    });

                else
                    DirAndFileCollection.Add(new FileSystemInfoExt()
                    {
                        ImageSource = Environment.CurrentDirectory + @"\file-128.png",
                        FSI = fsi
                    });
            }
        }

        public void SelectedItemDoubleClick()
        {
            try
            {
                if (Path.GetExtension(SelectedItem.FSI.FullName) == "")
                {
                    DirectoryInfo SubDir = new DirectoryInfo(SelectedItem.FSI.FullName);
                    CurrentDirectory = SubDir;
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
