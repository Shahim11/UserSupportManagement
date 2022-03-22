using System.ComponentModel.DataAnnotations;

namespace UserSupportManagement.Models
{
    public class Concern:CreatedUpdated
    {
        [Key]
        public int ConcernId { get; set; }

        public string ConcernName { get; set; }

        public string ConcernShortName { get; set; }

        public bool IsActive { get; set; }
    }
}
