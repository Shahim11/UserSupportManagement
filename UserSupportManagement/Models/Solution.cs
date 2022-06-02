using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSupportManagement.Models
{
    public class Solution : CreatedUpdated
    {
        [Key]
        public int SolutionId { get; set; }

        public int ProblemId { get; set; }
        [ForeignKey("ProblemId")]
        public Problem Problem { get; set; }

        public int StatusTypeId { get; set; }
        [ForeignKey("StatusTypeId")]
        public StatusType StatusType { get; set; }

        public string SolutionDetails { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool orderNeeded { get; set; }

        public string OrderItemName { get; set; }

        public string OrderItemDetails { get; set; }

        public string OrderItemQuantity { get; set; }
    }
}