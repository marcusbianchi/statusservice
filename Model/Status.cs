using System.Collections.Generic;

namespace statusservice.Model
{
    public class Status
    {
        public int statusId { get; set; }
        public int thingId { get; set; }
        public IList<StatusDescription> statusDescrptions { get; set; }
    }
}