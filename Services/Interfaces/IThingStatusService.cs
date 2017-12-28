using System.Collections.Generic;
using System.Threading.Tasks;
using statusservice.Model;

namespace statusservice.Services.Interfaces
{
    public interface IThingStatusService
    {
        Task<List<ThingStatus>> getAllCurrentStatus();
        Task<List<ThingStatus>> getListCurrentStatus(int[] thingId);
        Task<ThingStatus> getCurrentStatus(int thingId);
        Task<List<ThingStatus>> updateCurrentStatus(int thingId, ThingStatus newStatus, bool recurrent = false);
        Task saveHistoryStatus(int thingId);
        Task<List<HistoryThingStatus>> getHistoryStatus(int thingId, long? initTimestamp, long? endTimestamp);
    }
}