using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class DungeonInfo
    {

        public string Path { get; set; }

        [Key]
        public string Name { get; set; }
    }
}
