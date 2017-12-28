using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace statusservice.Model
{
    public class ContextStatus
    {
        [JsonIgnore]
        public int contextStatusId { get; set; }
        [Required]
        [MaxLength(50)]
        public string context { get; set; }
        [Required]
        [MaxLength(50)]
        public string contextDescription { get; set; }
        [Required]
        [MaxLength(50)]
        public string statusName { get; set; }
        [Required]
        [MaxLength(50)]
        public string value { get; set; }
        public long startTimestampTicks { get; set; }
        public long endTimestampTicks { get; set; }
    }
}