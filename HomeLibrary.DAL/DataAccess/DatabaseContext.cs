using HomeLibrary.Infrastructure.DataAccess.Interfaces;
using HomeLibrary.Infrastructure.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Infrastructure.DataAccess
{
    public class DatabaseContext : IDbContext
    {
        public DatabaseContext(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = new SqlConnection(ConnectionString);
            BooksDAL = new BooksDAL(this);
        }
        public IDbConnection Connection { get; }
        public IBooksDAL BooksDAL { get; private set; }

        public string ConnectionString { get; private set; }

    }
}
