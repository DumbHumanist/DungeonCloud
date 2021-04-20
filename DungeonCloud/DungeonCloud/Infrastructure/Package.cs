using DungeonCloud.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Infrastructure
{
    class Package
    {
        public int Type { get; set; }
        public UserDirectory UserDirectory { get; set; }
        public string Sub { get; set; }
        public FileSystemInfo FileTransfer { get; set; }

        public Package(int type, UserDirectory userDirectory, User user, FileSystemInfo fileInfo)
        {
            Type = type;
            UserDirectory = userDirectory;
            Sub = user.sub;
            FileTransfer = fileInfo;
        }
        public Package(int type, User user)
        {
            Type = type;
            Sub = user.sub;
        }
    }
}
