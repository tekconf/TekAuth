using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Tekconf.Data;
using Tekconf.Data.Entities;

namespace TekAuth.Controllers
{
    [MyAuth(AuthenticationRequirement.RequireAuthentication)]
    public class SchedulesController : ApiController
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IConferenceRepository _conferenceRepository;

        public SchedulesController()
        {
            _scheduleRepository = new ScheduleEfRepository(new ConferenceContext());
        }

        public SchedulesController(IScheduleRepository scheduleRepository, IConferenceRepository conferenceRepository)
        {
            _scheduleRepository = scheduleRepository;
            _conferenceRepository = conferenceRepository;
        }

        public async Task<IHttpActionResult> Get()
        {
            string name = ClaimsPrincipal.Current?.FindFirst("name")?.Value;

            if (string.IsNullOrWhiteSpace(name))
            {
                return Unauthorized();
            }

            var schedules = await _scheduleRepository.GetSchedules(name)
                .Include(s => s.Conference)
                .ProjectTo<Tekconf.DTO.Schedule>()
                .ToListAsync();

            return Ok(schedules);
        }

        public async Task<IHttpActionResult> Post(string conferenceSlug)
        {
            string name = ClaimsPrincipal.Current?.FindFirst("name")?.Value;
            if (string.IsNullOrWhiteSpace(name))
            {
                return Unauthorized();
            }

            var schedule =
                await _scheduleRepository.GetSchedules(name)
                    .Include(s => s.Conference)
                    .SingleOrDefaultAsync(s => s.Conference.Slug == conferenceSlug);

            if (schedule == null)
            {
                var conference =
                    await _conferenceRepository.GetConferences().SingleOrDefaultAsync(c => c.Slug == conferenceSlug);
                var user = await _conferenceRepository.GetUsers().SingleOrDefaultAsync(u => u.Name == name);

                var newSchedule = new Schedule()
                {
                    Conference = conference,
                    ConferenceId = conference.Id,
                    Created = DateTime.Now,
                    User = user,
                    UserId = user.Id
                };

                var result = _scheduleRepository.InsertSchedule(newSchedule);
                schedule = result.Entity;
            }

            var dto = Mapper.Map<Tekconf.DTO.Schedule>(schedule);

            return Ok(dto);
        }

        public async Task<HttpResponseMessage> Delete(string conferenceSlug)
        {
            string name = ClaimsPrincipal.Current?.FindFirst("name")?.Value;
            if (string.IsNullOrWhiteSpace(name))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
                ;
            }

            var schedule =
                await _scheduleRepository.GetSchedules(name)
                    .Include(s => s.Conference)
                    .SingleOrDefaultAsync(s => s.Conference.Slug == conferenceSlug);

            if (schedule == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
                ;
            }

            var result = _scheduleRepository.DeleteSchedule(schedule.Id);

            var returnCode = result.Status == RepositoryActionStatus.Deleted
                ? HttpStatusCode.NoContent
                : HttpStatusCode.InternalServerError;

            return Request.CreateResponse(returnCode);
        }
    }
}