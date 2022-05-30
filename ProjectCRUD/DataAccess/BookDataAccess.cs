using ProjectCRUD.Models;
using System.Data.SqlClient;

namespace ProjectCRUD.DataAccess
{
    public class BookDataAccess
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public BookDataAccess()
        {
            SuccessMessage = "";
            ErrorMessage = "";
        }

        //GetAll
        public List<BookDataModel> GetAll()
        {
            try
            {
                //ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<BookDataModel> Books = new List<BookDataModel>();

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select B.Id,B.BookName,B.AuthorName,B.PublishedYear,B.Price,B.Status,C.CategoryName from Book As B " +
                         $"INNER JOIN [dbo].Category AS C ON B.CategoryId = C.Id " ;
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                BookDataModel book = new BookDataModel();
                                book.Id = reader.GetInt32(0);
                                book.BookName = reader.GetString(1);
                                book.AuthorName= reader.GetString(2);
                                book.PublishedYear = reader.GetDateTime(3);
                                book.Price = reader.GetDecimal(4);
                                book.Status=reader.GetString(5);
                                book.CategoryName=reader.GetString(6);
                                Books.Add(book);
                            }
                        }
                    }
                }

                return Books;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Insert
        public BookDataModel Insert(BookDataModel newBook)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Book (BookName,AuthorName,PublishedYear,Price,Status,CategoryId) VALUES ('{newBook.BookName}', '{newBook.AuthorName}',' {newBook.PublishedYear.ToString("yyyy-MM-dd")}',{newBook.Price},'{newBook.Status}',{newBook.CategoryId}); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newBook.Id = idInserted;
                            return newBook;
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
        public BookDataModel GetBookbyId(int id)
        {
            try
            {
                //ErrorMessage = string.Empty;
                ErrorMessage = "";

                BookDataModel book=null;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select B.Id,B.BookName,B.AuthorName,B.PublishedYear,B.Price,B.Status,C.CategoryName from Book As B " +
                         $"INNER JOIN [dbo].Category AS C ON B.CategoryId = C.Id where B.Id = {id} ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                book = new BookDataModel();
                                book.Id = reader.GetInt32(0);
                                book.BookName = reader.GetString(1);
                                book.AuthorName = reader.GetString(2);
                                book.PublishedYear = reader.GetDateTime(3);
                                book.Price = reader.GetDecimal(4);
                                book.Status = reader.GetString(5);
                                book.CategoryName = reader.GetString(6);
                            }
                        }
                    }
                }

                return book;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Update
        public bool Update(BookDataModel updBook)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Book SET BookName = '{updBook.BookName}', " +
                        $"AuthorName = '{updBook.AuthorName}', " +
                        $"PublishedYear = '{updBook.PublishedYear.ToString("yyyy-MM-dd")}', " +
                        $"Price = {updBook.Price}, " +
                        $"Status = '{updBook.Status}' ," +
                        $"CategoryId= {updBook.CategoryId} " +
                        $"where Id = {updBook.Id}";

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

        public List<BookDataModel> GetBookByName(string name)
        {
            try
            {
                List<BookDataModel> books = new List<BookDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id,BookName,AuthorName,PublishedYear,Price,Status,CategoryId from Book where BookName like '%{name}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                BookDataModel book = new BookDataModel();
                                book.Id = reader.GetInt32(0);
                                book.BookName = reader.GetString(1);
                                book.AuthorName = reader.GetString(2);
                                book.PublishedYear = reader.GetDateTime(3);
                                book.Price = reader.GetDecimal(4);
                                book.Status = reader.GetString(5);
                                book.CategoryId = reader.GetInt32(6);
                                books.Add(book);
                            }
                        }
                    }
                }

                return books;
            }

            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

    }

}
