using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Plans
{
    public class AddModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        [Display(Name = "Plan_Validity")]
        public string Plan_Validity { get; set; }
        [BindProperty]
        [Display(Name = "Plan_Validity")]
        public decimal Amount { get; set; }
        public AddModel()
        {

            SuccessMessage = "";
            ErrorMessage = "";
            Plan_Validity = "";
            Amount = 0;
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
            var planDataAccess = new PlanDataAccess();
            var newCatg = new PlanDataModel

            {
                Plan_Validity = Plan_Validity,
                Amount=Amount

            };
            var insertedCatg = planDataAccess.Insert(newCatg);

            if (insertedCatg != null && insertedCatg.Id > 0)
            {
                SuccessMessage = $"Successfully Inserted Plan {insertedCatg.Id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please try Again {planDataAccess.ErrorMessage}";
            }
        }
    }
}
