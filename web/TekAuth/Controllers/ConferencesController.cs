using System;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using Tekconf.Data;
using Tekconf.Data.Entities;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;


namespace TekAuth.Controllers
{
    [MyAuth(AuthenticationRequirement.AllowAnoymous)]
    public class ConferencesController : ApiController
    {
        private readonly IConferenceRepository _repository;

        public ConferencesController()
        {
            _repository = new ConferenceEfRepository(new ConferenceContext());
        }

        public ConferencesController(IConferenceRepository repository)
        {
            _repository = repository;
        }


        public async Task<IHttpActionResult> Get()
        {
            try
            {
                await CreateUserIfNotExists();
                var conferences = await _repository
                    .GetConferences()
                    .Include(c => c.Sessions)
                    .Include(c => c.Sessions.Select(s => s.Speakers))
                    .ProjectTo<Tekconf.DTO.Conference>()
                    .ToListAsync();

                return Ok(conferences);

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
               // return InternalServerError(ex);
            }
        }

        private async Task CreateUserIfNotExists()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    string name = ClaimsPrincipal.Current?.FindFirst("name")?.Value;
                    var user = await _repository
                        .GetUser(name);
                    if (user == null)
                    {
                        user = new User()
                        {
                            Name = name
                        };
                        await _repository.SaveUser(user);
                    }
                }
                catch (Exception ex)
                {
                    var xx = ex.Message;
                }
            }
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var conference = await _repository
                    .GetConferences()
                    .Where(c => c.Slug == id)
                    .ProjectTo<Tekconf.DTO.Conference>()
                    .SingleOrDefaultAsync();

                if (conference == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(conference);
                }
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }
    }
}