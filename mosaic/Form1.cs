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
    /// Класс главной формы, размещает на себе элементы управления - панель, прямоугольники PictureBox.     
    /// </summary>
    public partial class MEM_mosaic : Form
    {
        public MEM_mosaic()
        {
            InitializeComponent();
            DataBaseManager manager = new DataBaseManager();
        }
    
        PictureBox[] PictureBox = null;// Массив объектов прямоугольников для хранения сегментов картинки.
        int Length = 3;// Длина стороны в прямоугольниках.
        Bitmap Picture = null;// Объект хранения картинки.

        /// <summary>
        /// Метод для вывода инфомации об игре.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MEM_mosaic_Load(object sender, EventArgs e)
        {
            MessageBox.Show("MEM-mosaic\nДобро пожаловать в самую мемную игру на планете! Вы готовы погрузиться в мир смешных картинок и эмоций и собрать их по кусочкам?\nСкорее нажимайте ОК и мы начинаем!!!");
        }

        /// <summary>
        /// Метод для создания области рисования картинки
        /// </summary>
        private void CreatePictureZone()
        {
            // Удаляется предыдущий массив, чтобы создать новый.
            if (PictureBox != null)
            {
                for (int i = 0; i < PictureBox.Length; i++)
                {
                    PictureBox[i].Dispose();
                }
                PictureBox = null;
            }

            int num = Length;
            PictureBox = new PictureBox[num * num];// Создается массив прямоугольников размером Length Х Length.
            int width = zone.Width / num;// Вычисляется ширина прямоугольников.
            int height = zone.Height / num;// Вычисляется высота прямоугольников.
            int x = 0;// счетчик прямоугольников по координате Х в одном ряду
            int y = 0;// счетчик прямоугольников по координате Y в одном столбце

            for (int i = 0; i < PictureBox.Length; i++)
            {
                PictureBox[i] = new PictureBox();// Создание прямоугольника, элемента массива
                // Размеры и координаты размещения созданного прямоугольника.
                PictureBox[i].Width = width;
                PictureBox[i].Height = height;
                PictureBox[i].Left = 0 + x * PictureBox[i].Width;
                PictureBox[i].Top = 0 + y * PictureBox[i].Height;

                // Начальные координаты прямоугольника для восстановления перемешанной картинки, определения полной сборки картинки.
                Point point = new Point();
                point.X = PictureBox[i].Left;
                point.Y = PictureBox[i].Top;
                PictureBox[i].Tag = point;// сохраним координаты в свойстве Tag каждого прямоугольника
                x++;

                // Считаем прямоугольники по рядам и столбцам.
                if (x == num)
                {
                    x = 0;
                    y++;
                }

                PictureBox[i].Parent = zone;// разместим прямоугольники на панели
                PictureBox[i].BorderStyle = BorderStyle.Fixed3D;
                PictureBox[i].SizeMode = PictureBoxSizeMode.StretchImage;// размеры картинки будут подгоняться под размеры прямоугольника
                PictureBox[i].Show();

                // Для всех прямоугольников массива событие клика мыши будет обрабатываться в одной и той же функции, для удобства вычисления координат прямоугольников в одном месте.
                PictureBox[i].Click += new EventHandler(pictureBox1_Click);
            }

            DrawPicture();// Раскидываем картинку на сегменты и рисуем каждый сегмент на своем прямоугольнике.
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
                // Сначала определим пустое место на области рисования картинки.
                if (PictureBox[i].Visible == false)
                {
                    // Затем проверим кликнутый прямоугольник и если у него совпадают координаты по X или Y, (откидываем из вычисления прямоугольники расположеннные по диагонали)
                    // но при этом он на ближайшем расстоянии от пустого прямоугольника (откидываем прямоугольники расположенные через прямоугольник от пустого)
                    if (
                        (pb.Location.X == PictureBox[i].Location.X &&
                         Math.Abs(pb.Location.Y - PictureBox[i].Location.Y) == PictureBox[i].Height)
                        ||
                        (pb.Location.Y == PictureBox[i].Location.Y && Math.Abs(pb.Location.X - PictureBox[i].Location.X) == PictureBox[i].Width))
                    {
                        // После успешной проверки меняем местами пустой и кликнутый прямоугольники.
                        Point pt = PictureBox[i].Location;
                        PictureBox[i].Location = pb.Location;
                        pb.Location = pt;

                        // Если хоть у одного прямоугольника не совпадают реальные координаты и первичные выходим из функции.
                        for (int j = 0; j < PictureBox.Length; j++)
                        {
                            Point point = (Point)PictureBox[j].Tag;
                            if (PictureBox[j].Location != point)
                            {
                                return;
                            }
                        }

                        // Если у всех прямоугольников совпали реальные и первичные координаты - картинка собрана!
                        for (int m = 0; m < PictureBox.Length; m++)
                        {
                            PictureBox[m].Visible = true;
                            PictureBox[m].BorderStyle = BorderStyle.None;// убираем обрамление прямоугольников
                        }
                    }
                    Exit a = new Exit();

                    if (a.ShowDialog() == DialogResult.OK)
                    {
                        Application.Restart();
                    }
                    else
                    {
                        Application.Exit();
                    }

                    //break;          
                }
            }
        }

        /// <summary>
        /// Открытие диалогового окна выбора файла и создание новой области прорисовки картинки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Фильтр показа только файлов с определенным расширением.
            openFileDialog.Filter = "(*.bmp;*.jpg;*.jpeg;)|*.bmp;*.jpg;.jpeg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Picture = new Bitmap(openFileDialog.FileName);// Загружаем выбранную картинку
                CreatePictureZone();// Создаем новую область прорисовки.
            }
        }

        /// <summary>
        /// Открытие диалогового окна выбора файла из базы данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ImageFromDB imageChoose = new ImageFromDB(new DataBaseManager());
            imageChoose.ShowDialog();
            if (imageChoose.Result != null)
            {
                Picture = imageChoose.Result;
                CreatePictureZone();
            }
        }

        /// <summary>
        /// Перемешивание прямоугольников, хаотично меняем их координаты.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (Picture == null) return;

            // Создаем объект генерирования псевослучайных чисел, для различного набора случайных чисел инициализацию
            // объекта Random производим от счетчика количества миллисекунд прошедших со времени запуска операционной системы. 
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

            // Случайным образом выбираем пустой прямоугольник, делаем его невидимым.
            a = random.Next(0, PictureBox.Length);
            PictureBox[a].Visible = false;
        }

        /// <summary>
        /// Восстанавливаем картинку соответсвенно первичным координатам.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PictureBox.Length; i++)
            {
                Point point = (Point)PictureBox[i].Tag;
                PictureBox[i].Location = point;
                PictureBox[i].Visible = true;
            }
        }

        /// <summary>
        /// Открываем диалоговое окно настроек приложения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Length = Length;
            if (settings.ShowDialog() == DialogResult.OK)
            {
                Length = settings.Length;
                CreatePictureZone();
            }
        }

        /// <summary>
        /// Открываем диалоговое окно с нормальным видом картинки, для освежения памяти пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            Help helpDlg = new Help();
            helpDlg.ImageDuplicate = Picture;
            helpDlg.ShowDialog();
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
