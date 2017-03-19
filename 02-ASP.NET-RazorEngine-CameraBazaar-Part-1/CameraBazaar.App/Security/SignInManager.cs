namespace CameraBazaar.App.Security
{
    using System.Linq;
    using Data;
    using CameraBazaar.Models;

    public static class SignInManager
    {
        private static CameraBazaarContext context = new CameraBazaarContext();

        public static bool IsAuthenticated(string sessionId)
        {
            return context.Sessions.Any(s => s.SessionId == sessionId && s.IsActive);
        }

        public static void Logout(string sessionId)
        {
            context.Sessions.FirstOrDefault(s => s.SessionId == sessionId).IsActive = false;
            context.SaveChanges();
        }

        public static User GetAuthenticatedUser(string sessionId)
        {
            return context.Sessions.FirstOrDefault(s => s.SessionId == sessionId && s.IsActive)?.User;
        }
    }
}