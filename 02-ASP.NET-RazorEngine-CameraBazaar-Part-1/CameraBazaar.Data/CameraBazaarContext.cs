namespace CameraBazaar.Data
{
    using System.Data.Entity;
    using Models;

    public class CameraBazaarContext : DbContext
    {
        public CameraBazaarContext()
            : base("name=CameraBazaarContext")
        {
        }
        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<Camera> Cameras { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }

        public virtual DbSet<LightMetering> LightMeterings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camera>()
                .HasMany(c => c.LightMeterings)
                .WithMany(lm => lm.Cameras)
                .Map(clm => clm
                    .MapLeftKey("CameraId")
                    .MapRightKey("LightMeteringId")
                    .ToTable("CamerasLightMeterings"));

            base.OnModelCreating(modelBuilder);
        }
    }
}