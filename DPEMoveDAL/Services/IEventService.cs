using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public interface IEventService
    {
        IEnumerable<EventViewModel> GetEvent(EventViewModel model);
        IEnumerable<EventDbQuery> GetEvent2(EventRequestViewModel model);
        Task<Event> GetEventDetails(int id);
        List<EventFacilities> GetEventFacilities(int id);

        Task<EventViewModel2> GetEventDetails2(int id);
        void UpdateEvent(EventViewModel2 model);
        Event AddViewCount(string eventCode);
    }
}
