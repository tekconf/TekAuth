using StructureMap;
using StructureMap.Configuration.DSL;
using Tekconf.Data;

namespace TekAuth
{
    public class TekAuthRegistry : Registry
    {
        public TekAuthRegistry()
        {
            For<IConferenceRepository>().Use<ConferenceEfRepository>();
            For<IScheduleRepository>().Use<ScheduleEfRepository>();

        }
    }
}