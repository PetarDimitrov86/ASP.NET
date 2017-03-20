namespace CarDealer.Models
{
    using System;

    public class Log
    {
        public int Id { get; set; }

        public Operation Operation { get; set; }

        public ModifiedTable ModifiedTable { get; set; }

        public DateTime? Time { get; set; }

        public virtual User User { get; set; }
    }

    public enum Operation
    {
        Add,
        Edit,
        Delete
    }

    public enum ModifiedTable
    {
        Car,
        Sale,
        Supplier
    }
}