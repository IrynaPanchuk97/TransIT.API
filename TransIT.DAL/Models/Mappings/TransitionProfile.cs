﻿using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;
using AutoMapper;

namespace TransIT.DAL.Models.Mappings
{
    public class TransitionProfile : Profile
    {
        public TransitionProfile()
        {
            CreateMap<TransitionDTO, Transition>()
            .ForMember(i => i.ModId, opt => opt.Ignore())
            .ForMember(i => i.CreateId, opt => opt.Ignore())
            .ForMember(i => i.Mod, opt => opt.Ignore())
            .ForMember(i => i.Create, opt => opt.Ignore())
            .ForMember(i => i.ActionType, opt => opt.Ignore())
            .ForMember(i => i.ActionTypeId, opt => opt.MapFrom(x => x.ActionType.Id))
            .ForMember(i => i.FromState, opt => opt.Ignore())
            .ForMember(i => i.FromStateId, opt => opt.MapFrom(x => x.FromState.Id))
            .ForMember(i => i.ToState, opt => opt.Ignore())
            .ForMember(i => i.ToStateId, opt => opt.MapFrom(x => x.ToState.Id))
            .ForMember(i => i.CreateDate, opt => opt.Ignore())
            .ForMember(i => i.ModDate, opt => opt.Ignore());

            CreateMap<Transition, TransitionDTO>();
        }
    }
}
