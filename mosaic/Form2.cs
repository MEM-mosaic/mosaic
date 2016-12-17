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
    /// <summary>
    /// Класс диалогового окна настроек программы
    /// </summary>
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        public int Length = 3;       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            { Length = 3; }
            if (comboBox1.SelectedIndex == 1)
            { Length = 5; }
            if (comboBox1.SelectedIndex == 2)
            { Length = 7; }
            if (comboBox1.SelectedIndex == 3)
            { Length = 9; }
        }
    }
}
