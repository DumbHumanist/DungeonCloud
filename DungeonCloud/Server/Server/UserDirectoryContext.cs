using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class UserDirectoryContext : DbContext
    {
        public UserDirectoryContext()
            : base("DbConnection")
        { }

        public DbSet<UserDirectory> UserDirectories { get; set; }
    }
}
