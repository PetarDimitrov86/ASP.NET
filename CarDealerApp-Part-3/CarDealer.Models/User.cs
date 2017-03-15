namespace CarDealer.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{5,}$")]
        public string Username { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+@[a-z]+\.[a-z]+$")]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}