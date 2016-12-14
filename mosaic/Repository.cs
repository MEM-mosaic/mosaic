using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace mosaic
{
    class Repository
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory + "mems\\";

        private class DataSet
        {
            public List<Links> Links { get; set; }
        }

        DataSet _dataset;

        public IEnumerable<Links> Links
        {
            get
            {
                return _dataset.Links;
            }
        }

        public Repository()
        {
            _dataset = new DataSet();
            _dataset.Links = new List<Links>();
            //Seed();
        }

        protected void Seed()
        {
            using (var c = new Context())
            {
               // c.Links.ToList();
                for (int i = 1; i < 4; i++)
                {
                    List<string> fileNames = GetPicturesNames(i.ToString());
                    for (int k = 0; k < fileNames.Count; k++)
                    {
                        Links l = new Links();
                        l.Level = i;
                        l.Name = fileNames[k];
                        l.Link = path + fileNames[k];
                        _dataset.Links.Add(l);
                        c.Links.AddOrUpdate(l);
                    }
                }
                c.SaveChanges();
            }
        }

        public List<string> GetPicturesNames(string s)
        {
            List<string> names = new List<string>();
            DirectoryInfo d = new DirectoryInfo(path + s + "\\");
            FileInfo[] Files1 = d.GetFiles("*.jpg");
            foreach (FileInfo file in Files1)
            {
                names.Add(file.Name);

            }

            FileInfo[] Files2 = d.GetFiles("*.png");
            foreach (FileInfo file in Files2)
            {
                names.Add(file.Name);
            }
            return names;
        }

        public List<string> GetNamesByLevel(int level) //1 - easy; 2 - medium; 3 - hard
        {
            List<string> names = new List<string>();
            using (var c = new Context())
            {
                var res = from l in c.Links where l.Level == level select l.Link;
                foreach (string s in res)
                    names.Add(s);
            }
            return names;
        }

        public void AddToDataBase(string name, int level)
        {
            string destFile = System.IO.Path.Combine(path + level.ToString() + "\\", name);
            if (!System.IO.Directory.Exists(path + level.ToString() + "\\"))
            {
                System.IO.Directory.CreateDirectory(path + level.ToString() + "\\");
            }


            //   System.IO.File.Copy(name, destFile, true);


            FileStream reader = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            FileStream writer = File.Create(destFile);
            while (reader.Position < reader.Length)
            {
                writer.WriteByte((byte)reader.ReadByte());
            }
            reader.Close();
            writer.Close();


            using (var c = new Context())
            {
                Links l = new Links();
                l.Name = name;
                l.Link = name;
                l.Level = level;
                c.Links.AddOrUpdate(l);
            }
        }
    }
}

