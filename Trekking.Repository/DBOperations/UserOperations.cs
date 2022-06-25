using System;
using System.Data.SqlClient;
using Trekking.Repository.Models.DB;

namespace Trekking.Repository.DBOperations
{
    public class UserOperations
    {
        public static UserModel Login(UserModel userData, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    SqlCommand userCommand = new SqlCommand();
                    userCommand.Connection = connection;
                    userCommand.CommandText = "SELECT * FROM users WHERE name=@Name and password=@Password";
                    userCommand.Parameters.AddWithValue("Name", userData.Name);
                    userCommand.Parameters.AddWithValue("Password", userData.Password);

                    SqlDataReader reader = userCommand.ExecuteReader();
                    var readerCanRead = reader.Read();
                    if (!readerCanRead)
                    {
                        return null;
                    }
                    
                    connection.Close();
                    return userData;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public static bool? Register(UserModel userData, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    SqlCommand userCommand = new SqlCommand();
                    userCommand.Connection = connection;
                    userCommand.CommandText = "INSERT INTO users(name, password) values(@Name, @Password)";
                    userCommand.Parameters.AddWithValue("Name", userData.Name);
                    userCommand.Parameters.AddWithValue("Password", userData.Password);

                    SqlDataReader reader = userCommand.ExecuteReader();
                    int rowsAffected = userCommand.ExecuteNonQuery();

                    connection.Close();

                    return rowsAffected == 1;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        
    }
}