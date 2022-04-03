using System.ComponentModel.DataAnnotations;

namespace UserSupportManagement.Models
{
    public class StatusType:CreatedUpdated
    {
        [Key]
        public int StatusTypeId { get; set; }

        public string StatusTypeName { get; set; }

        public int StatusTypeValue { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
