namespace UserSupportManagement.Models
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string EmployeeName { get; set; }
        //public IList<UserRolesViewModel> UserRoles { get; set; }
        public bool Selected { get; set; }
    }
}
