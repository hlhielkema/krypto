using System;
using System.Configuration;
using System.Data.Linq;

namespace ProjectBluefox.Database.Util
{
    public sealed class MSSqlConnection : IDisposable
    {
        public static string DatabaseConnectionString { get; set; }

        public static MSSqlConnection GetConnection()
        {
            if (DatabaseConnectionString != null)
                return new MSSqlConnection(DatabaseConnectionString);
            else
                throw new InvalidOperationException("Connection string not set");
        }

        private DataContext _context;

        public MSSqlConnection(string connectionString)
        {
            _context = new DataContext(connectionString);
        }

        public Table<T> GetTable<T>() where T : class
        {
            return _context.GetTable<T>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SubmitChanges()
        {
            _context.SubmitChanges();
        }

        public bool DeferredLoadingEnabled
        {
            get
            {
                return _context.DeferredLoadingEnabled;
            }
            set
            {
                _context.DeferredLoadingEnabled = value;
            }
        }

        public DataLoadOptions LoadOptions
        {
            get
            {
                return _context.LoadOptions;
            }
            set
            {
                _context.LoadOptions = value;
            }
        }
    }
}