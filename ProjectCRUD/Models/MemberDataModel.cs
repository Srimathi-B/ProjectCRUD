namespace ProjectCRUD.Models
{
    public class MemberDataModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string MemStatus { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public MemberDataModel()
        {
            Id= 0;
            FirstName = "";
            LastName = "";
            Gender = "";
            DOB= DateTime.Now;
            EmailId = "";
            MobileNumber = "";
            MemStatus = "";
            UserName = "";
            Password = "";
        }
    }
}
