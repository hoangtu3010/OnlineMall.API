using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMall.API.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string ImageSrc { get; set; }
        public string ImageLogoName { get; set; }
        [NotMapped]
        public IFormFile ImageLogoFile { get; set; }
        public string ImageLogoSrc { get; set; }
        public string Contact { get; set; }
        [DataType(DataType.Time)]
        public DateTime ShowTime { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
