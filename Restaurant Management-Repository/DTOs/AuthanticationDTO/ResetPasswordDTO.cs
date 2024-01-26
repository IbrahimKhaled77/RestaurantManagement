

namespace RestaurantManagement.DTOs.AuthanticationDTO
{
    public class ResetPasswordDTO
    {
        public required string Email { get; set; }
        public required string NewPassword { get; set; }
    }
}
