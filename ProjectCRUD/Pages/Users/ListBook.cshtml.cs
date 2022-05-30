using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;

namespace ProjectCRUD.Pages.Users
{
    public class ListBookModel : PageModel
    {
        public List<BookDataModel> Books { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        public string SearchText { get; set; }
        public ListBookModel()
        {
            Books = new List<BookDataModel>();
            SuccessMessage = "";
            ErrorMessage = "";
            SearchText = "";
        }
        public void OnGet()
        {
            var bookDataAccess = new BookDataAccess();
            Books = bookDataAccess.GetAll();
        }
        public void OnPostSearch()
        {

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please try again";
                return;
            }
            if (string.IsNullOrEmpty(SearchText))
            {
                ErrorMessage = $"Please Input a string ";
            }
            BookDataAccess book = new BookDataAccess();
            Books = book.GetBookByName(SearchText);

            if (book != null)
            {
                SuccessMessage = $"Book Name is found";
            }
            else
            {
                ErrorMessage = $"Book not Found";
            }
        }

        public void OnPostClear()
        {
            SearchText = "";
            ModelState.Clear();
            var bookDataAccess = new BookDataAccess();
            Books = bookDataAccess.GetAll();
        }
    }
}
