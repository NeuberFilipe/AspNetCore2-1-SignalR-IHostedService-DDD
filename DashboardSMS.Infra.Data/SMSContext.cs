using Dapper;
using DashboardSMS.Domain.Entities.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DashboardSMS.Infra.Data
{
    public class SMSContext : DbContext
    {
        private readonly string connectionString;
        public SMSContext(string conn)
        {
            connectionString = conn;
        }

        public SMSContext(DbContextOptions<SMSContext> options)
          : base(options)
        {
        }

        public DbSet<ConsultaOnlineSMS> ConsultaOnlineSMS { get; set; }
        public DbSet<ConsultarStatusEnvioSMS> ConsultarStatusEnvioSMS { get; set; }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return Connection.Query<T>(sql, param);
        }

        public T QueryScalar<T>(string sql, object param = null)
        {
            return Connection.ExecuteScalar<T>(sql, param);
        }

        public int Execute(string sql, object param = null)
        {
            return Connection.Execute(sql, param);
        }
    }
}
