using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static online_ordering_api.Data.DBColumnExtension;

namespace online_ordering_api.Data
{
    public class DatabaseManager : IDataManager
    {
        public static int InstanceCount = 0;
        private IConfiguration _configuration;
        private string connetionString { get; set; }

        public DatabaseManager(IConfiguration configuration)
        {
            InstanceCount++;
            _configuration = configuration;

            //read Database Connection from appsettings.js file.
            string DbConnection = (_configuration.GetSection("ConnectionStrings") == null || string.IsNullOrEmpty(connetionString)) ? _configuration.GetSection("ConnectionStrings")["ConfigDB"] : connetionString;
            connetionString = DbConnection;
            Console.WriteLine($"DatabaseManager Instance count : {InstanceCount}");
            Console.WriteLine($"DatabaseManager connetionString : {connetionString}");


        }


        public List<T> ExecuteReaderDataSet<T>(string queryString) where T : new()
        {
            T ObjClass = new T();
            List<T> data = new List<T>();

            try
            {
                //create connections DB
                using (SqlConnection connection = new SqlConnection(connetionString))
                {

                    using (SqlCommand cmd = new SqlCommand(queryString, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (reader.Read())
                        {
                            T NewOBJClass = new T();
                            var TargetClass = ObjClass.GetType();
                            var ObjectTargetClass = ObjClass.GetType().GetProperties();

                            foreach (var prop in ObjectTargetClass)
                            {
                                //var NewOBJClass = ObjClassforNew.GetType();
                                var attributeData = prop.CustomAttributes.FirstOrDefault();
                                if (attributeData == null)
                                {
                                    //skip none mapping column property
                                    continue;
                                }
                                DbColumn Columnttribute = (DbColumn)Attribute.GetCustomAttribute(prop, typeof(DbColumn));
                                var ColumName = Columnttribute.Name;
                                var nameprop = prop.Name;
                                var prop_name = TargetClass.GetProperty(nameprop);
                                var prop_type = prop_name.PropertyType;
                                var datafromDB = reader[ColumName];
                                if (datafromDB.GetType() != typeof(DBNull))
                                {
                                    prop_type = Nullable.GetUnderlyingType(prop_type) ?? prop_type;
                                    var dataforReturn = Convert.ChangeType(datafromDB, prop_type.IsEnum ? typeof(int) : prop_type);
                                    var IsTypeString = dataforReturn.GetType().FullName.ToUpper().Equals("SYSTEM.STRING");
                                    var IsDBNull = prop_type == typeof(DBNull);
                                    prop.SetValue(NewOBJClass, dataforReturn);
                                }


                            }

                            //set return data
                            data.Add(NewOBJClass);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ExecuteReaderDataSet Error: {ex.Message}");
                throw new Exception("ExecuteReaderDataSet Error: " + ex.Message, ex.InnerException);

            }
            return data;
        }

        public T ExecuteReaderData<T>(string queryString) where T : class, new()
        {
            T data = null;

            try
            {
                //create connections DB
                using (SqlConnection connection = new SqlConnection(connetionString))
                {

                    using (SqlCommand cmd = new SqlCommand(queryString, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (!reader.HasRows)
                        {
                            // No data returned from database
                            return null;
                        }

                        while (reader.Read())
                        {
                            T NewOBJClass = new T();
                            var TargetClass = NewOBJClass.GetType();
                            var ObjectTargetClass = NewOBJClass.GetType().GetProperties();

                            foreach (var prop in ObjectTargetClass)
                            {
                                //var NewOBJClass = ObjClassforNew.GetType();
                                var attributeData = prop.CustomAttributes.FirstOrDefault();
                                if (attributeData == null)
                                {
                                    //skip none mapping column property
                                    continue;
                                }
                                DbColumn Columnttribute = (DbColumn)Attribute.GetCustomAttribute(prop, typeof(DbColumn));
                                var ColumName = Columnttribute.Name;
                                var nameprop = prop.Name;
                                var prop_name = TargetClass.GetProperty(nameprop);
                                var prop_type = prop_name.PropertyType;
                                var datafromDB = reader[ColumName];
                                if (datafromDB.GetType() != typeof(DBNull))
                                {
                                    prop_type = Nullable.GetUnderlyingType(prop_type) ?? prop_type;
                                    var dataforReturn = Convert.ChangeType(datafromDB, prop_type.IsEnum ? typeof(int) : prop_type);
                                    var IsTypeString = dataforReturn.GetType().FullName.ToUpper().Equals("SYSTEM.STRING");
                                    var IsDBNull = prop_type == typeof(DBNull);
                                    prop.SetValue(NewOBJClass, dataforReturn);
                                }

                            }

                            //set return data
                            data = NewOBJClass;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"ExecuteReaderData Error: {ex.Message}");
                throw new Exception("ExecuteReaderData Error: " + ex.Message, ex.InnerException);
            }

            return data;
        }
        public void ExecuteNoneQuery(string queryString)
        {
            try
            {
                //create connections DB
                using (SqlConnection connection = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ExecuteNoneQuery Error : {ex.Message}");
                throw new Exception("ExecuteNonQuery Error: " + ex.Message, ex.InnerException);
            }
        }


        public void WithConectionString(string _connectionString)
        {
            connetionString = _connectionString;
            Console.WriteLine($"DB connect with : {connetionString}");
        }
        public string GetConString()
        {
            return this.connetionString;
        }


    }

}
