namespace bookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = null!;
        public int AutherId { get; set; }
        public Auther Auther { get; set; }
        public string Publisher { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt {  get; set; } = DateTime.Now;
        public List<BookCategory> Categories { get; set; } = new List<BookCategory>();

    } 
}
