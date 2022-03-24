using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSupportManagement.Models
{
    public class Order:CreatedUpdated
    {
        [Key]
        public int OrderId { get; set; }

        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

        public int StatusTypeId { get; set; }
        [ForeignKey("StatusTypeId")]
        public StatusType StatusType { get; set; }

        public int ProblemId { get; set; }
        [ForeignKey("ProblemId")]
        public Problem Problem { get; set; }

        public string OrderName { get; set; }

        public string OrderItemName { get; set; }

        public string OrderItemDetails { get; set; }
        
        public string OrderItemQuantity { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
