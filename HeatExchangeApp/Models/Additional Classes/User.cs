using System.ComponentModel.DataAnnotations.Schema;

namespace HeatExchangeApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string PasswordCheck { get; set; }
    }
}
