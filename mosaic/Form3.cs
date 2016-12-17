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
    /// Класс диалогового окна помощи пользователю
    /// </summary>
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        public Image ImageDuplicate = null;

        /// <summary>
        /// Метод для загрузки картинки на форму посредством PictureBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageDuplicate;
        }
    }
}
