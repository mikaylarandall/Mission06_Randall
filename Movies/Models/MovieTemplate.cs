using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class MovieTemplate
    {
        // building the database
        [Key]
        [Required]
        public int MovieId { get; set; }
        public string Title { get; set; }

        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required]
        public int Year { get; set; }
        public string? Director { get; set; }
        public string? Rating { get; set; }
        [Required]
        public bool Edited { get; set; }
        public string? LentTo { get; set; }
        [Required]
        public bool CopiedToPlex { get; set; }
        public string? Notes { get; set; }
    }
}
