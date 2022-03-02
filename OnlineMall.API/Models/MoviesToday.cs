using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineMall.API.Models
{
    public class MoviesToday
    {
        public int Id { get; set; }
        public int? MoviesId { get; set; }
        public virtual Movies Movies { get; set; }
        [DataType(DataType.Date)]
        public DateTime ShowDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime ShowTime { get; set; }
    }
}
