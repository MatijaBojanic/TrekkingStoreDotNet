using System;
using Trekking.Repository.DBOperations;
using Trekking.Repository.Models.DB;

namespace Trekking.Services
{
    public class UserService
    {
        public static UserModel Login(UserModel userData)
        {
            string connectionString = System.Configuration.
                ConfigurationManager.
                ConnectionStrings["TrekkingDB"].
                ConnectionString;
            return UserOperations.Login(userData, connectionString);
        }
        
        public static bool? Register(UserModel userData)
        {
            string connectionString = System.Configuration.
                ConfigurationManager.
                ConnectionStrings["TrekkingDB"].
                ConnectionString;
            return UserOperations.Register(userData, connectionString);
        }
    }
}