namespace BookShopSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using BookShopSytem.Models.EntityModels;

    public class BookShopContext : IdentityDbContext<ApplicationUser>
    {
        public BookShopContext()
            : base("BookShopContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BookShopContext>());
        }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(book => book.RelatedBooks)
                .WithMany()
                .Map(configuration =>
                {
                    configuration.MapLeftKey("BookId");
                    configuration.MapRightKey("RelatedBookId");
                    configuration.ToTable("BooksRelatedBooks");
                });

            base.OnModelCreating(modelBuilder);
        }


        public static BookShopContext Create()
        {
            return new BookShopContext();
        }
    }
}