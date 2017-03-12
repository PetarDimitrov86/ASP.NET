namespace Whisper.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Whisper.Data.Data.WhisperContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Whisper.Data.Data.WhisperContext context)
        {
        }
    }
}
