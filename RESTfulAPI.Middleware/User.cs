using System.Collections.Generic;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Middleware
{
    public class User : IUser
    {
        private readonly IUserInterface _user;

        public User(IUserInterface user)
        {
            _user = user;
        }
        public ViewModel.User GetUser(int id)
        {
            Model.Models.User userData = _user.View<Model.Models.User>(id);
            ViewModel.User user = new ViewModel.User()
            {
                Id= userData.Id,
                UserName = userData.UserName,
                Birthday = userData.Birthday?.ToString("yyyy-MM-dd"),
                Email = userData.Email,
                Phone = userData.Phone,
            };

            return user;
        }

        public List<ViewModel.User> GetUsers()
        {
            List<ViewModel.User> users = new();
            foreach (Model.Models.User user in _user.View<Model.Models.User>())
            {
                ViewModel.User viewUser = new ViewModel.User
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


    }
}
