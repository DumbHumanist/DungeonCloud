using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
using DungeonCloud.Models.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DungeonCloud.ViewModels
{
    class RegistrationViewModel : Caliburn.Micro.PropertyChangedBase
    {
        public async void AuthorizeWithChrome()
        {
            await SessionSingleton.Instance.NM.StartAuthViaGoogle();

            UserDirectorySingletone.Instance.UD = SessionSingleton.Instance.NM.LoadFiles(SessionSingleton.Instance.NM.Session);
            UserDirectorySingletone.Instance.CurrentDirectory = UserDirectorySingletone.Instance.UD.Dir;

            string dir =
                Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + @"\Icons";

            foreach (DungeonDirectoryInfo ddi in UserDirectorySingletone.Instance.CurrentDirectory.ChildrenFolders)
            {
                UserDirectorySingletone.Instance.DirAndFileCollection.Add(new FileSystemInfoExt()
                {
                    ImageSource = dir + @"\folder-128.png",
                    FSI = ddi
                });
            }

            foreach (DungeonFileInfo dfi in UserDirectorySingletone.Instance.CurrentDirectory.ChildrenFiles)
            {
                UserDirectorySingletone.Instance.DirAndFileCollection.Add(new FileSystemInfoExt()
                {
                    ImageSource = dir + @"\file-128.png",
                    FSI = dfi
                });
            }
        }
    }
}
