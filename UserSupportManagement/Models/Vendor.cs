using System.ComponentModel.DataAnnotations;

namespace UserSupportManagement.Models
{
    public class Vendor:CreatedUpdated
    {
        [Key]
        public int VendorId { get; set; }

        public string VendorName { get; set; }

        public string VendorDetails { get; set; }

        public string VendorEmail { get; set; }

        public string VendorPhoneNumber { get; set; }

        public string VendorAddress { get; set; }

        public string ContactPersonName { get; set; }

        public string ContactPersonPhoneNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
