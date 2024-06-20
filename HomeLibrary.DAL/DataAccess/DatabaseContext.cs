using HomeLibrary.DAL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.DAL.DataAccess
{
    public class DatabaseContext : IDbContext, IDisposable
    {
        private bool _disposed;
        public DatabaseContext(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = new SqlConnection(ConnectionString);
            BooksDAL = new BooksDAL(this);
        }
        public IDbConnection Connection { get; }
        public IBooksDAL BooksDAL { get; private set; }

        public string ConnectionString { get; private set; }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
