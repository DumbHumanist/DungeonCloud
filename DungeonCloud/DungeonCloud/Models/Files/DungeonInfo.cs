using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DungeonCloud.Models.Files
{
    class DungeonInfo
    {
        public string Path { get; set; }
        public string Name { get; set; }


        public DungeonDirectoryInfo GetParent()
        {

            DungeonDirectoryInfo temp = SessionSingleton.Instance.NM.LoadFiles(SessionSingleton.Instance.NM.Session).Dir;
            
            for (int i = 1; i < Path.Split('\\').Length - 1; i++)
            {
                temp = temp.GetChildByName(Path.Split('\\')[i]);
            }

            return temp;
        }

    }
}
