namespace CameraBazaar.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Seller
    {
        public Seller()
        {
            this.Cameras = new HashSet<Camera>();
        }

        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ICollection<Camera> Cameras { get; set; }
    }
}