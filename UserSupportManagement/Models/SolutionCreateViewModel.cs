using System;

namespace UserSupportManagement.Models
{
    public class SolutionCreateViewModel
    {
        public int SolutionId { get; set; }

        public int ProblemId { get; set; }

        public int ProblemName { get; set; }

        public int StatusTypeId { get; set; }
        
        public string SolutionDetails { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public bool orderNeeded { get; set; }
    }
}
