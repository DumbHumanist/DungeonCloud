using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Models.Files
{
    class DungeonFileInfo : DungeonInfo
    {
        public long FileSize { get; set; }
        public DungeonFileInfo() { }
    }
}
