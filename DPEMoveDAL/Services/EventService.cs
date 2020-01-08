using AutoMapper;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<EventService> _logger;

        public EventService(AppDbContext context, IMapper mapper, ILogger<EventService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Event AddViewCount(string eventCode)
        {
            var q = _context.Event.Where(a => a.EventCode == eventCode).FirstOrDefault();

            q.ReadCount += 1;

            _context.SaveChanges();

            return q;
        }
        public async Task<Event> GetEventDetails(int id)
        {
            var q = await _context.Event
                .Include(a => a.Address)
                .Include(a => a.EventLevel)
                .Include(a => a.EventType)
                .Include(a => a.EventNearby)
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


        public async Task<EventViewModel2> GetEventDetails2(int id)
        {
            var @event = await GetEventDetails(id);
            var ev = _mapper.Map<EventViewModel2>(@event);

            var addr = @event.Address;

            if (addr != null)
            {
                ev.BuildingName = addr.BuildingName;
                ev.Moo = addr.Moo;
                ev.No = addr.No;
                ev.HousePropertyName = addr.HousePropertyName;
                ev.Lane = addr.Lane;
                ev.Floor = addr.Floor;
                ev.Soi = addr.Soi;
                ev.Road = addr.Road;
                ev.ProvinceCode = addr.ProvinceCode;
                ev.AmphurCode = addr.AmphurCode;
                ev.TambonCode = addr.TambonCode;
                ev.Postcode = addr.Postcode;
                ev.Latitude = addr.Latitude;
                ev.Longitude = addr.Longitude;
            }

            var listMEventObjective = _context.MEventObjective
                .Select(aa => new MEventObjectiveViewModel
                {
                    MEventObjectiveId = aa.MEventObjectiveId,
                    EventObjectiveName = aa.EventObjectiveName,
                    Selected = @event.EventObjective.Any(b => b.MEventObjectiveId == aa.MEventObjectiveId)
                })
                .ToList();

            ev.MEventObjective = listMEventObjective.ToArray();
            ev.EventSport = _context.EventSport.Where(a => a.EventId == id).ToList();

            return ev;
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
                .Include(a => a.EventUploadedFile)
                    .ThenInclude(b => b.UploadedFile)
                .Select(a => _mapper.Map<EventViewModel>(a))
                //.Select( a => new EventViewModel 
                //{
                //    EventId = a.EventId,
                //    EventCode = a.EventCode,
                //    EventName = a.EventName,
                //    Address = _mapper.Map<AddressViewModel>(a.Address)
                //})
                ;

            if (!string.IsNullOrEmpty(model.EventCode))
            {
                q = q.Where(a => a.EventCode.Contains(model.EventCode));
            }
            if (!string.IsNullOrEmpty(model.EventName))
            {
                q = q.Where(a => a.EventName.Contains(model.EventName));
            }
            if (model.EventStartTimestamp != null)
            {
                q = q.Where(a => model.EventStartTimestamp.CompareTo(a.EventFinishTimestamp) <= 0);
            }
            if (model.EventFinishTimestamp != null)
            {
                q = q.Where(a => a.EventStartTimestamp.CompareTo(model.EventFinishTimestamp) <= 0);
            }

            //return q;

            /* Order by*/

            //if (!string.IsNullOrWhiteSpace(model.OrderBy))
            //{
            //    if (model.OrderBy == "EventCode")
            //    {
            //        q = q.OrderBy(a => a.EventCode);
            //    }
            //    else if (model.OrderBy == "EventName")
            //    {
            //        q = q.OrderBy(a => a.EventName);
            //    }
            //}

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
                    _logger.LogDebug("eventsNearby = ", eventsNearby);

                    var nn = eventsNearby.OrderByDescending(a => a.EventStartTimestamp);

                    return PaginatedList<EventViewModel>.Create(nn, model.LimitStart ?? 1, model.LimitSize ?? 10000); ;
                }
            }

            _logger.LogDebug("q = ", q);

            var qq = q.OrderByDescending(a => a.EventStartTimestamp);

            return PaginatedList<EventViewModel>.Create(qq, model.LimitStart ?? 1, model.LimitSize ?? 10000);
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
                    EventStartTH = a.EventStartTimestamp.ToString("d MMM yy"),
                    EventFinishTimestamp = a.EventFinishTimestamp,
                    Budget = a.Budget,
                    Budgetused = a.Budgetused,
                    PublishUrl = a.PublishUrl,
                    ReadCount = a.ReadCount,
                    CommentCount = _context.Comment.Where(b => b.EventCode == a.EventCode).Count(),
                    AddressDescription = a.Address.Description,
                    Latitude = a.Address.Latitude,
                    Longitude = a.Address.Longitude,
                    FileUrl = a.EventUploadedFile.FirstOrDefault().UploadedFile.FileUrl,
                    AmphurCode = a.Address.AmphurCode,
                    TambonCode = a.Address.TambonCode,
                    ProvinceCode = a.Address.ProvinceCode,
                    VoteAvg = _context.Vote.Where(b => b.EventCode == a.EventCode).Count() == 0 ? 0: _context.Vote.Where(b => b.EventCode == a.EventCode).Average(c => c.VoteValue)
                });

            if (!string.IsNullOrEmpty(model.EventCode))
            {
                q = q.Where(a => a.EventCode.Contains(model.EventCode));
            }
            if (!string.IsNullOrEmpty(model.EventName))
            {
                q = q.Where(a => a.EventName.Contains(model.EventName));
            }
            if (!string.IsNullOrEmpty(model.EventStart))
            {
                //ThaiBuddhistCalendar cal = new ThaiBuddhistCalendar();
                //var dateStart = cal.ToDateTime(int.Parse(model.EventStart.Substring(6, 4)), int.Parse(model.EventStart.Substring(3, 2)), int.Parse(model.EventStart.Substring(0, 2)), 0, 0, 0, 0);

                var dateStart = DateTime.Parse(model.EventStart);
                q = q.Where(a => dateStart.ToString("yyyyMMdd").CompareTo(a.EventFinishTimestamp.Value.ToString("yyyyMMdd")) <= 0);
            }
            if (!string.IsNullOrEmpty(model.EventFinish))
            {
                //ThaiBuddhistCalendar cal = new ThaiBuddhistCalendar();
                //var dateFinish = cal.ToDateTime(int.Parse(model.EventFinish.Substring(6, 4)), int.Parse(model.EventFinish.Substring(3, 2)), int.Parse(model.EventFinish.Substring(0, 2)), 0, 0, 0, 0);
                var dateFinish = DateTime.Parse(model.EventStart);
                q = q.Where(a => a.EventStartTimestamp.ToString("yyyyMMdd").CompareTo(dateFinish.ToString("yyyyMMdd")) <= 0);
            }

            if (!string.IsNullOrEmpty(model.ProvinceCode))
            {
                q = q.Where(a => a.ProvinceCode == model.ProvinceCode);
            }
            if (!string.IsNullOrEmpty(model.AmphurCode))
            {
                q = q.Where(a => a.AmphurCode == model.AmphurCode);
            }
            if (!string.IsNullOrEmpty(model.TambonCode))
            {
                q = q.Where(a => a.TambonCode == model.TambonCode);
            }


            q = q.OrderByDescending(a => a.EventStartTimestamp);

            _logger.LogDebug("GetEvent q before Latitude,Longitude = {0},{1}", model.Latitude, model.Longitude);

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
                _logger.LogDebug("eventsNearby = {0}", eventsNearby);
                return PaginatedList<EventDbQuery>.Create(eventsNearby, model.LimitStart ?? 1, model.LimitSize ?? 10000); ;
            }

            _logger.LogDebug("q = {0}", q);
            return PaginatedList<EventDbQuery>.Create(q.ToList(), model.LimitStart ?? 1, model.LimitSize ?? 10000);
        }
        public void UpdateEvent(EventViewModel2 model)
        {
            /* EVENT */
            var ev = _context.Event.Where(a => a.EventId == model.EventId).FirstOrDefault();

            ev.EventName = model.EventName;
            //ev.EventCode = model.EventCode;
            ev.Budget = model.Budget;
            ev.Budgetused = model.Budgetused;
            ev.ResponsiblePersonType = model.ResponsiblePersonType;
            ev.ResponsiblePerson = model.ResponsiblePerson;
            ev.ResponsiblePersonCode = model.ResponsiblePersonCode;
            ev.EventShortDescription = model.EventShortDescription;
            ev.EventDescription = model.EventDescription;
            ev.EventStartTimestamp = model.EventStartTimestamp;
            ev.EventFinishTimestamp = model.EventFinishTimestamp;
            ev.ContactPersonName = model.ContactPersonName;
            ev.ContactPersonEmail = model.ContactPersonEmail;
            ev.ContactPersonMobile = model.ContactPersonMobile;
            ev.ContactPersonFax = model.ContactPersonFax;
            //ev.ProjectCode = model.ProjectCode;
            ev.ProjectSelect = model.ProjectSelect;
            ev.PublishUrl = model.PublishUrl;
            ev.EventLevelId = model.EventLevelId;
            ev.EventLevelEtc = model.EventLevelEtc;

            _context.Update(ev).State = EntityState.Modified;
            _context.SaveChanges();


            /* EVENT_OBJECTIVE */
            var evObjective = _context.EventObjective.Where(a => a.EventId == ev.EventId);
            foreach (var x in evObjective)
            {
                _context.Remove(x).State = EntityState.Deleted;
            }
            _context.SaveChanges();

            foreach (var id in model.MEventObjectiveIds ?? new int[] { })
            {
                _logger.LogDebug("inserting EventObjective id={0}", id);
                var obj = new EventObjective 
                { 
                    EventId = model.EventId,
                    MEventObjectiveId = id,
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now                    
                };

                _context.Entry(obj).State = EntityState.Added;
            }
            _context.SaveChanges();
        }
    }
}
