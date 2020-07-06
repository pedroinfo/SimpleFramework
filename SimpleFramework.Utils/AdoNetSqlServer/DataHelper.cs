using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SimpleFramework.Utils.AdoNetSqlServer
{

    /// <summary>
    /// sometimes we need to use pure ADO. 
    /// I've worked on projects where it was not allowed to use Dapper, Entity framework or any other ORM. 
    /// This class is to help at this point, a set of methods to interact via ADO.NET with high level through Lists, 
    /// Procedures, Datatable, etc. In progress...
    /// </summary>
    public class DataHelper
    {
        private static string _connectionString = "";

        public static void InitializeConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataHelper()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new Exception("ConnectionString cannot null");
        }

        public int ExecuteProcedure(string query, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        public async Task<int> ExecuteProcedureAsync(string query, SqlParameter[] parameters = null)
        {
            using(var connection = new SqlConnection())
            {
                await connection.OpenAsync();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public int Execute(string query, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        public async Task<int> ExecuteAsync(string query, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        
        public DataTable GetDataTableFromProcedure(string query, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                var dataTable = new DataTable();
                command.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataTable GetDataTableFrom(string query, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                var dataTable = new DataTable();
                command.CommandType = CommandType.Text;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public List<T> GetList<T>(string query, SqlParameter[] parameters = null)
        {
            return null;
        }

        //public T GetSingle<T>(string query, SqlParameter[] parameters = null)
        //{
        //    T t;
        //    return t;
        //}


    }
}
