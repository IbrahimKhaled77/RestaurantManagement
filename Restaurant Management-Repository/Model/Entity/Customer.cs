


namespace RestaurantManagement_Repository.Model.Entity
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public string AccessKey { get; set; }
        public DateTime? AccesskeyExpireDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsLoggedIn { get; set; }

        public virtual ICollection<Order> OrderCustomer { get; set; }

        //
       // public virtual Table Table { get; set; }


    }
}
