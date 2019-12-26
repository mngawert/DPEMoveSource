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
        Task<Event> GetEventById(int id);
        Event AddViewCount(string eventCode);
    }
}
