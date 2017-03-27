using System.Collections.Generic;

namespace CameraBazaar.Models.EntityModels
{
    public class LightMetering
    {
        public LightMetering()
        {
            this.Cameras = new HashSet<Camera>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Camera> Cameras { get; set; }
    }
}