using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.DAL.Domain.Interfaces
{
    public interface IDbContext
    {
        IBooksDAL BooksDAL { get; }
        string ConnectionString { get; }
        IDbConnection Connection { get; }

    }
}
