using Data.Interface;

namespace Data.Dto
{
    public class ManagerDto : IUser
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ManagerId { get; set; }
    }
}
