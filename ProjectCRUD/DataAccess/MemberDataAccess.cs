using ProjectCRUD.Models;
using System.Data.SqlClient;

namespace ProjectCRUD.DataAccess
{
    public class MemberDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<MemberDataModel> GetAll()
        {
            try
            {
                ErrorMessage = string.Empty;
                List<MemberDataModel> members = new List<MemberDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "SELECT Id, FirstName,LastName,Gender,DOB,EmailId,MobileNumber,MemStatus,UserName,Password FROM Member";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                MemberDataModel member = new MemberDataModel();
                                member.Id = reader.GetInt32(0);
                                member.FirstName = reader.GetString(1);
                                member.LastName = reader.GetString(2);
                                member.Gender = reader.GetString(3);
                                member.DOB = reader.GetDateTime(4);
                                member.EmailId = reader.GetString(5);
                                member.MobileNumber = reader.GetString(6);
                                member.MemStatus = reader.GetString(7);
                                member.UserName = reader.GetString(8);
                                member.Password=reader.GetString(9);
                                members.Add(member);
                            }
                        }
                    }
                }
                return members;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //Insert new Member

        public MemberDataModel Insert(MemberDataModel newMember)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Member (FirstName,LastName,Gender,DOB,EmailId,MobileNumber,MemStatus,UserName,Password,MemStatus) VALUES ('{newMember.FirstName}','{newMember.LastName}', '{newMember.Gender}',' {newMember.DOB.ToString("yyyy-MM-dd")}','{newMember.EmailId}','{newMember.MobileNumber}','{newMember.MemStatus}','{newMember.UserName}','{newMember.Password}', '{newMember.MemStatus}'); SELECT SCOPE_IDENTITY();";


                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newMember.Id = idInserted;
                            return newMember;
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
        public bool Update(MemberDataModel updMember)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Member SET FirstName = '{updMember.FirstName}', " +
                        $"LastName='{updMember.LastName}'," +
                        $"Gender = '{updMember.Gender}', " +
                        $"DOB = '{updMember.DOB.ToString("yyyy-MM-dd")}', " +
                        $"EmailId = '{updMember.EmailId}', " +
                        $"MobileNumber = '{updMember.MobileNumber}', " +
                        $"UserName = '{updMember.UserName}'," +
                        $"Password = '{updMember.Password}'," +
                        $"MemStatus = '{updMember.MemStatus}' " +
                        $"where Id = {updMember.Id}";

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

        public bool UpdateStatus(string status, int memberId)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Member SET MemStatus = '{status}' " +
                        $"where Id = {memberId}";

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
        public MemberDataModel GetMemberById(int id)
        {
            try
            {
                ErrorMessage = string.Empty;
                MemberDataModel member = null;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt =$"SELECT Id, FirstName,LastName,Gender,DOB,EmailId,MobileNumber,MemStatus,UserName,Password FROM Member where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                member = new MemberDataModel();
                                member.Id = reader.GetInt32(0);
                                member.FirstName = reader.GetString(1);
                                member.LastName = reader.GetString(2);
                                member.Gender = reader.GetString(3);
                                member.DOB = reader.GetDateTime(4);
                                member.EmailId = reader.GetString(5);
                                member.MobileNumber = reader.GetString(6);
                                member.MemStatus = reader.GetString(7);
                                member.UserName = reader.GetString(8);
                                member.Password = reader.GetString(9);
                            }
                        }
                    }
                }
                return member;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //
        public List<MemberDataModel> GetUserNamePassword(string username, string password)
        {
            try
            {
                List<MemberDataModel> members = new List<MemberDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id, FirstName, LastName,Gender,DOB,EmailId,MobileNumber,UserName,Password  from Department where Name like '%{username}%' OR Location like '%{password}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                MemberDataModel member = new MemberDataModel();
                                member.Id = reader.GetInt32(0);
                                member.FirstName = reader.GetString(1);
                                member.LastName = reader.GetString(2);
                                member.Gender = reader.GetString(3);
                                member.DOB = reader.GetDateTime(4);
                                member.EmailId = reader.GetString(5);
                                member.MobileNumber = reader.GetString(6);
                                member.UserName = reader.GetString(7);
                                member.Password = reader.GetString(8);
                                members.Add(member);
                            }
                        }
                    }
                }

                return members;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }



    }
}
