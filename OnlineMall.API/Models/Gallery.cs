﻿namespace OnlineMall.API.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? User_Id { get; set; }
        public virtual User User { get; set; }
    }
}
