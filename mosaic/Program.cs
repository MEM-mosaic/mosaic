using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mosaic
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Repository r = new Repository();
            DataBaseManager d = new DataBaseManager();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MEM_mosaic());
        }
    }
}