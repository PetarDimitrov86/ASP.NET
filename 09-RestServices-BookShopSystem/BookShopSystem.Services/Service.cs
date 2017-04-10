using BookShopSystem.Data;

namespace BookShopSystem.Services
{
    public abstract class Service
    {
        protected Service()
        {
            this.Context = new BookShopContext();
        }

        protected BookShopContext Context { get; set; }
    }
}