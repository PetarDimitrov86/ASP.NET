namespace CarDealerApp.Services
{
    using CarDealer.Data;

    public class UsersService
    {
        private CarDealerContext context;

        public UsersService()
        {
            this.context = new CarDealerContext();
        }
    }
}