


namespace RestaurantManagement_Repository.Model.Entity
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Order> OrderCustomer { get; set; }
    }
}
