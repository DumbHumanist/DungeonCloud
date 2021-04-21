using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DungeonCloud.Models
{
    class UserDirectory
    {
        public string DirectoryName { get; set; }

        public string UserSub { get; set; }

        public DirectoryInfo Dir { get; set; }
    }
}
