using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace statusservice.Model
{
    public class HistoryThingStatus
    {
        public int historyThingStatusId { get; set; }
        [Required]
        public int? thingId { get; set; }
        [NotMapped]
        public Thing thing { get; set; }
        public IList<HistoryContextStatus> statusContexts { get; set; }
    }
}