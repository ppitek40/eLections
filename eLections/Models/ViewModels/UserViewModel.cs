using System.Collections.Generic;
using System.Security.AccessControl;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eLections.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public Land Land { get; set; }
        public List<ApplicationRole> RolesList { get; set; }

    }
}