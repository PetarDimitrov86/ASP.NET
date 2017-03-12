namespace Whisper.Data.Contracts
{
    using Models;

    public interface ITwitterData
    {
        IRepository<Tweet> Tweets { get; }

        IRepository<User> Users { get; }

        IRepository<PrivateMessage> PrivateMessages { get; }

        int SaveChanges();
    }
}