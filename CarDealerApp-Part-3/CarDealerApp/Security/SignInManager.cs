namespace CarDealerApp.Security
{
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Models;

    public static class SignInManager
    {
        public static bool IsAuthenticated(string sessionId, CarDealerContext context)
        {
            return context.Sessions.Any(s => s.SessionId == sessionId && s.IsActive);
        }

        public static void Logout(string sessionId, CarDealerContext context)
        {
            context.Sessions.FirstOrDefault(s => s.SessionId == sessionId).IsActive = false;
            context.SaveChanges();
        }

        public static User GetAuthenticatedUser(string sessionId, CarDealerContext context)
        {
            return context.Sessions.FirstOrDefault(s => s.SessionId == sessionId && s.IsActive)?.User;
        }
    }
}