namespace CarDealerApp.Models.ViewModels
{
    using CarDealer.Models;
    using System;

    public class LogViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public Operation Operation { get; set; }

        public ModifiedTable ModifiedTable { get; set; }
       
        public DateTime? Date { get; set; }

        public static int Page { get; set; }
    }
}