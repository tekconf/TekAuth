using AutoMapper;
using Tekconf.Data.Entities;

namespace TekAuth
{
    public class Bootstrapper
    {
        public void Init()
        {
            Mapper.CreateMap<Conference, Tekconf.DTO.Conference>();
        }
    }
}