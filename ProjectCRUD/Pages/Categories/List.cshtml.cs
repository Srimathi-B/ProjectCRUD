using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;

namespace ProjectCRUD.Pages.Categories
{
    public class ListModel : PageModel
    {
        public List<CategoryDataModel> Catgs { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        public string SearchText { get; set; }
        public ListModel()
        {
            Catgs = new List<CategoryDataModel>();
            SuccessMessage = "";
            ErrorMessage = "";
            SearchText = "";
        }
        public void OnGet()
        {
            var categoryDataAccess = new CategoryDataAccess();
            Catgs = categoryDataAccess.GetAll();
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
            CategoryDataAccess catg = new CategoryDataAccess();
            Catgs = catg.GetCatgByName(SearchText);

            if (catg != null)
            {
                SuccessMessage = $"Category name is found";
            }
            else
            {
                ErrorMessage = $"Category not Found";
            }
        }

        public void OnPostClear()
        {
            SearchText = "";
            ModelState.Clear();
            var categoryDataAccess = new CategoryDataAccess();
            Catgs = categoryDataAccess.GetAll();
        }
    }
}

