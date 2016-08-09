using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using StructureMap;
using Tekconf.DTO;
using Conference = Tekconf.Data.Entities.Conference;
using Schedule = Tekconf.Data.Entities.Schedule;
using Session = Tekconf.Data.Entities.Session;
using Speaker = Tekconf.Data.Entities.Speaker;

namespace TekAuth.Infrastructure.Mapping
{
    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            var profiles =
                from t in typeof(AutoMapperRegistry).Assembly.GetTypes()
                where typeof(Profile).IsAssignableFrom(t)
                select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }

                cfg.CreateMap<Conference, Tekconf.DTO.Conference>()
                                .ForMember(dest => dest.Address, opt => opt.MapFrom(conference => new Address
                                {
                                    AddressLine1 = conference.AddressLine1,
                                    AddressLine2 = conference.AddressLine2,
                                    AddressLine3 = conference.AddressLine3,
                                    City = conference.City,
                                    Country = conference.Country,
                                    Latitude = conference.Latitude,
                                    Longitude = conference.Longitude,
                                    StateOrProvince = conference.StateOrProvince,
                                    PostalCode = conference.PostalCode
                                }));

                cfg.CreateMap<Session, Tekconf.DTO.Session>();
                cfg.CreateMap<Schedule, Tekconf.DTO.Schedule>();
                cfg.CreateMap<Speaker, Tekconf.DTO.Speaker>();
            });

            
            For<MapperConfiguration>().Use(config);
            For<IMapper>().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance));
        }
    }
}