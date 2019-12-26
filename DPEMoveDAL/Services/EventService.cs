using AutoMapper;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EventService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Event AddViewCount(string eventCode)
        {
            var q = _context.Event.Where(a => a.EventCode == eventCode).FirstOrDefault();

            q.ReadCount += 1;

            _context.SaveChanges();

            return q;
        }
        public async Task<Event> GetEventById(int id)
        {
            var q = await _context.Event
                .Include(a => a.Address)
                .Include(a => a.EventLevel)
                .Include(a => a.EventType)
                .Include(a => a.EventJoinPersonType)
                    .ThenInclude(b => b.JoinPersonType)
                .Include(a => a.EventUploadedFile)
                    .ThenInclude(b => b.UploadedFile)
                .Include(a => a.EventObjective)
                    .ThenInclude(b => b.MEventObjective)
                .Include(a => a.EventGoal)
                    .ThenInclude(b => b.MEventGoal)
                .Include(a => a.EventSport)
                    .ThenInclude(b => b.Sport)
                .Include(a => a.EventFacilities)
                    .ThenInclude(b => b.MEventFacilitiesTopic)
                .Where(a => a.EventId == id)
                .FirstOrDefaultAsync();

            return q;
        }


        public IEnumerable<EventViewModel> GetEvent(EventViewModel model)
        {
            var q = _context.Event
                .Include(a => a.Address)
                //.Include(a => a.EventLevel)
                //.Include(a => a.EventType)
                //.Include(a => a.EventJoinPersonType)
                //    .ThenInclude(b => b.JoinPersonType)
                //.Include(a => a.StatusNavigation)
                //.Include(a => a.EventUploadedFile)
                //    .ThenInclude(b => b.UploadedFile)
                .Select(a => _mapper.Map<EventViewModel>(a))
                //.Select( a => new EventViewModel 
                //{
                //    EventId = a.EventId,
                //    EventCode = a.EventCode,
                //    EventName = a.EventName,
                //    Address = _mapper.Map<AddressViewModel>(a.Address)
                //})
                ;

            if (model.EventCode != null)
            {
                q = q.Where(a => a.EventCode.Contains(model.EventCode));
            }
            if (model.EventName != null)
            {
                q = q.Where(a => a.EventName.Contains(model.EventName));
            }

            /* Order by*/
            q = q.OrderBy(a => a.CreatedDate);
            if (!string.IsNullOrWhiteSpace(model.OrderBy))
            {
                if (model.OrderBy == "EventCode")
                {
                    q = q.OrderBy(a => a.EventCode);
                }
                else if (model.OrderBy == "EventName")
                {
                    q = q.OrderBy(a => a.EventName);
                }
            }

            if (model.Address != null)
            {
                if (model.Address?.Latitude != null && model.Address?.Longitude != null)
                {
                    var eventsNearby = new List<EventViewModel>();

                    foreach (var e in q)
                    {
                        if (e.Address?.Latitude != null && e.Address?.Longitude != null)
                        {
                            var lat1 = Convert.ToDouble(e.Address.Latitude);
                            var lon1 = Convert.ToDouble(e.Address.Longitude);
                            var lat2 = Convert.ToDouble(model.Address.Latitude);
                            var lon2 = Convert.ToDouble(model.Address.Longitude);

                            var distance = GeoDataSource.Distance(lat1, lon1, lat2, lon2, 'K');
                            var ev = _mapper.Map<EventViewModel>(e);
                            ev.Distance = distance;

                            if (model.Distance != null)
                            {
                                if (ev.Distance <= model.Distance)
                                {
                                    eventsNearby.Add(ev);
                                }
                            }
                            else
                            {
                                eventsNearby.Add(ev);
                            }
                        }
                    }

                    return PaginatedList<EventViewModel>.Create(eventsNearby.ToList(), model.LimitStart ?? 1, model.LimitSize ?? 10000); ;
                }
            }

            return PaginatedList<EventViewModel>.Create(q, model.LimitStart ?? 1, model.LimitSize ?? 10000);
        }

        public IEnumerable<EventDbQuery> GetEvent2(EventRequestViewModel model)
        {
            var q = _context.Event
                .Select(a => new EventDbQuery 
                { 
                    EventId = a.EventId,
                    EventCode = a.EventCode,
                    EventName = a.EventName,
                    EventShortDescription = a.EventShortDescription,
                    EventDescription = a.EventShortDescription,
                    EventStartTimestamp = a.EventStartTimestamp,
                    EventFinishTimestamp = a.EventFinishTimestamp,
                    Budget = a.Budget,
                    Budgetused = a.Budgetused,
                    PublishUrl = a.PublishUrl,
                    ReadCount = a.ReadCount,
                    CommentCount = _context.Comment.Where(b => b.EventCode == a.EventCode).Count(),
                    AddressDescription = a.Address.Description,
                    Latitude = a.Address.Latitude,
                    Longitude = a.Address.Longitude,
                    FileUrl = a.EventUploadedFile.FirstOrDefault().UploadedFile.FileUrl
                });


            if (model.EventCode != null)
            {
                q = q.Where(a => a.EventCode.Contains(model.EventCode));
            }
            if (model.EventName != null)
            {
                q = q.Where(a => a.EventName.Contains(model.EventName));
            }

            if (model.Latitude != null && model.Longitude != null)
            {
                var eventsNearby = new List<EventDbQuery>();

                foreach (var e in q)
                {
                    if (e.Latitude != null && e.Longitude != null)
                    {
                        var lat1 = Convert.ToDouble(e.Latitude);
                        var lon1 = Convert.ToDouble(e.Longitude);
                        var lat2 = Convert.ToDouble(model.Latitude);
                        var lon2 = Convert.ToDouble(model.Longitude);

                        var distance = GeoDataSource.Distance(lat1, lon1, lat2, lon2, 'K');
                        e.Distance = distance;

                        if (model.Distance != null)
                        {
                            if (e.Distance <= model.Distance)
                            {
                                eventsNearby.Add(e);
                            }
                        }
                        else
                        {
                            eventsNearby.Add(e);
                        }
                    }
                }
                return PaginatedList<EventDbQuery>.Create(eventsNearby.ToList(), model.LimitStart ?? 1, model.LimitSize ?? 10000); ;
            }
            return PaginatedList<EventDbQuery>.Create(q, model.LimitStart ?? 1, model.LimitSize ?? 10000);
        }
    }
}
