using AutoMapper;
using Stripovi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stripovi.Web.Profiles
{
    public class StripProfile : Profile
    {
        public StripProfile()
        {
        CreateMap<Strip, Korpa>();
        CreateMap<Korpa, Strip>();
        }
        
    }
}
