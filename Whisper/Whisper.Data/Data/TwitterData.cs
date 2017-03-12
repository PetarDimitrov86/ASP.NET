namespace Whisper.Data.Data
{
    using Contracts;
    using Models;
    using Repositories;

    public class TwitterData : ITwitterData
    {
        private readonly WhisperContext context;
        private IRepository<User> users;
        private IRepository<Tweet> tweets;
        private IRepository<PrivateMessage> privateMessages;

        public TwitterData()
        {
            this.context = new WhisperContext();
        }

        public IRepository<User> Users => this.users ?? (this.users = new Repository<User>(this.context.Users));

        public IRepository<Tweet> Tweets => this.tweets ?? (this.tweets = new Repository<Tweet>(this.context.Tweets));

        public IRepository<PrivateMessage> PrivateMessages => this.privateMessages ?? (this.privateMessages = new Repository<PrivateMessage>(this.context.PrivateMessages));

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
