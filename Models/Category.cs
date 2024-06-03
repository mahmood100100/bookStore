using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace bookStore.Models
{
    [Index(nameof(Name) , IsUnique =true)]
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<BookCategory> Books { get; set; } = new List<BookCategory>();
    }
}
