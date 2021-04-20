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
        string DirectoryName { get; set; }

        string UserSub { get; set; }

        DirectoryInfo Dir { get; set; }
    }
}
