using HomeLibrary.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.DAL.Domain.Interfaces
{
    public interface IBooksDAL
    {
        Task<IEnumerable<Book>> GetBooksAsync(string title);
        Task<IEnumerable<Book>> GetDetailedBooksAsync(string search);
        Task<bool> InsertBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
        Task<Book> GetBookByIdAsync(int id);
    }
}
