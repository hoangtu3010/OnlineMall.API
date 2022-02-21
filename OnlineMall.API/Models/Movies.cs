using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineMall.API.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Trailer { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Time)]
        public DateTime PremiereTime { get; set; }
        public string Duration { get; set; }
        public int? Genre_Id { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
