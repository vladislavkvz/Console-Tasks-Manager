namespace TaskManager.Repositories
{
    using System;
    using System.IO;
    using Entities;

    public class CommentRepo : BaseRepo<Comment>
    {
        public CommentRepo(string filePath) : base(filePath)
        {
        }

        protected override void ReadFromStream(StreamReader sr, Comment entity)
        {
            entity.ParentId = Convert.ToInt32(sr.ReadLine());
            entity.Comments = sr.ReadLine();
            entity.CreatedBy = Convert.ToInt32(sr.ReadLine());
        }

        protected override void WriteToStream(StreamWriter sw, Comment entity)
        {
            sw.WriteLine(entity.ParentId);
            sw.WriteLine(entity.Comments);
            sw.WriteLine(entity.CreatedBy);
        }
    }
}