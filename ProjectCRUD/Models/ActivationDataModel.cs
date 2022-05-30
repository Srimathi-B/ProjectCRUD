namespace ProjectCRUD.Models
{
    public class ActivationDataModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int Mem_Id { get; set; }
        public int Plan_Id { get; set; }
        public DateTime Plan_Start { get; set; }
        public DateTime Plan_End { get; set; }
        public string Plan_Validity { get; set; }
        public string MemStatus { get; set; }
        public decimal Amount { get; set; }

        public ActivationDataModel()
        {
            Id = 0;
            FirstName = "";
            Plan_Id = 0;
            Mem_Id = 0;
            Plan_Start = DateTime.Now;
            Plan_End = DateTime.Now;
            Plan_Validity = "";
            MemStatus = "";
            Amount = 0;
        }
    }
}
