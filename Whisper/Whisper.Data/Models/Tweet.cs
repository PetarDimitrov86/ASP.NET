namespace Whisper.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        public Tweet()
        {
            this.FavouritedUsers = new HashSet<User>();
            this.Replies = new HashSet<Tweet>();
        }

        public int Id { get; set; }

        [StringLength(160)]
        public string Message { get; set; }

        public DateTime? PublishDate { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<User> FavouritedUsers { get; set; }

        public virtual ICollection<Tweet> Replies { get; set; }
    }
}