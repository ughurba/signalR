using Microsoft.AspNetCore.Identity;

namespace SignalRPractice.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        public string ConnectId { get; set; }
    }
}
