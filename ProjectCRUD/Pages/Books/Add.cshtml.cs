using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Books
{
    public class AddModel : PageModel
    {
        
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        [Display(Name="BookName")]
        [MinLength(3)]
        [MaxLength(20)]
        public string BookName { get; set; }
        [BindProperty]
        [Display(Name = "AuthorName")]
        [MinLength(3)]
        [MaxLength(20)]
        public string AuthorName { get; set; }
        [BindProperty]
        [Display(Name = "PublishedYear")]
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
        public AddModel()
        {
            
            SuccessMessage = "";
            ErrorMessage = "";
            BookName = "";
            AuthorName = "";
            PublishedYear=DateTime.Now;
            Price =new decimal(0);
            Status = "";
            CategoryId = 0;
            StatusList = GetStatus();
            CategoryList = GetCategory();
        }
        public void OnGet()
        {
            
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

        public void OnPost()
        {

            StatusList = GetStatus();
            CategoryList = GetCategory();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please try again";
                return;
            }
            var bookDataAccess = new BookDataAccess();
            var newBook = new BookDataModel

            {
                BookName = BookName,
                AuthorName = AuthorName,
                PublishedYear = PublishedYear,
                Price = Price,
                Status = Status,
                CategoryId = CategoryId
            };
            var insertedCatg = bookDataAccess.Insert(newBook);

            if (insertedCatg != null && insertedCatg.Id > 0)
            {
                SuccessMessage = $"Successfully Inserted Book {insertedCatg.Id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please try Again {bookDataAccess.ErrorMessage}";
            }
        }
    }
}
