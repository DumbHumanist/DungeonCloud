using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Models.Files
{
    class DungeonDirectoryInfo : DungeonInfo
    {
     public List<DungeonInfo> Children { get; set; }
     public DungeonDirectoryInfo() { }
     public DungeonInfo GetChildByName(string childName)
     {
         foreach (var i in this.Children)
                if (i.Name == childName)
                    return i;
          return null;
     }
     public DungeonInfo GetParent()
     {
            DungeonInfo temp = UserDirectorySingletone.Instance.UD.Dir;
             for(int i = 1; i<Path.Split('/').Length-2;i++)
              {
                temp = (temp as DungeonDirectoryInfo).GetChildByName(Path.Split('/')[i]);
              }
            return temp;
     }

    }
}
