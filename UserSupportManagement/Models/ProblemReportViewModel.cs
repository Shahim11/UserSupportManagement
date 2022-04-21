using System;

namespace UserSupportManagement.Models
{
    public class ProblemReportViewModel
    {
        public int ProblemId { get; set; }

        public string ProblemName { get; set; }

        public string ProblemDetails { get; set; }

        public int ProblemTypeId { get; set; }

        public string ProblemTypeName { get; set; }

        public string StatusTypeName { get; set; }

        public int StatusTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        //public Problem Problem { get; set; }

        public int SolutionId { get; set; }

        public string SolutionDetails { get; set; }

        //public Solution Solution { get; set; }

        public string EmployeeCode { get; set; }
    }
}
