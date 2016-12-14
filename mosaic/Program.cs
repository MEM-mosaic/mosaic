using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mosaic
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository r = new Repository();
            DataBaseManager d = new DataBaseManager();
            using (var c = new Context())
            {
                c.Links.ToList();
            }
        }
    }
}
