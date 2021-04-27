﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Models.Files
{
    class DungeonDirectoryInfo : DungeonInfo
    {
     public List<DungeonDirectoryInfo> ChildrenFolders { get; set; }
     public List<DungeonFileInfo> ChildrenFiles { get; set; }
     public DungeonDirectoryInfo() { }
     public DungeonDirectoryInfo GetChildByName(string childName)
     {          
         foreach (var i in this.ChildrenFolders)
                if (i.Name == childName)
                    return i;
          return null;
     }
     public DungeonDirectoryInfo GetParent()
     {

            DungeonDirectoryInfo temp = UserDirectorySingletone.Instance.UD.Dir;
             for(int i = 1; i<Path.Split('/').Length-2;i++)
              {
                temp = temp.GetChildByName(Path.Split('/')[i]);
              }
            return temp;
     }

    }
}
