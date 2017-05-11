namespace TaskManager.Entities
{
    public class Comment : BaseEntity
    {
        public int ParentId { get; set; }
        public string Comments { get; set; }
        public int CreatedBy { get; set; }
    }
}