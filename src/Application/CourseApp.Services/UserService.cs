using System;
using CourseApp.Entities;

namespace CourseApp.Services
{
    public class UserService : IUserService
    {
        private List<User> users;

        public UserService()
        {
            users = new List<User>()
            {
                new(){ Id=1, Name="Türkay", Password="123", Email="abc@xyz.com", Role="Admin", UserName="turko"},
                new(){ Id=1, Name="Süm", Password="123", Email="abc@xyz.com", Role="Editor", UserName="SümSüm"},
                new(){ Id=1, Name="Üms", Password="123", Email="abc@xyz.com", Role="Client", UserName="üb"},


            };
        }
        public User? ValidateUser(string username, string password)
        {
            return users.SingleOrDefault(u => u.UserName == username && u.Password == password);
        }
    }
}

