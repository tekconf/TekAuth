using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Tekconf.Data;
using Tekconf.Data.Entities;

namespace TekAuth.Controllers
{
    [MyAuth(AuthenticationRequirement.RequireAuthentication)]
    public class SchedulesController : ApiController
    {
        private readonly IConferenceRepository _repository;

        public SchedulesController()
        {
            _repository = new ConferenceEfRepository(new ConferenceContext());
        }

        public SchedulesController(IConferenceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IHttpActionResult> Get()
        {
            string name = ClaimsPrincipal.Current?.FindFirst("name")?.Value;
            
            return Ok();
        }
    }
}