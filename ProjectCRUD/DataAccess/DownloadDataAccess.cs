using ProjectCRUD.Models;
using System.Data.SqlClient;

namespace ProjectCRUD.DataAccess
{
    public class DownloadDataAccess
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public DownloadDataAccess()
        {
            SuccessMessage = "";
            ErrorMessage = "";
        }
        public List<DownloadDataModel> GetAll()
        {
            try
            {
                //ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<DownloadDataModel> downloads = new List<DownloadDataModel>();

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id, Book_Id,Mem_Id,DownloadCount from dbo.[Download]";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                DownloadDataModel plan = new DownloadDataModel();
                                plan.Id = reader.GetInt32(0);
                                plan.Book_Id=reader.GetInt32(1);
                                plan.Mem_Id = reader.GetInt32(2);
                                plan.DownloadCount = reader.GetInt32(3);
                                downloads.Add(plan);
                            }
                        }
                    }
                }

                return downloads;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public DownloadDataModel GetDownloadbyId(int id)
        {
            try
            {
                //ErrorMessage = string.Empty;
                ErrorMessage = "";

                DownloadDataModel downloads = null;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select Id, Book_Id, Mem_Id,DownloadCount from dbo.[Download] where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                downloads = new DownloadDataModel();
                                downloads.Id = reader.GetInt32(0);
                                downloads.Book_Id = reader.GetInt32(1);
                                downloads.Mem_Id = reader.GetInt32(2);
                                downloads.DownloadCount = reader.GetInt32(3);
                            }
                        }
                    }
                }

                return downloads;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public bool Update(DownloadDataModel updDown)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.[Plan] SET Plan_Validity = {updDown.Book_Id}, " +
                        $"Amount = {updDown.Mem_Id} ," +
                        $"DownloadCount={updDown.DownloadCount} " +
                        $"where Id = {updDown.Id}";

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

    }
}
