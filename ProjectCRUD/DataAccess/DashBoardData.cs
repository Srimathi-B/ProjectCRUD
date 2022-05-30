using ProjectCRUD.Models;
using System.Data.SqlClient;

namespace ProjectCRUD.DataAccess
{
    public class DashBoardData
    {
        public string ErrorMessage { get; set; }
        public DashBoard GetAll()
        {
            try
            {

                ErrorMessage = String.Empty;
                ErrorMessage = "";
                var d = new DashBoard();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select count(*) as BookCount from Book";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.BookCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    sqlStmt = "select count(*) as CategoryCount from Category";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.CategoryCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as MemberCount from Member";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.MemberCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return d;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
    }
}
