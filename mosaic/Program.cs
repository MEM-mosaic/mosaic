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
           
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            Repository r = new Repository();

            using (var c = new Context())
            {
                //  foreach (Links l in c.Links) c.Links.Remove(l);
                c.Links.AddRange(r.Links);
                c.SaveChanges();
            }
          //  Console.ReadKey();
        }
    }
}
