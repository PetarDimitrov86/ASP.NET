namespace CarDealer.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Logs = new HashSet<Log>();
        }

        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{5,}$")]
        public string Username { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+@[a-z]+\.[a-z]+$")]
        public string Email { get; set; }

        public int? Age { get; set; }

        public bool Subscribed { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }
}