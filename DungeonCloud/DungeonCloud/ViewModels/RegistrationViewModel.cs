using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
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
            UserDirectorySingletone.Instance.CurrentDirectory = new DirectoryInfo(@"C:\Windows");
                //JsonConvert.DeserializeObject<DirectoryInfo>(UD.Dir, settings);

            Environment.CurrentDirectory =
                Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + @"\Icons";

            foreach (FileSystemInfo fsi in UserDirectorySingletone.Instance.CurrentDirectory.GetFileSystemInfos())
            {
                if (Path.GetExtension(fsi.FullName) == "")
                    UserDirectorySingletone.Instance.DirAndFileCollection.Add(new FileSystemInfoExt()
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
    }
}
