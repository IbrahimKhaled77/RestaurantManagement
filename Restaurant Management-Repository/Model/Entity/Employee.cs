
namespace RestaurantManagement.Model.Entity
{
    public class Employee

    {
        public int EmployeeId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }
        public required string Position { get; set; }

        public required bool IsLoggedIn { get; set; }
        public required string AccessKey { get; set; }
        public DateTime? AccesskeyExpireDate { get; set; }
        public required bool IsActive { get; set; }

       

        public virtual List<EmployeeOrder>? EmployeeOrder { get; set; }

      
    }
}
