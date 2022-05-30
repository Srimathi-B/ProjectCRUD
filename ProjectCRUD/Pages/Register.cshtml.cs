using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages
{
    public class RegisterModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [BindProperty]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [BindProperty]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [BindProperty]
        public List<SelectListItem> Genders { get; set; }
        [BindProperty]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [BindProperty]
        [Display(Name = "EmailId")]
        public string EmailId { get; set; }
        [BindProperty]
        [Display(Name = "MobileNumber")]
        public string MobileNumber { get; set; }
        [BindProperty]
        [Display(Name = "MemberStatus")]
        public string MemStatus { get; set; }
        [BindProperty]
        public List<SelectListItem> StatusList { get; set; }
        [BindProperty]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [BindProperty]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public RegisterModel()
        {

            SuccessMessage = "";
            ErrorMessage = "";
            FirstName = "";
            LastName = "";
            Gender = "";
            DOB = DateTime.Now;
            EmailId = "";
            MobileNumber = "";
            Genders = GetGenders();
            UserName = "";
            Password = "";
            MemStatus = "";
            StatusList = GetStatus();

        }
        private List<SelectListItem> GetGenders()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Male", Value = "M" });
            selectItems.Add(new SelectListItem { Text = "Female", Value = "F" });
            selectItems.Add(new SelectListItem { Text = "Unspecified", Value = "U" });
            return selectItems;
        }
        private List<SelectListItem> GetStatus()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Pending", Value = "P" });
            selectItems.Add(new SelectListItem { Text = "Active", Value = "A" });
            selectItems.Add(new SelectListItem { Text = "InActive", Value = "I" });
            return selectItems;
        }
        public void OnGet()
        {

        }
        public void OnPost()
        {
            StatusList = GetStatus();
            Genders = GetGenders();
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please try again";
                return;
            }
            var memberDataAccess = new MemberDataAccess();
            var newMember = new MemberDataModel

            {
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                DOB = DOB,
                EmailId = EmailId,
                MobileNumber = MobileNumber,
                UserName=UserName,
                Password= Password
            };
            var insertedMember = memberDataAccess.Insert(newMember);

            if (insertedMember != null && insertedMember.Id > 0)
            {
                SuccessMessage = $"Successfully Inserted Member {insertedMember.Id}";
                ModelState.Clear();

                //Response.Redirect($"/Activation/{insertedMember.Id}");
            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please try Again {memberDataAccess.ErrorMessage}";
            }
        }
    }
}
