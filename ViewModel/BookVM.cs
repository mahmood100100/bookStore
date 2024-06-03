using bookStore.Models;

namespace bookStore.ViewModel
{
    public class BookVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AutherName { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public string imageUrl { get; set; } = null!;
        public List<string> CategoriesNames { get; set; } = new List<string>();
    }
}
