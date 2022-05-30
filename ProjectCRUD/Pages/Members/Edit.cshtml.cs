using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Members
{
    public class EditModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        public int Id { get; set; }
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
        [BindProperty]
        [Display(Name = "UserName")]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }
        [BindProperty]
        [Display(Name = "Password")]
        [MinLength(3)]
        [MaxLength(20)]
        public string Password { get; set; }
        [BindProperty]
        [Display(Name = "MemStatus")]
        public string MemStatus { get; set; }
        public EditModel()
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
            Password="";
            MemStatus = "";

        }
        private List<SelectListItem> GetGenders()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Male", Value = "M" });
            selectItems.Add(new SelectListItem { Text = "Female", Value = "F" });
            selectItems.Add(new SelectListItem { Text = "Unspecified", Value = "U" });
            return selectItems;
        }
        public void OnGet(int id)
        {
            Id = id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }

            var memberDataAccess = new MemberDataAccess();
            var member = memberDataAccess.GetMemberById(id);

            if (member != null)
            {
                FirstName = member.FirstName;
                LastName = member.LastName;
                Gender = member.Gender;
                DOB = member.DOB;
                EmailId = member.EmailId;
                MobileNumber = member.MobileNumber;
                UserName = member.UserName;
                Password = member.Password;
                MemStatus = member.MemStatus;
            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }
        }
        public void OnPost()
        {

            var memberDataAccess = new MemberDataAccess();
            var updMember = new MemberDataModel
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                DOB = DOB,
                EmailId = EmailId,
                MobileNumber = MobileNumber,
                UserName=UserName,
                Password=Password,
                MemStatus=MemStatus
                
                

            };
            var result = memberDataAccess.Update(updMember);

            if (result)
            {
                SuccessMessage = "Member Updated Successfully";
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = $"Error! Updating Member - {memberDataAccess.ErrorMessage}";
                SuccessMessage = "";
            }
        }
    }
}
