namespace TekAuth.Features.Department
{
    using AutoMapper;
    using Tekconf.Data.Entities;

    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Department, Index.Model>();
            CreateMap<Create.Command, Department>(MemberList.Source);
            CreateMap<Department, Edit.Command>().ReverseMap();
            CreateMap<Department, Delete.Command>();
        }
    }
}