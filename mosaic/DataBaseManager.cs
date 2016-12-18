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
    }
}
