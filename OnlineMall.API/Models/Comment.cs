namespace OnlineMall.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int? GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }
        public int? ReplyId { get; set; }
    }
}
