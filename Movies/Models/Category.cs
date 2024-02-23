using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    // categories table
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
