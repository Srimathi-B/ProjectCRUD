using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ProjectCRUD.Pages.Members
{
    public class ActivationModel : PageModel
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
        [Display(Name = "MemberStatus")]
        public string SelectedMemberStatus { get; set; }
        [BindProperty]
        public List<SelectListItem> Members { get; set; }
        public ActivationModel()
        {
            SuccessMessage = "";
            ErrorMessage = "";
            Plan_Start = DateTime.Now;
            Plan_End = DateTime.Now;
            SelectedPlanId = 0;
            SelectedMemberStatus = "P";
            Plans = GetPlanValidity();
            Members = GetMemberStatus();
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
        private List<SelectListItem> GetMemberStatus()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Pending", Value = "P" });
            selectItems.Add(new SelectListItem { Text = "Activation", Value = "A" });
            selectItems.Add(new SelectListItem { Text = "InActivate", Value = "I" });
            return selectItems;
        }

        public void OnGet(int id)
        {
            Id = id;
            
        }

        public async void OnPost()
        {
            // ActivationDataAccess.Insert
            Plans = GetPlanValidity();
            Members = GetMemberStatus();
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please try again";
                return;
            }
            var activationDataAccess = new ActivationDataAccess();
            var newData = new ActivationDataModel

            {
                Mem_Id = Id,
                Plan_Id = SelectedPlanId,
                Plan_Start = Plan_Start,
                Plan_End = Plan_End,
                Plan_Validity = ""
                

            };
             var insertedData = activationDataAccess.Insert(newData);
            if (insertedData != null && insertedData.Id > 0)
            {
                SuccessMessage = $"Successfully Activated  {insertedData.Id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please try Again {activationDataAccess.ErrorMessage}";
                return;
            }


            //SignIn on Insert Success

        }
    }
}

