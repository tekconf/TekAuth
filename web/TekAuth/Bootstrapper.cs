using AutoMapper;
using Tekconf.DTO;
using Conference = Tekconf.Data.Entities.Conference;
using Schedule = Tekconf.Data.Entities.Schedule;
using Session = Tekconf.Data.Entities.Session;
using Speaker = Tekconf.Data.Entities.Speaker;

namespace TekAuth
{
    public class Bootstrapper
    {
        public void Init()
        {
            //Mapper.CreateMap<Conference, Tekconf.DTO.Conference>()
            //    .ForMember(dest => dest.Address, opt => opt.MapFrom(conference => new Address
            //    {
            //        AddressLine1 = conference.AddressLine1,
            //        AddressLine2 = conference.AddressLine2,
            //        AddressLine3 = conference.AddressLine3,
            //        City = conference.City,
            //        Country = conference.Country,
            //        Latitude = conference.Latitude,
            //        Longitude = conference.Longitude,
            //        StateOrProvince = conference.StateOrProvince,
            //        PostalCode = conference.PostalCode
            //    }));

            //Mapper.CreateMap<Session, Tekconf.DTO.Session>();
            //Mapper.CreateMap<Schedule, Tekconf.DTO.Schedule>();
            //Mapper.CreateMap<Speaker, Tekconf.DTO.Speaker>();
        }
    }

    //public class AddressResolver : ValueResolver<Conference, Address>
    //{
    //    protected override Address ResolveCore(Conference conference)
    //    {
    //        return new Address
    //        {
    //            AddressLine1 = conference.AddressLine1,
    //            AddressLine2 = conference.AddressLine2,
    //            AddressLine3 = conference.AddressLine3,
    //            City = conference.City,
    //            Country = conference.Country,
    //            Latitude = conference.Latitude,
    //            Longitude = conference.Longitude,
    //            StateOrProvince = conference.StateOrProvince,
    //            PostalCode = conference.PostalCode
    //        };
    //    }
    //}
}