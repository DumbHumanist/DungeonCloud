using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class DungeonDirectoryInfo : DungeonInfo
    {
        public List<DungeonDirectoryInfo> ChildrenFolders { get; set; } = new List<DungeonDirectoryInfo>();
        public List<DungeonFileInfo> ChildrenFiles { get; set; } = new List<DungeonFileInfo>();
        public DungeonDirectoryInfo() { }
    }
}
