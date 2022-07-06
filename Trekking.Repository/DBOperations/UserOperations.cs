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
                    userCommand.CommandText = "SELECT * FROM users WHERE email=@Email and password=@Password";
                    userCommand.Parameters.AddWithValue("Email", userData.Email);
                    userCommand.Parameters.AddWithValue("Password", userData.Password);

                    SqlDataReader reader = userCommand.ExecuteReader();
                    var readerCanRead = reader.Read();
                    if (!readerCanRead)
                    {
                        return null;
                    }

                    userData.UserId = reader.GetInt32(0);
                    userData.Role = reader.GetString(8);
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
                    userCommand.CommandText = "INSERT INTO users(email, password) values(@Email, @Password)";
                    userCommand.Parameters.AddWithValue("Email", userData.Email);
                    userCommand.Parameters.AddWithValue("Password", userData.Password);
                    
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