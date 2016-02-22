using MvvmCross.Platform.IoC;
using TekConf.Mobile.Core.ViewModels;
using AutoMapper;
using Tekconf.DTO;
using MvvmCross.Platform;

namespace TekConf.Mobile.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			InitAutoMapper();

			RegisterAppStart<ConferencesViewModel>();
        }

		private void InitAutoMapper()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Conference, ConferenceListViewModel>()
				   .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
				   .ForMember(dest => dest.StateOrProvince, opt => opt.MapFrom(src => src.Address.StateOrProvince)); 
			});

			Mvx.RegisterSingleton<IMapper>(() => config.CreateMapper());
		}
    }
}
