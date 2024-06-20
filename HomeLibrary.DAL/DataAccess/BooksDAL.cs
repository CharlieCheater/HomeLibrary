using HomeLibrary.Infrastructure.DataAccess.Interfaces;
using HomeLibrary.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Infrastructure.DataAccess
{
    public class BooksDAL : IBooksDAL
    {
        public BooksDAL(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public DatabaseContext DbContext {get; private set;}

        public async Task<bool> DeleteBookAsync(int id)
        {
            using(SqlCommand cmd = new SqlCommand())
            {
                DbContext.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteBook";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                var result = await cmd.ExecuteNonQueryAsync();

                DbContext.Connection.Close();
                return result == 1;
            }
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                DbContext.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetBookById";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                var result = await cmd.ExecuteReaderAsync();

                if(!result.HasRows)
                    return null;

                var canRead = await result.ReadAsync();

                if(!canRead) 
                    return null;

                Book book = new Book()
                {
                    Id = int.Parse(result[nameof(Book.Id)].ToString()),
                    Author = result[nameof(Book.Author)].ToString(),
                    Description = result[nameof(Book.Description)].ToString(),
                    TableContents = result[nameof(Book.TableContents)].ToString(),
                    Title = result[nameof(Book.Title)].ToString(),
                    PublicationYear = int.Parse(result[nameof(Book.PublicationYear)]?.ToString()),
                    Publisher = result[nameof(Book.Publisher)]?.ToString(),
                    CreatedAt = DateTime.Parse(result[nameof(Book.CreatedAt)].ToString()),
                };
                var updatedAt = result[nameof(Book.CreatedAt)];
                if (updatedAt != null)
                    book.UpdatedAt = DateTime.Parse(result[nameof(Book.UpdatedAt)].ToString());
                DbContext.Connection.Close();
                return book;
            }
        }

        public Task<IEnumerable<Book>> GetBooksAsync(string title, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
