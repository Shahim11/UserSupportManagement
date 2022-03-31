﻿using System.Collections.Generic;

namespace UserSupportManagement.Models
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string EmployeeName { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }

    public class UserRolesViewModel
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
