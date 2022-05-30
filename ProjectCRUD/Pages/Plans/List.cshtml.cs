using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;

namespace ProjectCRUD.Pages.Plans
{
    public class ListModel : PageModel
    {
        public List<PlanDataModel> Plans { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public ListModel()
        {
            Plans = new List<PlanDataModel>();
            SuccessMessage = "";
            ErrorMessage = "";
        }
        public void OnGet()
        {
            var planDataAccess = new PlanDataAccess();
            Plans = planDataAccess.GetAll();
        }
    }
}
