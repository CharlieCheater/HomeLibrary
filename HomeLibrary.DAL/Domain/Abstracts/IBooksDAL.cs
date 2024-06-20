using HomeLibrary.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Infrastructure.Domain.Interfaces
{
    public interface IBooksDAL
    {
        Task<IEnumerable<Book>> GetBooksAsync(string title, int page = 1, int pageSize = 10);
        Task<IEnumerable<Book>> GetDetailedBooksAsync(string search, int page = 1, int pageSize = 10);
        Task<bool> InsertBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
        Task<Book> GetBookByIdAsync(int id);
    }
}
