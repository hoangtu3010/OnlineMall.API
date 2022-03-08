using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMall.API.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string ImageSrc { get; set; }
        public decimal Price { get; set; }
        public string Trailer { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
