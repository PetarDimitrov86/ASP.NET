namespace CarDealerApp.Services
{
    using CarDealer.Data;
    using System.Collections.Generic;
    using Models.ViewModels;
    using System.Linq;
    using CarDealer.Models;
    using System;

    public class LogsService
    {
        private CarDealerContext context;

        public LogsService()
        {
            this.context = new CarDealerContext();
        }

        public IEnumerable<LogViewModel> GetAllLogs(int pageToDisplay)
        {
            var logsVms = this.GetFilteredLogs("");
            LogViewModel.Page = pageToDisplay;
            return logsVms;
        }

        public void GenerateLog(ModifiedTable modifiedTable, Operation operation, int userId)
        {
            Log log = new Log
            {
                ModifiedTable = modifiedTable,
                Operation = operation,
                Time = DateTime.Now,
                User = context.Users.Find(userId)
            };
            context.Logs.Add(log);
            context.SaveChanges();
        }

        public DeleteLogViewModel GetLogToDeleteInfo(int id)
        {
            Log log = context.Logs.Find(id);

            var logToDelete = new DeleteLogViewModel
            {
                Date = log.Time,
                Id = log.Id,
                User = log.User.Username
            };

            return logToDelete;
        }

        public IEnumerable<LogViewModel> GetFilteredLogs(string username)
        {
            var allLogs = context.Logs.Where(l => l.User.Username.Contains(username)).ToList();
            var logsVms = new HashSet<LogViewModel>();

            foreach (var log in allLogs)
            {
                logsVms.Add(new LogViewModel
                {
                    Date = log.Time,
                    ModifiedTable = log.ModifiedTable,
                    Operation = log.Operation,
                    Username = log.User.Username,
                    Id = log.Id
                });
            }
            return logsVms;
        }

        public IEnumerable<UsersInfoViewModel> GetAllUsersInfo()
        {
            var usersVms = new HashSet<UsersInfoViewModel>();
            var userEntities = context.Users.ToList();

            foreach (var user in userEntities)
            {
                usersVms.Add(new UsersInfoViewModel
                {
                    Age = user.Age == null ? "[Not Specified]" : user.Age.ToString(),
                    Email = user.Email ?? "[Not Specified]",
                    IsSubscribed = user.Subscribed ? "Yes" : "No",
                    Name = user.Username
                });
            }

            return usersVms;
        }
    }
}