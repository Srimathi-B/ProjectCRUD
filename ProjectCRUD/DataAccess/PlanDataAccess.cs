using ProjectCRUD.Models;
using System.Data.SqlClient;

namespace ProjectCRUD.DataAccess
{
    public class PlanDataAccess
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public PlanDataAccess()
        {
            SuccessMessage = "";
            ErrorMessage = "";
        }
        public List<PlanDataModel> GetAll()
        {
            try
            {
                //ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<PlanDataModel> plans = new List<PlanDataModel>();

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id, Plan_Validity, Amount from dbo.[Plan]";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                PlanDataModel plan = new PlanDataModel();
                                plan.Id = reader.GetInt32(0);
                                plan.Plan_Validity = reader.GetString(1);
                                plan.Amount = reader.GetDecimal(2);

                                plans.Add(plan);
                            }
                        }
                    }
                }

                return plans;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //Insert 

        public PlanDataModel Insert(PlanDataModel newPlan)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.[Plan] (Plan_Validity,Amount) VALUES ('{newPlan.Plan_Validity}',{newPlan.Amount}); SELECT SCOPE_IDENTITY();";


                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newPlan.Id = idInserted;
                            return newPlan;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


        //Update 
        public bool Update(PlanDataModel updPlan)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.[Plan] SET Plan_Validity = '{updPlan.Plan_Validity}', " +
                        $"Amount = {updPlan.Amount} " +
                        $"where Id = {updPlan.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }
            return false;
        }

        //Get by Id
        public PlanDataModel GetPlanById(int id)
        {
            try
            {
                ErrorMessage = string.Empty;
                PlanDataModel plan = null;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"SELECT Id, Plan_Validity,Amount FROM [Plan] where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                plan = new PlanDataModel();
                                plan.Id = reader.GetInt32(0);
                                plan.Plan_Validity = reader.GetString(1);
                                plan.Amount = reader.GetDecimal(2);
                            }
                        }
                    }
                }
                return plan;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
