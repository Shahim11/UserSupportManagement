using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSupportManagement.Models
{
    public class Problem : CreatedUpdated
    {
        [Key]
        public int ProblemId { get; set; }

        public int ProblemTypeId { get; set; }
        [ForeignKey("ProblemTypeId")]
        public ProblemType ProblemType { get; set; }

        public int StatusTypeId { get; set; }
        [ForeignKey("StatusTypeId")]
        public StatusType StatusType { get; set; }

        public string ProblemName { get; set; }

        public string ProblemDetails { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}