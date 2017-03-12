namespace Whisper.Data.Data
{
    using Contracts;

    public class Data
    {
        private static ITwitterData context;

        public static ITwitterData Context => context ?? (context = new TwitterData());
    }
}