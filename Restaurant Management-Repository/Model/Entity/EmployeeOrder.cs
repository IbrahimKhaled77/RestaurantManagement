
namespace RestaurantManagement_Repository.Model.Entity
{
    public class EmployeeOrder
    {
        public int EmployeeOrderId { get; set; }

        public virtual Employee? Employee { get; set; }

        public virtual Order? Order { get; set; }

    }
}
