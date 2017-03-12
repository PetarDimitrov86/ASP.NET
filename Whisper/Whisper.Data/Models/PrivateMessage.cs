namespace Whisper.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PrivateMessage
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public virtual User Sender { get; set; }

        public virtual User Recipient { get; set; }
    }
}