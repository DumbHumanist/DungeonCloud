using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DungeonCloud.Models
{
    
    [Serializable]
    class User
    {
        public string sub { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }

        public User() { }
    }
}
