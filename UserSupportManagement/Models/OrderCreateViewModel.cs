namespace UserSupportManagement.Models
{
    public class OrderCreateViewModel
    {
        public int OrderId { get; set; }

        public int VendorId { get; set; }
        
        public Vendor Vendor { get; set; }

        public int StatusTypeId { get; set; }
       
        public StatusType StatusType { get; set; }

        public int ProblemId { get; set; }
        
        public Problem Problem { get; set; }

        public string OrderName { get; set; }

        public string OrderItemName { get; set; }

        public string OrderItemDetails { get; set; }

        public string OrderItemQuantity { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
