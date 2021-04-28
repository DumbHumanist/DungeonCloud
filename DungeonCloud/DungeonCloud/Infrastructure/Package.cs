using DungeonCloud.Models;
using DungeonCloud.Models.Files;
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
        public DungeonInfo FileTransfer { get; set; }
        public string Path { get; set; }
        public Package(int type, UserDirectory userDirectory, User user, DungeonInfo fileInfo)
        {
            Type = type;
            UserDirectory = userDirectory;
            Sub = user.sub;
            FileTransfer = fileInfo;
        }
        public Package(int type, UserDirectory userDirectory, User user, DungeonInfo fileInfo, string path)
        {
            Type = type;
            UserDirectory = userDirectory;
            Sub = user.sub;
            FileTransfer = fileInfo;
            Path = path;
        }
        public Package(int type, User user)
        {
            Type = type;
            Sub = user.sub;
        }

        public Package(int type, UserDirectory userDir, string path)
        {
            Type = type;
            UserDirectory = userDir;
            Path = path;

        }

        public Package() { }
    }
}
