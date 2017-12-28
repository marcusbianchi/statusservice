using System.Collections.Generic;
using System.Threading.Tasks;
using statusservice.Model;

namespace statusservice.Services.Interfaces
{
    public interface IContextStatusService
    {
        Task<List<ContextStatus>> getAllCurrentContextStatus(int thingId);
        Task<ContextStatus> getCurrentContextStatus(int thingId, string context);
        Task<List<ContextStatus>> updateCurrentContextStatus(int thingId, string context, ContextStatus newContextStatus, bool recurrent = false);
        Task saveHistoryContextStatus(int thingId, string context);
        Task<List<HistoryContextStatus>> getHistoryContextStatus(int thingId, string context);
    }
}