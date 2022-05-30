using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Users
{
    public class PayFeesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        [Display(Name = "Plan_Start")]
        [DataType(DataType.Date)]
        public DateTime Plan_Start { get; set; }
        [BindProperty]
        [Display(Name = "Plan_End")]
        [DataType(DataType.Date)]
        public DateTime Plan_End { get; set; }
        [BindProperty]
        [Display(Name = "Plan_Validity")]
        public int SelectedPlanId { get; set; }
        [BindProperty]
        public List<SelectListItem> Plans { get; set; }
        [BindProperty]
        [Display(Name = "Amount")]
        public int SelectedAmountId { get; set; }
        [BindProperty]
        public List<SelectListItem> Amount{get; set;}
        [BindProperty]
        [Display(Name = "MemberStatus")]
        public int SelectedMemberId { get; set; }
        [BindProperty]
        public List<SelectListItem> Members { get; set; }
        public PayFeesModel()
        {
            SuccessMessage = "";
            ErrorMessage = "";
            Plan_Start = DateTime.Now;
            Plan_End = DateTime.Now;
            SelectedPlanId = 0;
            SelectedMemberId = 0;
            SelectedAmountId = 0;
            Plans = GetPlanValidity();
            Members = GetMemberStatus();
            Amount = GetAmounts();
        }
        private List<SelectListItem> GetPlanValidity()
        {
            var planDataAccess = new PlanDataAccess();
            var plans = planDataAccess.GetAll();
            var planList = new List<SelectListItem>();

            foreach (var c in plans)
            {
                planList.Add(new SelectListItem
                {
                    Text = c.Plan_Validity,
                    Value = c.Id.ToString()
                });
            }

            return planList;
        }
        private List<SelectListItem> GetAmounts()
        {
            var planDataAccess = new PlanDataAccess();
            var plans = planDataAccess.GetAll();
            var planList = new List<SelectListItem>();

            foreach (var c in plans)
            {
                planList.Add(new SelectListItem
                {
                    Text = c.Amount.ToString(),
                    Value = c.Id.ToString()
                });
            }

            return planList;
        }
        private List<SelectListItem> GetMemberStatus()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Pending", Value = "P" });
            selectItems.Add(new SelectListItem { Text = "Activation", Value = "A" });
            selectItems.Add(new SelectListItem { Text = "InActivate", Value = "I" });
            return selectItems;
        }
        public void OnGet()
        {
        }
        public async void OnPost()
        {
            Plans = GetPlanValidity();
            Members = GetMemberStatus();
            Amount = GetAmounts();
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please try again";
                return;
            }
            var activationDataAccess = new ActivationDataAccess();
            var newData = new ActivationDataModel

            {
                Id = Id,
                Plan_Start = Plan_Start,
                Plan_End = Plan_End,
                Plan_Validity = "",
                //Amount=Amount


            };
            var insertedData = activationDataAccess.Insert(newData);

            if (insertedData != null && insertedData.Id > 0)
            {
                SuccessMessage = $"Plan Activated {insertedData.Id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please try Again {activationDataAccess.ErrorMessage}";
                return;
            }
        }
    }
}
