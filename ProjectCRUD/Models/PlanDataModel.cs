namespace ProjectCRUD.Models
{
    public class PlanDataModel
    {
        public int Id { get; set; }
        public string Plan_Validity { get; set; }
        public decimal Amount { get; set; }

        public PlanDataModel()
        {
           
            Id = 0;
            Plan_Validity = "";
            Amount = 0;
        }
    }
}
