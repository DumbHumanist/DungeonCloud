using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
using DungeonCloud.Models.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                await SessionSingleton.Instance.NM.StartAuthViaGoogle();

                UserDirectorySingletone.Instance.UD = SessionSingleton.Instance.NM.LoadFiles(SessionSingleton.Instance.NM.Session);
                UserDirectorySingletone.Instance.CurrentDirectory = UserDirectorySingletone.Instance.UD.Dir;
            }
            catch(Exception a) 
            {
                var result = MessageBox.Show("Sorry, but we can't offer you server services, But we can offer you cool snake", "Oh shit, I'm sorry", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                {
                    string dir = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + "\\Snake.exe";
                    if (File.Exists(dir))
                    {
                        ProcessStartInfo processInfo = new ProcessStartInfo();
                        processInfo.FileName = dir;
                        processInfo.WorkingDirectory = Path.GetDirectoryName(dir);
                        processInfo.ErrorDialog = true;
                        processInfo.UseShellExecute = false;
                        processInfo.RedirectStandardOutput = true;
                        processInfo.RedirectStandardError = true;
                        Process proc = Process.Start(processInfo);
                    }
                }
            }
        }
    }
}
