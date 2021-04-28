using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Models.Files
{
    class DungeonInfo
    {
        public string Path { get; set; }
        public string Name { get; set; }


        public DungeonDirectoryInfo GetParent()
        {

            DungeonDirectoryInfo temp = SessionSingleton.Instance.NM.LoadFiles(SessionSingleton.Instance.NM.Session).Dir;
            for (int i = 1; i < Path.Split('\\').Length - 2; i++)
            {
                temp = temp.GetChildByName(Path.Split('\\')[i]);
            }
            return temp;
        }

    }
}
