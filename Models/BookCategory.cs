namespace bookStore.Models
{
    public class BookCategory
    {
        public int bookId { get; set; }
        public Book books { get; set; } = null!;
        public int categoryId { get; set; }
        public Category categories { get; set; } = null!;

    }
}
