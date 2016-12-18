using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace mosaic
{
    public class DataBaseManager
    {
        Repository repo = new Repository();

        //Получить массив Bitmap картинок с первого уровня сложности
        public List<Bitmap> GetBitmapListEasy()
        {
            List<Bitmap> result = new List<Bitmap>();
            List<string> fileNames = new List<string>();
            fileNames = repo.GetNamesByLevel(1);
            foreach (string s in fileNames)
            {
                result.Add(new Bitmap(s));
            }
            return result;
        }


        //Со второго
        public List<Bitmap> GetBitmapListMedium()
        {
            List<Bitmap> result = new List<Bitmap>();
            List<string> fileNames = new List<string>();
            fileNames = repo.GetNamesByLevel(2);
            foreach (string s in fileNames)
            {
                result.Add(new Bitmap(s));
            }
            return result;
        }

        //С третьего
        public List<Bitmap> GetBitmapListHard()
        {
            List<Bitmap> result = new List<Bitmap>();
            List<string> fileNames = new List<string>();
            fileNames = repo.GetNamesByLevel(3);
            foreach (string s in fileNames)
            {
                result.Add(new Bitmap(s));
            }
            return result;
        }


        //Добавление своей картинки  (name - полный путь к файлу)
        public void AddPictureToEasyList(string name)
        {
            repo.AddToDataBase(name, 1);
        }

        public void AddPictureToMediumList(string name)
        {
            repo.AddToDataBase(name, 2);
        }

        public void AddPictureToHardList(string name)
        {
            repo.AddToDataBase(name, 3);
        }


        //Получить массив Image по уровням
        public List<Image> GetImageListEasy()
        {
            List<Image> result = new List<Image>();
            List<string> fileNames = new List<string>();
            fileNames = repo.GetNamesByLevel(1);
            foreach (string s in fileNames)
            {
                result.Add(Image.FromFile(s));
            }
            return result;
        }

        public List<Image> GetImageListMedium()
        {
            List<Image> result = new List<Image>();
            List<string> fileNames = new List<string>();
            fileNames = repo.GetNamesByLevel(2);
            foreach (string s in fileNames)
            {
                result.Add(Image.FromFile(s));
            }
            return result;
        }

        public List<Image> GetImageListHard()
        {
            List<Image> result = new List<Image>();
            List<string> fileNames = new List<string>();
            fileNames = repo.GetNamesByLevel(3);
            foreach (string s in fileNames)
            {
                result.Add(Image.FromFile(s));
            }
            return result;
        }
    }
}
