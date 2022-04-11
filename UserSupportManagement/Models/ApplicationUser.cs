using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSupportManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string EmployeeName { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeDepartment { get; set; }

        public string EmployeeDesignation { get; set; }

        public DateTime EmployeeDOB { get; set; }

        public int ConcernId { get; set; }
        [ForeignKey("ConcernId")]
        public Concern Concern { get; set; }

        public bool IsActive { get; set; }
    }
}