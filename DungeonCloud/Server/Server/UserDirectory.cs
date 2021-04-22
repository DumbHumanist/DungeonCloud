using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Table("UserDirectories")]
    class UserDirectory
    {
        public string DirectoryName { get; set; }

        [Key]
        public string UserSub { get; set; }

        public string Dir { get; set; }

        public UserDirectory() { }
    }
}
