using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCRUD.DataAccess;

namespace ProjectCRUD.Pages
{
    public class IndexModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public int BookCount { get; set; }
        public int CategoryCount { get; set; }
        public int MemberCount { get; set; }
        [FromQuery(Name = "action")]
        public string Action { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            BookCount = 0;
            CategoryCount = 0;
            MemberCount = 0;
            ErrorMessage = "";
        }

        public void OnGet()
        {
            if (!String.IsNullOrEmpty(Action) && Action.ToLower() == "logout")
            {
                Logout();
                return;
            }
            var dashBoardData = new DashBoardData();
            var dashboard = dashBoardData.GetAll();
            if (dashboard != null)
            {
                BookCount = dashboard.BookCount;
                CategoryCount = dashboard.CategoryCount;
                MemberCount = dashboard.MemberCount;
            }
            else
            {
                ErrorMessage = $"No Dashboard Data Available - {dashBoardData.ErrorMessage}";
            }

        }
        private void Logout()
        {
            HttpContext.SignOutAsync();
            Response.Redirect("/Index");
        }

        public void OnPost()
        {
            Logout();
        }
    }
}