using AutoMapper;
using Tekconf.Data.Entities;

namespace TekAuth
{
    public class Bootstrapper
    {
        public void Init()
        {
            Mapper.CreateMap<Conference, Tekconf.DTO.Conference>();
            Mapper.CreateMap<Session, Tekconf.DTO.Session>();
            Mapper.CreateMap<Schedule, Tekconf.DTO.Schedule>();
            Mapper.CreateMap<Speaker, Tekconf.DTO.Speaker>();
        }
    }
}