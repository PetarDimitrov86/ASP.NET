namespace Whisper.Data.Data
{
    using Models;
    using System.Data.Entity;

    public class WhisperContext : DbContext
    {
        public WhisperContext()
            : base("name=WhisperContext")
        {
        }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Tweet> Tweets { get; set; }

        public virtual DbSet<PrivateMessage> PrivateMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.PostedTweets)
                .WithRequired(t => t.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.FavouritedTweets)
                .WithMany(t => t.FavouritedUsers)
                .Map(ut => ut
                    .MapLeftKey("FavouritedUserId")
                    .MapRightKey("FavouritedTweetId")
                    .ToTable("FavouritedUsersTweets"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithMany()
                .Map(uf =>
                uf.MapLeftKey("UserId")
                    .MapRightKey("FollowerId")
                    .ToTable("UsersFollowers"));

            modelBuilder.Entity<User>()
               .HasMany(u => u.Following)
               .WithMany()
               .Map(uf =>
               uf.MapLeftKey("UserId")
                   .MapRightKey("FollowingId")
                   .ToTable("UsersFollowing"));

            modelBuilder.Entity<Tweet>()
                .HasMany(t => t.Replies)
                .WithMany()
                .Map(tr => tr
                    .MapLeftKey("TweetId")
                    .MapRightKey("ReplyId")
                    .ToTable("TweetsReplies"));
                
            base.OnModelCreating(modelBuilder);
        }
    }
}