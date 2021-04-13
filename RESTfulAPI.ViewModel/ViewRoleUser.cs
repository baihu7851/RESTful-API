using System.Collections.Generic;

namespace RESTfulAPI.ViewModel

{
    public class ViewRoleUser
    {
        public int RoleId { get; set; }
        public List<int> UserId { get; set; }
    }
}