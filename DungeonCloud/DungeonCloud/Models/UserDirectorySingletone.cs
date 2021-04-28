using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DungeonCloud.Infrastructure;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;
using DungeonCloud.Models.Files;

namespace DungeonCloud.Models
{
    class UserDirectorySingletone : Caliburn.Micro.PropertyChangedBase
    {
        static UserDirectorySingletone instance;

        private UserDirectory uD;

        public UserDirectory UD 
        {
            get => uD;
            set
            {
                uD = value;
                CurrentDirectory = uD.Dir;
                NotifyOfPropertyChange();
            }
        }

        private DungeonDirectoryInfo currentDirectory;

        public DungeonDirectoryInfo CurrentDirectory
        {
            get => currentDirectory;
            set
            {
                
                currentDirectory = value;
                CollectionRefresh();
                PathString = UserDirectorySingletone.Instance.CurrentDirectory.Path.Replace(SessionSingleton.Instance.NM.Session.sub, SessionSingleton.Instance.NM.Session.name);
                NotifyOfPropertyChange();
            }
        }

        private string pathString;

        public string PathString
        {
            get { return pathString; }
            set
            {
                pathString = value;
                NotifyOfPropertyChange();
            }
        }

        private void CollectionRefresh()
        {
            DirAndFileCollection.Clear();
            DirAndFileCollection = new ObservableCollection<FileSystemInfoExt>();
            string dr =
                Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + @"\Icons";

            foreach (DungeonDirectoryInfo ddi in CurrentDirectory.ChildrenFolders)
            {
                DirAndFileCollection.Add(new FileSystemInfoExt()
                {
                    ImageSource = dr + @"\folder-128.png",
                    FSI = ddi
                });
            }

            foreach (DungeonFileInfo dfi in CurrentDirectory.ChildrenFiles)
            {
                DirAndFileCollection.Add(new FileSystemInfoExt()
                {
                    ImageSource = dr + @"\file-128.png",
                    FSI = dfi
                });
            }
        }

        private UserDirectorySingletone()
        {
        }

        private ObservableCollection<FileSystemInfoExt> dirAndFileCollection = new ObservableCollection<FileSystemInfoExt>();

        public ObservableCollection<FileSystemInfoExt> DirAndFileCollection
        {
            get { return dirAndFileCollection; }
            set
            {
                dirAndFileCollection = value;
                NotifyOfPropertyChange();
            }
        }

        public static UserDirectorySingletone Instance => instance ?? (instance = new UserDirectorySingletone());
    }
}
