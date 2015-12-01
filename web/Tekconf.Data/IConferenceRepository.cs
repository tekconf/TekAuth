using System.Linq;
using System.Threading.Tasks;
using Tekconf.Data.Entities;

namespace Tekconf.Data
{
    public interface IConferenceRepository
    {
        RepositoryActionResult<Conference> DeleteConference(int id);
        Task<Conference> GetConference(int id);
        IQueryable<Conference> GetConferences();
    
        RepositoryActionResult<Conference> InsertConference(Conference e);
        RepositoryActionResult<Conference> UpdateConference(Conference e);

        IQueryable<User> GetUsers();
        Task<User> GetUser(string name);
        Task SaveUser(User user);
    }
}
