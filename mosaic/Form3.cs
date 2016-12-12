using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mosaic
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        public Image ImageDuplicate = null;

        private void Help_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageDuplicate;
        }
    }
}
