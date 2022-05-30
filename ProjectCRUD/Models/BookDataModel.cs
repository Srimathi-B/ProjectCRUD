namespace ProjectCRUD.Models
{
    public class BookDataModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishedYear { get; set; }
        public Decimal Price { get; set; }
        public string Status { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public BookDataModel()
        {
            Id = 0;
            BookName = "";
            AuthorName = "";
            PublishedYear = DateTime.Now;
            Price = new Decimal(0);
            Status = "";
            CategoryName = "";
            CategoryId = 0;
        }
    }
}
