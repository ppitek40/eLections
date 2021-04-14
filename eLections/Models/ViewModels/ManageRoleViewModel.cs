using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using eLections.Dtos;

namespace eLections.Models.ViewModels
{
    public class ManageRoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsSelected { get; set; }
    }

    public class ManagedRole
    {
        
    }
}