using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class UserInfo
    {
        public uint UserId { get; set; }
        public string? Address { get; set; }
        public string Name { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string? AvatarIcon { get; set; } 
    }
}
