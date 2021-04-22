using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DungeonCloud.Infrastructure;
using System.IO;

namespace DungeonCloud.Models
{
    class UserDirectorySingletone
    {
        static UserDirectorySingletone instance;

        private UserDirectory uD;

        private FileSystemInfoConverter settings;

        public UserDirectory UD
        {
            get => uD;
        }

        private DirectoryInfo dir;

        public DirectoryInfo Dir
        {
            get => JsonConvert.DeserializeObject<DirectoryInfo>(UD.Dir, settings);
        }

        private UserDirectorySingletone()
        {
                uD = SessionSingleton.Instance.NM.LoadFiles(SessionSingleton.Instance.NM.Session);
        }

        public static UserDirectorySingletone Instance => instance ?? (instance = new UserDirectorySingletone());
    }
}
