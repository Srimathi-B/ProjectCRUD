using ProjectCRUD.Models;
using System.Data.SqlClient;

namespace ProjectCRUD.DataAccess
{
    public class ActivationDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<ActivationDataModel> GetAll()
        {
            try
            {
                ErrorMessage = string.Empty;
                List<ActivationDataModel> datas = new List<ActivationDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt =  " SELECT A.Id,M.FirstName,A.Plan_Id,A.Plan_Start,A.Plan_End,P.Plan_Validity,M.MemStatus " +
                                    " FROM [dbo].[Activation] AS A " +
                                    " INNER JOIN [dbo].[Plan] AS P ON A.Plan_Id = P.Id " +
                                    " INNER JOIN [dbo].[Member] AS M ON A.Mem_Id = M.Id ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                ActivationDataModel data = new ActivationDataModel();
                                data.Id = reader.GetInt32(0);
                                data.FirstName = reader.GetString(1);
                                data.Plan_Id = reader.GetInt32(2);
                                data.Plan_Start = reader.GetDateTime(3);
                                data.Plan_End = reader.GetDateTime(4);
                                data.Plan_Validity = reader.GetString(5);
                                data.MemStatus = reader.GetString(6);
                                datas.Add(data);
                            }
                        }
                    }
                }
                return datas;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //Insert 

       public ActivationDataModel Insert(ActivationDataModel newData)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Activation (Mem_Id, Plan_Id, Plan_Start,Plan_End) VALUES ({newData.Mem_Id},{newData.Plan_Id},'{newData.Plan_Start.ToString("yyyy-MM-dd")}','{newData.Plan_End.ToString("yyyy-MM-dd")}'); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newData.Id = idInserted;
                            return newData;
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
        public ActivationDataModel Update(ActivationDataModel updData)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Activation SET Mem_Id = {updData.Mem_Id}, Plan_Id = {updData.Plan_Id} ," +
                        $"Plan_Start = '{updData.Plan_Start.ToString("yyyy-MM-dd")}', " +
                        $"Plan_End = '{updData.Plan_End.ToString("yyyy-MM-dd")}' " +
                        $"where Id = {updData.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updData;
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

        //Get Activation
        public List<ActivationDataModel> GetActivationbyUser()
        {
            try
            {
                ErrorMessage = string.Empty;
                List<ActivationDataModel> datas = new List<ActivationDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"SELECT A.Id,A.Mem_Id,A.Plan_Id,A.Plan_Start,A.Plan_End,P.Plan_Validity,P.Amount " +
                                    " FROM [dbo].[Activation] AS A " +
                                    " INNER JOIN [dbo].[Plan] AS P ON A.Plan_Id = P.Id " +
                                    " INNER JOIN [dbo].[Member] AS M ON A.Mem_Id = M.Id; ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                ActivationDataModel data = new ActivationDataModel();
                                data.Id = reader.GetInt32(0);
                                data.Mem_Id = reader.GetInt32(1);
                                data.Plan_Id = reader.GetInt32(2);
                                data.Plan_Start = reader.GetDateTime(3);
                                data.Plan_End = reader.GetDateTime(4);
                                data.Plan_Validity = reader.GetString(5);
                                data.Amount = reader.GetDecimal(6);
                                datas.Add(data);
                            }
                        }
                    }
                }
                return datas;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
