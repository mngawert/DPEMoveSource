using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public interface IMasterService
    {
        IEnumerable<MEventObjective> GetMEventObjective();
        IEnumerable<MEventLevel> GetMEventLevel();
        IEnumerable<MEventGoal> GetMEventGoal();
        IEnumerable<MSport> GetMSport();
        IEnumerable<MEventFacilitiesTopic> GetMEventFacilitiesTopic();
    }
}
