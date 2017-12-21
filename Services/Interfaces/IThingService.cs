using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using statusservice.Model;

namespace statusservice.Services.Interfaces
{
    public interface IThingService
    {
        Task<(Thing, HttpStatusCode)> getThing(int thingId);
        Task<(List<Thing>, HttpStatusCode)> getThingList(int[] thingId);
    }
}