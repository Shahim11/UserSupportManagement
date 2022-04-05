using System.Collections.Generic;

namespace UserSupportManagement.Models
{
    public class UserRolesViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }
    }
}