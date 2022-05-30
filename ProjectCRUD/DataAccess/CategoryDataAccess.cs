using ProjectCRUD.Models;
using System.Data.SqlClient;

namespace ProjectCRUD.DataAccess
{
    public class CategoryDataAccess
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public CategoryDataAccess()
        {
            SuccessMessage = "";
            ErrorMessage = "";
        }
        public List<CategoryDataModel> GetAll()
        {
            try
            {
                //ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<CategoryDataModel> cats = new List<CategoryDataModel>();

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id,CategoryName from dbo.Category";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                CategoryDataModel cat = new CategoryDataModel();
                                cat.Id = reader.GetInt32(0);
                                cat.CategoryName = reader.GetString(1);
                                cats.Add(cat);
                            }
                        }
                    }
                }

                return cats;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Insert

        public CategoryDataModel Insert(CategoryDataModel newCatg)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Category (CategoryName) VALUES ('{newCatg.CategoryName}'); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newCatg.Id = idInserted;
                            return newCatg;
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

        //Get by Id
        public CategoryDataModel GetCatgbyId(int id)
        {
            try
            {
                //ErrorMessage = string.Empty;
                ErrorMessage = "";

                CategoryDataModel catg = null;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select Id,CategoryName from Category where Id = {id} ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                catg = new CategoryDataModel();
                                catg.Id = reader.GetInt32(0);
                                catg.CategoryName = reader.GetString(1);
                            }
                        }
                    }
                }

                return catg;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Update
        public bool Update(CategoryDataModel updCatg)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Category SET CategoryName = '{updCatg.CategoryName}' " +
                        $"where Id = {updCatg.Id}";

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


        //Search by Name

        public List<CategoryDataModel> GetCatgByName(string name)
        {
            try
            {
                List<CategoryDataModel> catgs = new List<CategoryDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id,CategoryName from Category where CategoryName like '%{name}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                CategoryDataModel catg = new CategoryDataModel();
                                catg.Id = reader.GetInt32(0);
                                catg.CategoryName = reader.GetString(1);
                                catgs.Add(catg);
                            }
                        }
                    }
                }

                return catgs;
            }

            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

    }
}
