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
    public partial class ImageFromDB : Form
    {

        public Bitmap Result = null;
        int imageIndex = 0;
        List<Bitmap> easyList = new List<Bitmap>();
        List<Bitmap> mediumList = new List<Bitmap>();
        List<Bitmap> hardList = new List<Bitmap>();

        public ImageFromDB(DataBaseManager dbm)
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;

            easyList = dbm.GetBitmapListEasy();
            mediumList = dbm.GetBitmapListMedium();
            hardList = dbm.GetBitmapListHard();

            pictureBox1.Image = easyList[0];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageIndex = 0;
            button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imageIndex--;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    if (imageIndex < 0)
                        imageIndex = 0;
                    pictureBox1.Image = easyList[imageIndex];
                    break;
                case 1:
                    if (imageIndex < 0)
                        imageIndex = 0;
                    pictureBox1.Image = mediumList[imageIndex];
                    break;
                case 2:
                    if (imageIndex < 0)
                        imageIndex = 0;
                    pictureBox1.Image = hardList[imageIndex];
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = new Bitmap(pictureBox1.Image);
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageIndex++;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    if (easyList.Count > 0)
                        imageIndex %= easyList.Count();
                    pictureBox1.Image = easyList[imageIndex];
                    break;
                case 1:
                    if (mediumList.Count > 0)
                        imageIndex %= mediumList.Count();
                    pictureBox1.Image = mediumList[imageIndex];
                    break;
                case 2:
                    if (hardList.Count > 0)
                        imageIndex %= hardList.Count();
                    pictureBox1.Image = hardList[imageIndex];
                    break;
            }
        }
    }
}
