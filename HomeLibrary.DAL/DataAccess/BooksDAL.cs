using HomeLibrary.Infrastructure.DataAccess.Interfaces;
using HomeLibrary.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Infrastructure.DataAccess
{
    /// <summary>
    /// Класс для работы с таблицей Books в базе данных
    /// </summary>
    public class BooksDAL : IBooksDAL
    {
        public BooksDAL(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public DatabaseContext DbContext { get; private set; }
        // charliecheater: Добавить обработку исключений (20-06-2024 10:03)
        public async Task<bool> DeleteBookAsync(int id)
        {
            using (SqlCommand cmd = new SqlCommand())
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
        // charliecheater: Добавить обработку исключений (20-06-2024 10:03)
        public async Task<Book> GetBookByIdAsync(int id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                DbContext.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetBookById";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                var reader = await cmd.ExecuteReaderAsync();

                if (!reader.HasRows)
                    return null;

                var canRead = await reader.ReadAsync();

                if (!canRead)
                    return null;

                Book book = FullReadBook(reader);

                DbContext.Connection.Close();
                return book;
            }
        }
        /// <summary>
        /// Возвращает объекты книг без оглавлений
        /// </summary>
        /// <param name="title">Поиск по наименованию</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество элементов</param>
        /// <returns></returns>
        // charliecheater: Необходимо реализовать мeтод GetBooks (20-06-2024 9:36)
        public Task<IEnumerable<Book>> GetBooksAsync(string title, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Возвращает объекты книг с оглавлениями
        /// </summary>
        /// <param name="search">Поиск по наименованию</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество элементов</param>
        /// <returns></returns>
        // charliecheater: Добавить обработку исключений (20-06-2024 10:03)
        public async Task<IEnumerable<Book>> GetDetailedBooksAsync(string search, int page = 1, int pageSize = 10)
        {
            List<Book> books = new List<Book>();
            using (SqlCommand cmd = new SqlCommand())
            {
                DbContext.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetDetailedBooks";
                cmd.Parameters.Add("@search", SqlDbType.VarChar).Value = search;
                cmd.Parameters.Add("@page", SqlDbType.Int).Value = page;
                cmd.Parameters.Add("@pageSize", SqlDbType.Int).Value = pageSize;
                var reader = await cmd.ExecuteReaderAsync();

                if (!reader.HasRows)
                    return null;
                var canRead = await reader.ReadAsync();
                while (!canRead)
                {
                    books.Add(FullReadBook(reader));
                }
                DbContext.Connection.Close();
            }
            return books;
        }
        public async Task<bool> InsertBookAsync(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            using (SqlCommand cmd = new SqlCommand())
            {
                DbContext.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertBook";
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = book.Title;
                cmd.Parameters.Add("@author", SqlDbType.VarChar).Value = book.Author;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = book.Description;
                cmd.Parameters.Add("@publisher", SqlDbType.VarChar).Value = book.Publisher;
                cmd.Parameters.Add("@tableContents", SqlDbType.Xml).Value = book.TableContents;
                cmd.Parameters.Add("@publicationYear", SqlDbType.Xml).Value = book.PublicationYear;

                var result = false;
                try
                {
                    var id = await cmd.ExecuteScalarAsync();
                    book.Id = int.Parse(result.ToString());
                    result = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[Critical]" + ex.Message);
                }
                finally
                {
                    DbContext.Connection.Close();
                }
                return true;
            }
        }

        public Task<bool> UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }
        private Book ReadBook(SqlDataReader reader)
        {
            Book book = new Book()
            {
                Id = int.Parse(reader[nameof(Book.Id)].ToString()),
                Author = reader[nameof(Book.Author)].ToString(),
                Description = reader[nameof(Book.Description)].ToString(),
                Title = reader[nameof(Book.Title)].ToString(),
                PublicationYear = int.Parse(reader[nameof(Book.PublicationYear)]?.ToString()),
                Publisher = reader[nameof(Book.Publisher)]?.ToString(),
                CreatedAt = DateTime.Parse(reader[nameof(Book.CreatedAt)].ToString()),
            };
            var updatedAt = reader[nameof(Book.CreatedAt)];
            if (updatedAt != null)
                book.UpdatedAt = DateTime.Parse(reader[nameof(Book.UpdatedAt)].ToString());
            return book;
        }
        private Book FullReadBook(SqlDataReader reader)
        {
            var book = ReadBook(reader);
            book.TableContents = reader[nameof(Book.TableContents)].ToString();
            return book;
        }
    }
}
