using System.ComponentModel.DataAnnotations;
using System;
namespace CRUDelicious.Models
{
    public class Dishes
    {
        // auto-implemented properties need to match columns in your table
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string name { get; set; }
        [Required]
        [MinLength(3)]
        public string chef { get; set; }
        [Required]
        [Range(1,5)]
        public int tastiness { get; set; }
        [Required]
        [Range(1, 5000)]
        public int calories { get; set; }
        [Required]
        [MinLength(3)]
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public Dishes(){
            created_at=DateTime.Now;
            updated_at=DateTime.Now;
        }

    }
}