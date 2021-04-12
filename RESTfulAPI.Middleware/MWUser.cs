using System.Collections.Generic;
using RESTfulAPI.Model.Models;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Middleware
{
    public class MWUser
    {
        private readonly IUserInterface _user;

        public MWUser(IUserInterface user)
        {
            _user = user;
        }

        public List<ViewModel.User> GetUsers()
        {
            List<ViewModel.User> users = new List<ViewModel.User>();
            foreach (var user in _user.View<User>())
            {
                ViewModel.User viewUser = new ViewModel.User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Birthday = user.Birthday?.ToString("yyy-MM-dd"),
                    Email = user.Email,
                    Phone = user.Phone,
                };
                users.Add(viewUser);
            }
            return users;
        }

        public ViewModel.User GetUser(int id)
        {
            User user = _user.View<User>(id);
            ViewModel.User viewUser = new ViewModel.User
            {
                Id = user.Id,
                UserName = user.UserName,
                Birthday = user.Birthday?.ToString("yyy-MM-dd"),
                Email = user.Email,
                Phone = user.Phone,
            };
            return viewUser;
        }
    }
}