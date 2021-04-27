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
            UserDirectorySingletone.Instance.CurrentDirectory = JsonConvert.DeserializeObject<DungeonDirectoryInfo>(UserDirectorySingletone.Instance.UD.Dir);

            Environment.CurrentDirectory =
                Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + @"\Icons";

            foreach (DungeonInfo fsi in UserDirectorySingletone.Instance.CurrentDirectory.Children)
            {
                if (Path.GetExtension(fsi.Path) == "")
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
