namespace CameraBazaar.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Session
    {
        [Key]
        public string SessionId { get; set; }

        public bool IsActive { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public override string ToString()
        {
            return $"{this.SessionId}\t{this.User.Id}";
        }
    }
}