using System.Linq;
using Tekconf.Data.Entities;

namespace Tekconf.Data
{
    public interface IConferenceRepository
    {
        RepositoryActionResult<Conference> DeleteConference(int id);
        Conference GetConference(int id);
        IQueryable<Conference> GetConferences();
    
        RepositoryActionResult<Conference> InsertConference(Conference e);
        RepositoryActionResult<Conference> UpdateConference(Conference e);
    }
}
