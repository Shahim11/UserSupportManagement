using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSupportManagement.Models
{
    public class UserAccountViewModel
    {
        //public UserAccountViewModel()
        //{
        //    Concerns = new List<int>();
        //}

        public string Id { get; set; }

        public string RoleId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeDepartment { get; set; }

        public string EmployeeDesignation { get; set; }

        [DataType(DataType.Date)]
        public DateTime EmployeeDOB { get; set; }

        //public IList<int> Concerns;

        public int ConcernId { get; set; }
        //[ForeignKey("ConcernId")]
        //public Concern Concern { get; set; }

        public bool IsActive { get; set; }
    }
}
