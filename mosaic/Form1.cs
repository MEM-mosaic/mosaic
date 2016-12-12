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
    public partial class MEM_mosaic : Form
    {
        public MEM_mosaic()
        {
            InitializeComponent();
        }

        PictureBox[] PictureBox = null;
        int Length = 6;
        Bitmap Picture = null;

        private void CreatePictureZone()
        {
            if (PictureBox != null)
            {
                for (int i = 0; i < PictureBox.Length; i++)
                {
                    PictureBox[i].Dispose();
                }
                PictureBox = null;
            }

            int num = Length;
            PictureBox = new PictureBox[num * num];
            int width = zone.Width / num;
            int height = zone.Height / num;
            int x = 0;
            int y = 0;

            for (int i = 0; i < PictureBox.Length; i++)
            {
                PictureBox[i] = new PictureBox();
                PictureBox[i].Width = width;
                PictureBox[i].Height = height;
                PictureBox[i].Left = 0 + x * PictureBox[i].Width;
                PictureBox[i].Top = 0 + y * PictureBox[i].Height;

                Point point = new Point();
                point.X = PictureBox[i].Left;
                point.Y = PictureBox[i].Top;
                PictureBox[i].Tag = point;
                x++;

                if (x == num)
                {
                    x = 0;
                    y++;
                }

                PictureBox[i].Parent = zone;
                PictureBox[i].BorderStyle = BorderStyle.Fixed3D;
                PictureBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                PictureBox[i].Show();
                PictureBox[i].Click += new EventHandler(pictureBox1_Click);
            }

            DrawPicture();
        }

        private void DrawPicture()
        {
            if (Picture == null) return;
            int x = 0;
            int y = 0;
            int num = Length;
            for (int i = 0; i < PictureBox.Length; i++)
            {
                int width = Picture.Width / num;
                int height = Picture.Height / num;
                PictureBox[i].Image = Picture.Clone(new RectangleF(x * width, y * height, width, height), Picture.PixelFormat);
                x++;
                if (x == Length)
                {
                    x = 0;
                    y++;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            for (int i = 0; i < PictureBox.Length; i++)
            {
                if (PictureBox[i].Visible == false)
                {
                    if (
                        (pb.Location.X == PictureBox[i].Location.X &&
                         Math.Abs(pb.Location.Y - PictureBox[i].Location.Y) == PictureBox[i].Height)
                        ||
                        (pb.Location.Y == PictureBox[i].Location.Y && Math.Abs(pb.Location.X - PictureBox[i].Location.X) == PictureBox[i].Width))
                    {

                        Point pt = PictureBox[i].Location;
                        PictureBox[i].Location = pb.Location;
                        pb.Location = pt;

                        for (int j = 0; j < PictureBox.Length; j++)
                        {
                            Point point = (Point)PictureBox[j].Tag;
                            if (PictureBox[j].Location != point)
                            {
                                return;
                            }
                        }

                        for (int m = 0; m < PictureBox.Length; m++)
                        {
                            PictureBox[m].Visible = true;
                            PictureBox[m].BorderStyle = BorderStyle.None;
                        }
                    }

                    break;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.bmp;*.jpg;*.jpeg;)|*.bmp;*.jpg;.jpeg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Picture = new Bitmap(openFileDialog.FileName);
                CreatePictureZone();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Picture == null) return;
            Random random = new Random(Environment.TickCount);
            int a = 0;
            for (int i = 0; i < PictureBox.Length; i++)
            {
                PictureBox[i].Visible = true;
                a = random.Next(0, PictureBox.Length);
                Point pointR = PictureBox[a].Location;
                Point pointI = PictureBox[i].Location;
                PictureBox[i].Location = pointR;
                PictureBox[a].Location = pointI;
                PictureBox[i].BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PictureBox.Length; i++)
            {
                Point point = (Point)PictureBox[i].Tag;
                PictureBox[i].Location = point;
                PictureBox[i].Visible = true;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Length = Length;
            if (settings.ShowDialog() == DialogResult.OK)
            {
                Length = settings.Length;
                CreatePictureZone();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Help helpDlg = new Help();
            helpDlg.ImageDuplicate = Picture;
            helpDlg.ShowDialog();
        }
    }
}
