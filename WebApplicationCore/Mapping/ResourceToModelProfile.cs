﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Controllers.Resources;
using WebApplicationCore.Domain.Models;
using WebApplicationCore.Resource;

namespace WebApplicationCore.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<UserCredentialsResource, User>();
            CreateMap<SaveCategoryResource, Category>();
        }
    }
}
