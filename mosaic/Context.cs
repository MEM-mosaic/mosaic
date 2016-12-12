using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace mosaic
{
    class Context : DbContext
    {
        public DbSet<Links> Links { get; set; }

        public Context() : base("localsql")
        {

        }
    }
}
