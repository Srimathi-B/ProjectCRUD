using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Categories
{
    public class AddModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        [Display(Name = "CategoryName")]
        [MinLength(3)]
        [MaxLength(20)]
        public string CategoryName { get; set; }
        public AddModel()
        {

            SuccessMessage = "";
            ErrorMessage = "";
            CategoryName = "";
        }
            public void OnGet()
        {
        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please try again";
                return;
            }
            var categoryDataAccess = new CategoryDataAccess();
            var newCatg = new CategoryDataModel

            {
                CategoryName = CategoryName

            };
            var insertedCatg = categoryDataAccess.Insert(newCatg);

            if (insertedCatg != null && insertedCatg.Id > 0)
            {
                SuccessMessage = $"Successfully Inserted Category {insertedCatg.Id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please try Again {categoryDataAccess.ErrorMessage}";
            }
        }
    }
}
