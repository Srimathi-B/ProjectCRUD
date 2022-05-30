using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Members
{
    public class AddModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        [Display(Name = "FirstName")]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [BindProperty]
        [Display(Name = "LastName")]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }
        [BindProperty]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [BindProperty]
        public List<SelectListItem> Genders { get; set; }
        [BindProperty]
        [Display(Name = "DOB")]
        public DateTime DOB { get; set; }
        [BindProperty]
        [Display(Name = "EmailId")]
        [MinLength(3)]
        [MaxLength(20)]
        public string EmailId { get; set; }
        [BindProperty]
        [Display(Name = "MobileNumber")]
        [MaxLength(10)]
        public string MobileNumber { get; set; }
        public decimal Price { get; set; }
        [BindProperty]
        [Display(Name = "BookStatus")]
        public string Status { get; set; }
        [BindProperty]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [MinLength(3)]
        [MaxLength(20)]

        [BindProperty]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public AddModel()
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

        }
        private List<SelectListItem> GetGenders()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Male", Value = "M" });
            selectItems.Add(new SelectListItem { Text = "Female", Value = "F" });
            selectItems.Add(new SelectListItem { Text = "Unspecified", Value = "U" });
            return selectItems;
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {

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
                Gender=Gender,
                DOB = DOB,
                EmailId = EmailId,
                MobileNumber = MobileNumber,
                UserName=UserName,
                Password=Password
            };
            var insertedMember = memberDataAccess.Insert(newMember);

            if (insertedMember != null && insertedMember.Id > 0)
            {
                SuccessMessage = $"Successfully Inserted Member {insertedMember.Id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please try Again {memberDataAccess.ErrorMessage}";
            }
        }
    }
}
