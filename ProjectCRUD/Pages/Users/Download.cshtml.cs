using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCRUD.DataAccess;
using ProjectCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCRUD.Pages.Users
{
    public class DownloadModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        [Display(Name="Book Name")]
        public int SelectedId { get; set; }
        [BindProperty]
        public List<SelectListItem> BookNameList { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public int DownloadCount { get; set; }
        public DownloadModel()
        {
            Id = 0;
            ErrorMessage = "";
            SuccessMessage = "";
            DownloadCount = 4;
            BookNameList= GetBooks();
        }
        private List<SelectListItem> GetBooks()
        {
            var bookDataAccess = new BookDataAccess();
            var bookNameList = bookDataAccess.GetAll();

            var bookSelectList = new List<SelectListItem>();
            foreach (var d in bookNameList)
            {
                bookSelectList.Add(new SelectListItem
                {
                    Text = $"{d.BookName}",
                    Value = d.Id.ToString(),
                });
            }
            return bookSelectList;
        }
        public void OnGet(int id)
        {
            Id = id;

            if (Id <= 0)
            {
                ErrorMessage = "Sorry! You Already Downloaded 3 times";
                return;
            }

            var downloadDataAccess = new DownloadDataAccess();
            var d = downloadDataAccess.GetDownloadbyId(id);

            if (d != null)
            {
                DownloadCount = d.DownloadCount;
                Id = d.Id;
            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }
        }
        public void OnPost()
        {
            int Count = 0;
            if (DownloadCount > 0 && DownloadCount<=3)
            {
                Count = DownloadCount;
                SuccessMessage = "Download Complete";
                Count=Count+1;
            }
            else
            {
                ErrorMessage = "You already Downloaded 3 times";
            }
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please correct and try again.";
                return;
            }
            var downloadDataAccess = new DownloadDataAccess();
            var updDown = new DownloadDataModel
            {
                Id = Id,
                DownloadCount = DownloadCount
            };
            var d = downloadDataAccess.Update(updDown);

            if (d != null)
            {
                SuccessMessage = $"Download successfully";
            }
            else
            {
                ErrorMessage = $"Error! Updating Download{downloadDataAccess.ErrorMessage}";
            }
        }
    }

}

