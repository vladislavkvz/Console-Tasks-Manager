﻿namespace TaskManager.Repositories
{
    using System;
    using System.Globalization;
    using System.IO;

    public class TaskRepo : BaseRepo<Entities.Task>
    {
        public TaskRepo(string filePath)
            : base(filePath)
        {
        }

        protected override void ReadFromStream(StreamReader sr, Entities.Task entity)
        {
            entity.ParentId = Convert.ToInt32(sr.ReadLine());
            entity.Title = sr.ReadLine();
            entity.Description = sr.ReadLine();
            entity.ResponsibleUser = Convert.ToInt32(sr.ReadLine());
            entity.Creator = sr.ReadLine();
            entity.Status = sr.ReadLine();
            entity.LastChange = DateTime.ParseExact(sr.ReadLine(), "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            entity.CreateTime = DateTime.ParseExact(sr.ReadLine(), "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }

        protected override void WriteToStream(StreamWriter sw, Entities.Task entity)
        {
            sw.WriteLine(entity.ParentId);
            sw.WriteLine(entity.Title);
            sw.WriteLine(entity.Description);
            sw.WriteLine(entity.ResponsibleUser);
            sw.WriteLine(entity.Creator);
            sw.WriteLine(entity.Status);
            sw.WriteLine(entity.LastChange.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture));
            sw.WriteLine(entity.CreateTime.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture));
        }
    }
}