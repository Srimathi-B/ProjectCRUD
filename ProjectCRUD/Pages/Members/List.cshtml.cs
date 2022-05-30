using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;

namespace ProjectCRUD.Pages.Members
{
    public class ListModel : PageModel
    {
        public List<MemberDataModel> Members { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public ListModel()
        {
            Members = new List<MemberDataModel>();
            SuccessMessage = "";
            ErrorMessage = "";
        }
        public void OnGet()
        {
            var memberDataAccess = new MemberDataAccess();
            Members = memberDataAccess.GetAll();
        }
    }
}
