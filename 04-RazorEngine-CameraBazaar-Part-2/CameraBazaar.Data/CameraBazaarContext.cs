namespace CameraBazaar.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using System.Data.Entity;

    public class CameraBazaarContext : IdentityDbContext<ApplicationUser>
    {
        public CameraBazaarContext()
            : base("name=CameraBazaarContext", throwIfV1Schema: false)
        {
        }

        public static CameraBazaarContext Create()
        {
            return new CameraBazaarContext();
        }

        public virtual DbSet<Seller> Sellers { get; set; }

        public virtual DbSet<Camera> Cameras { get; set; }

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