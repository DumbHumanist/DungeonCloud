using DungeonCloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.ViewModels
{
    class TextViewModel
    {
        public void BackButtonClick()
        {
            ViewSingleton.Instance.CurrentView = ViewSingleton.Instance.mainView;
        }
    }
}
