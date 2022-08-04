using System.ComponentModel.DataAnnotations;

namespace SignalRPractice.Models
{
    public class LoginVM
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
