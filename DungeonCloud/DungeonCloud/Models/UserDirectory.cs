using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DungeonCloud.Models.Files;

namespace DungeonCloud.Models
{
    class UserDirectory
    {
        public string DirectoryName { get; set; }

        public string UserSub { get; set; }

        public DungeonDirectoryInfo Dir { get; set; }

        public UserDirectory() { }
      
    }
}
