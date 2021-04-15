using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public List<ViewUser> GetUsers()
        {
            const string key = "Users";
            if (Cache.GetCache(key) == null)
            {
                List<ViewUser> users = _user.View<User>().Select(
                    user => new ViewUser
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Birthday = user.Birthday?.ToString("yyyy-MM-dd"),
                        Email = user.Email,
                        Phone = user.Phone,
                    }).ToList();
                Cache.SetCache(key, users);
            }
            List<ViewUser> result = (List<ViewUser>)Cache.GetCache(key);
            return result;
        }

        public ViewUser GetUser(int id)
        {
            string key = $"User{id}";
            if (Cache.GetCache(key) == null)
            {
                ViewUser viewRole = new ViewUser();
                User user = _user.View<User>(id);
                if (user == null)
                {
                    return null;
                }

                viewRole.Id = user.Id;
                viewRole.UserName = user.UserName;
                viewRole.Birthday = user.Birthday?.ToString("yyyy-MM-dd");
                viewRole.Email = user.Email;
                viewRole.Phone = user.Phone;
                Cache.SetCache(key, viewRole);
            }
            else
            {
            }

            ViewUser result = (ViewUser)Cache.GetCache(key);
            return result;
        }

        public List<ViewUser> AddUser(List<ViewUser> users)
        {
            List<int> listId = new();
            foreach (var user in users)
            {
                if (VerifyUser(user) != null)
                {
                    var id = _user.Add(user);
                    listId.Add(id);
                    string key = $"User{user.Id}";
                    Cache.SetCache(key, user);
                }
            }
            List<ViewUser> result = listId.Select(GetUser).ToList();
            return result;
        }

        public List<ViewUser> UpdateUser(List<ViewUser> users)
        {
            List<int> listId = new();
            foreach (var user in users)
            {
                if (VerifyUser(user) != null)
                {
                    _user.Update(user);
                    listId.Add(user.Id);
                }
                string key = $"User{user.Id}";
                Cache.SetCache(key, user);
            }
            List<ViewUser> result = listId.Select(GetUser).ToList();
            return result;
        }

        public ViewUser DeleteUser(int id)
        {
            var user = GetUser(id);
            if (user == null)
            {
                return null;
            }
            _user.Delete(id);
            string key = $"Role{id}";
            Cache.RemoveCache(key);
            return user;
        }

        private object VerifyUser(ViewUser user)
        {
            return string.IsNullOrWhiteSpace(user.UserName) ? null : user;
        }
    }
}