using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        [Display(Name = "BookName")]
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string BookName { get; set; }
        [BindProperty]
        [Display(Name = "AuthorName")]
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string AuthorName { get; set; }
        [BindProperty]
        [Display(Name = "PublishedYear")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime PublishedYear { get; set; }
        [BindProperty]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [BindProperty]
        [Display(Name = "Status")]
        public string Status { get; set; }
        [BindProperty]
        public List<SelectListItem> StatusList { get; set; }
        [BindProperty]
        [Display(Name = "CategoryName")]
        [MinLength(3)]
        [MaxLength(20)]
        public int CategoryId { get; set; }
        [BindProperty]
        public List<SelectListItem> CategoryList { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public EditModel()
        {

            SuccessMessage = "";
            ErrorMessage = "";
            BookName = "";
            AuthorName = "";
            PublishedYear = DateTime.Now;
            Price = new decimal(0);
            Status = "";
            CategoryId = 0;
            StatusList = GetStatus();
            CategoryList = GetCategory();
        }
        private List<SelectListItem> GetStatus()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Pending", Value = "P" });
            selectItems.Add(new SelectListItem { Text = "Activation", Value = "A" });
            selectItems.Add(new SelectListItem { Text = "InActivate", Value = "I" });
            return selectItems;
        }
        private List<SelectListItem> GetCategory()
        {
            var categoryDataAccess = new CategoryDataAccess();
            var categories = categoryDataAccess.GetAll();
            var categoryList = new List<SelectListItem>();

            foreach (var c in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.Id.ToString()
                });
            }

            return categoryList;
        }
          public void OnGet(int id)
            {
                Id = id;

                if (Id <= 0)
                {
                    ErrorMessage = "Invalid Id";
                    return;
                }

                var bookDataAccess = new BookDataAccess();
                var book = bookDataAccess.GetBookbyId(id);

                if (book != null)
                {
                    BookName = book.BookName;
                    AuthorName = book.AuthorName;
                    PublishedYear = book.PublishedYear;
                    Price= book.Price;
                    Status = book.Status;
                    CategoryId = book.CategoryId;
                }
                else
                {
                    ErrorMessage = "No Record found with that Id";
                }
            
          }
        public void OnPost() { 

        var bookDataAccess = new BookDataAccess();
        var updBook = new BookDataModel
        {
            Id = Id,
            BookName = BookName,
            AuthorName = AuthorName,
            PublishedYear=PublishedYear,
            Price=Price,
            Status=Status,
            CategoryId=CategoryId

        };
        var result = bookDataAccess.Update(updBook);

            if (result)
            {
                SuccessMessage = "Book Updated Successfully";
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = $"Error! Updating Book - {bookDataAccess.ErrorMessage}";
                SuccessMessage = "";
            }
        }
    }
}
