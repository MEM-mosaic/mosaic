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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        public int Length = 6;

        private void Settings_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Length;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Length = (int)numericUpDown1.Value;
        }
    }
}
