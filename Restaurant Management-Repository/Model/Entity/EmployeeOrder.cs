﻿
namespace RestaurantManagement.Model.Entity
{
    public class EmployeeOrder
    {
        public int EmployeeOrderId { get; set; }

        public int EmployeeId { get; set; }

        public int OrderId { get; set; }


        public virtual Employee? Employee { get; set; }

        public virtual Order? Order { get; set; }

    }
}
