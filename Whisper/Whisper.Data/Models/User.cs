namespace Whisper.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public User()
        {
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
            this.PostedTweets = new HashSet<Tweet>();
            this.FavouritedTweets = new HashSet<Tweet>();
            this.ReceivedMessages = new HashSet<PrivateMessage>();
            this.SentMessages = new HashSet<PrivateMessage>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<User> Following { get; set; }

        public virtual ICollection<Tweet> PostedTweets { get; set; }

        public virtual ICollection<Tweet> FavouritedTweets { get; set; }

        [InverseProperty("Recipient")]
        public virtual ICollection<PrivateMessage> ReceivedMessages { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<PrivateMessage> SentMessages { get; set; }
    }
}