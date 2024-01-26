



namespace RestaurantManagement.Model.Entity
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }

        public required string AccessKey { get; set; }
        public DateTime? AccesskeyExpireDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsLoggedIn { get; set; }

        public  virtual ICollection<Order>? OrderCustomer { get; set; }

      


    }
}