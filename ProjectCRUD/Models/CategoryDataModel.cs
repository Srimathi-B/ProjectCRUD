namespace ProjectCRUD.Models
{
    public class CategoryDataModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public CategoryDataModel()
        {
            Id= 0;
            CategoryName = "";
        }
    }
}
