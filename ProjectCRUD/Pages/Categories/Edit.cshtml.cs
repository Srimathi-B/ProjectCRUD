using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Categories
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        [Display(Name = "CategoryName")]
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string CategoryName { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public EditModel()
        {

            SuccessMessage = "";
            ErrorMessage = "";
            CategoryName = "";
        }
        public void OnGet(int id)
        {
            Id = id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }

            var categoryDataAccess = new CategoryDataAccess();
            var catg = categoryDataAccess.GetCatgbyId(id);

            if (catg != null)
            {
                CategoryName = catg.CategoryName;
            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }

        }
        public void OnPost()
        {

            var categoryDataAccess = new CategoryDataAccess();
            var updCatg = new CategoryDataModel
            {
                Id = Id,
                CategoryName = CategoryName
            };
            var result = categoryDataAccess.Update(updCatg);

            if (result)
            {
                SuccessMessage = "Category Updated Successfully";
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = $"Error! Updating Category - {categoryDataAccess.ErrorMessage}";
                SuccessMessage = "";
            }
        }
    }
}
