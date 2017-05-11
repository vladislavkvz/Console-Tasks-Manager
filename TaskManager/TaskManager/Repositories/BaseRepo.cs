namespace TaskManager.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Entities;

    public abstract class BaseRepo<T> where T : BaseEntity, new()
    {
        protected abstract void ReadFromStream(StreamReader sr, T entity);

        protected abstract void WriteToStream(StreamWriter sw, T entity);

        protected readonly string filePath;

        public BaseRepo(string filePath)
        {
            this.filePath = filePath;
        }

        private int GetNextId()
        {
            int id = 1;
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        T item = new T();
                        item.Id = Convert.ToInt32(sr.ReadLine());
                        ReadFromStream(sr, item);
                        if (id <= item.Id)
                        {
                            id = item.Id + 1;
                        }
                    }
                }
            }
            return id;
        }

        private void Insert(T item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                WriteToStream(sw, item);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(T item)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T entity = new T();
                    entity.Id = Convert.ToInt32(sr.ReadLine());
                    ReadFromStream(sr, entity);

                    if (entity.Id != item.Id)
                    {
                        sw.WriteLine(entity.Id);
                        WriteToStream(sw, entity);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        WriteToStream(sw, item);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public T GetById(int id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T entity = new T();

                    entity.Id = Convert.ToInt32(sr.ReadLine());
                    ReadFromStream(sr, entity);
                    if (entity.Id == id)
                    {
                        return entity;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
            return null;
        }

        public List<T> GetAll()
        {
            List<T> result = new List<T>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T entity = new T();
                    entity.Id = Convert.ToInt32(sr.ReadLine());
                    ReadFromStream(sr, entity);
                    result.Add(entity);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
            return result;
        }

        public void Delete(T item)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T entity = new T();
                    entity.Id = Convert.ToInt32(sr.ReadLine());
                    ReadFromStream(sr, entity);

                    if (entity.Id != item.Id)
                    {
                        sw.WriteLine(entity.Id);
                        WriteToStream(sw, entity);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }
            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public void Save(T item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }
    }
}