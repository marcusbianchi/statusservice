using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace statusservice.Model
{
    public class ThingStatus
    {
        public int thingStatusId { get; set; }
        [Required]
        public int? thingId { get; set; }
        [NotMapped]
        public Thing thing { get; set; }
        public IList<ContextStatus> statusContexts { get; set; }
    }
}