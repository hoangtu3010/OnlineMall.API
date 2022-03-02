using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineMall.API.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Contact { get; set; }
        [DataType(DataType.Time)]
        public DateTime ShowTime { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
