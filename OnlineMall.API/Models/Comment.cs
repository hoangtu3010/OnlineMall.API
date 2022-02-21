namespace OnlineMall.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int? Gallery_Id { get; set; }
        public virtual Gallery Gallery { get; set; }
        public int? Reply_Id { get; set; }
    }
}
