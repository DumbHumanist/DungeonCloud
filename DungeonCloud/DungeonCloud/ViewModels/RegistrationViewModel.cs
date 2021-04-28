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
            try
            {
                await SessionSingleton.Instance.NM.StartAuthViaGoogle();

                UserDirectorySingletone.Instance.UD = SessionSingleton.Instance.NM.LoadFiles(SessionSingleton.Instance.NM.Session);
                UserDirectorySingletone.Instance.CurrentDirectory = UserDirectorySingletone.Instance.UD.Dir;
            }
            catch(Exception a) 
            {
                MessageBox.Show("Unable to reach the server", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
