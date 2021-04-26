using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class DungeonDirectoryInfo : DungeonInfo
    {
        public List<DungeonInfo> Children { get; set; }
        public DungeonDirectoryInfo() { }
    }
}
