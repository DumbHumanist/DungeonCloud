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

namespace DungeonCloud.Models
{
    class UserDirectorySingletone : Caliburn.Micro.PropertyChangedBase
    {
        static UserDirectorySingletone instance;

        private UserDirectory uD;

        private FileSystemInfoConverter settings;

        public UserDirectory UD
        {
            get => uD;
            set => uD = value;
        }

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
                    UserDirectorySingletone.Instance.DirAndFileCollection.Add(new FileSystemInfoExt()
                    {
                        ImageSource = Environment.CurrentDirectory + @"\file-128.png",
                        FSI = fsi
                    });
            }
        }

        private UserDirectorySingletone()
        {
            settings = new FileSystemInfoConverter();
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

        public static UserDirectorySingletone Instance => instance ?? (instance = new UserDirectorySingletone());
    }
}
