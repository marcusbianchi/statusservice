using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace statusservice.Model
{
    public class StatusDescription
    {
        public int statusDescriptionId { get; set; }
        [Required]
        [MaxLength(50)]
        public string context { get; set; }
        [Required]
        [MaxLength(50)]
        public string description { get; set; }
        [Required]
        [MaxLength(50)]
        public string statusName { get; set; }
        [Required]
        [MaxLength(50)]
        public string value { get; set; }
        [Required]
        public long timestampTicks { get; set; }

    }
}