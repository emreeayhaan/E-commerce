using Data.Interface;

namespace Data.Dto
{
    public class CustomerDto : IUser
    {
        public string? CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public double PhoneNumber { get; set; }
    }
}
