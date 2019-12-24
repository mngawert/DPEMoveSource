using AutoMapper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Event, EventViewModel>().ReverseMap();
            CreateMap<EventJoinPersonType, EventJoinPersonTypeViewModel>().ReverseMap();
            CreateMap<MStatus, MStatusViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<MEventLevel, MEventLevelViewModel>().ReverseMap();
            CreateMap<MEventType, MEventTypeViewModel>().ReverseMap();
            CreateMap<EventUploadedFile, EventUploadedFileViewModel>().ReverseMap();
            CreateMap<UploadedFile, UploadedFileViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<CommentDbQuery, CommentViewModel>().ReverseMap();
        }
    }
}
