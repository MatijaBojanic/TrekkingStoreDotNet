﻿namespace Trekking.Repository.Models.DB
{
    public class UserModel
    {
        public int UserId { get; set; }
        
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Address { get; set; }
        
        public string City { get; set; }
        
        public string Country { get; set; }
        
        public string Role { get; set; }
    }
}