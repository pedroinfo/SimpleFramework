using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
        public int ExecuteProcedure(string query, SqlParameter [] parameters = null)
        {
            using (var connection = new SqlConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    if(parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteQuery(string query, SqlParameter [] parameters = null)
        {
            using (var connection = new SqlConnection())
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

        public DataTable GetDataTable(string query, SqlParameter [] parameters )
        {
            return null;
        }

        public List<T> GetList<T>(string query, SqlParameter [] parameters = null)
        {
            var list = new List<T>();

            var isBuiltInType = true;

            using(var connection = new SqlConnection())
            {
                connection.Open();

                using(var command = connection.CreateCommand())
                {
                    command.CommandText = query;


                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (isBuiltInType)
                            {
                                list.Add(GetFieldValue<T>(reader, 0));
                            }
                            else
                            {
                                var item = Activator.CreateInstance<T>();

                                Bind(item);

                                list.Add(item);
                            }
                        }
                    }

                }
            }

            return null;
        }

        private void Bind<T>(T item)
        {
            throw new NotImplementedException();
        }

        private T GetFieldValue<T>(SqlDataReader reader, int v)
        {
            throw new NotImplementedException();
        }

        private static bool IsBulitinType(Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }


    }
}
