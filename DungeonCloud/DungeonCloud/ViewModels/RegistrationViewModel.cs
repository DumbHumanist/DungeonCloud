using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
using System;
using System.Collections.Generic;
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
        }
    }
}
