
namespace RestaurantManagement_Repository.Model.Entity
{
    public class Employee

    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }

       // public virtual ICollection<Table> Table { get; set; }

        public virtual List<EmployeeOrder> EmployeeOrder { get; set; }

      
    }
}
