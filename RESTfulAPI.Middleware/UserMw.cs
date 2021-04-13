using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.Repository.Interfaces;
using System.Collections.Generic;
using RESTfulAPI.Model.Models;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.Middleware
{
    public class UserMw : IUser
    {
        private readonly IUserInterface _user;

        public UserMw(IUserInterface user)
        {
            _user = user;
        }

        public void AddUser(List<ViewUser> users)
        {
            foreach (var user in users)
            {
                _user.Add(user);
            }
        }

        public void DeleteUser(List<int> id)
        {
            foreach (var i in id)
            {
                _user.Delete(i);
            }
        }

        public ViewUser GetUser(int id)
        {
            User userData = _user.View<User>(id);
            var user = new ViewUser()
            {
                Id = userData.Id,
                UserName = userData.UserName,
                Birthday = userData.Birthday?.ToString("yyyy-MM-dd"),
                Email = userData.Email,
                Phone = userData.Phone,
            };
            return user;
        }

        public List<ViewUser> GetUsers()
        {
            List<ViewUser> users = new();
            foreach (User user in _user.View<User>())
            {
                ViewUser viewUser = new ViewUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Birthday = user.Birthday?.ToString("yyyy-MM-dd"),
                    Email = user.Email,
                    Phone = user.Phone,
                };
                users.Add(viewUser);
            }
            return users;
        }

        public void UpdateUser(List<ViewUser> users)
        {
            foreach (var user in users)
            {
                _user.Update(user);
            }
        }
    }
}