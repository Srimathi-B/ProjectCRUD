using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Plans
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        [Display(Name = "Plan_Validity")]
        [Required]
        public string Plan_Validity { get; set; }
        [BindProperty]
        [Display(Name = "Amount")]
        [Required]
        public decimal Amount { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public EditModel()
        {

            SuccessMessage = "";
            ErrorMessage = "";
            Plan_Validity = "";
            Amount = 0;
        }
        public void OnGet(int id)
        {
            Id = id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }

            var planDataAccess = new PlanDataAccess();
            var book = planDataAccess.GetPlanById(id);

            if (book != null)
            {
                Plan_Validity = book.Plan_Validity;
                Amount = book.Amount;
            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }

        }
        public void OnPost()
        {

            var planDataAccess = new PlanDataAccess();
            var updPlan = new PlanDataModel
            {
                Id = Id,
                Plan_Validity = Plan_Validity,
                Amount = Amount

            };
            var result = planDataAccess.Update(updPlan);

            if (result)
            {
                SuccessMessage = "Plan Updated Successfully";
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = $"Error! Updating Plan - {planDataAccess.ErrorMessage}";
                SuccessMessage = "";
            }
        }
    }
}
