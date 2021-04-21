using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Models
{
    public class FileSystemInfoExt
    {
        public string ImageSource { get; set; }

        public FileSystemInfo FSI { get; set; }

        public override string ToString() => FSI.Name;
    }
}
