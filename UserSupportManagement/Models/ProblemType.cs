using System.ComponentModel.DataAnnotations;

namespace UserSupportManagement.Models
{
    public class ProblemType:CreatedUpdated
    {
        [Key]
        public int ProblemTypeId { get; set; }

        public string ProblemTypeName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
