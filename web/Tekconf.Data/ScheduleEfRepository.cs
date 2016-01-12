using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekconf.Data.Entities;

namespace Tekconf.Data
{
    public class ScheduleEfRepository : IScheduleRepository
    {

        // TODO: in a later stage, everything must be user-specific, eg: the
        // userid must always be passed in!  Don't do this in the first stage,
        // this allows us to show what can go wrong if you don't include the
        // user check.

        private readonly ConferenceContext _ctx;

        public ScheduleEfRepository(ConferenceContext ctx)
        {
            _ctx = ctx;

            // Disable lazy loading - if not, related properties are auto-loaded when
            // they are accessed for the first time, which means they'll be included when
            // we serialize (b/c the serialization process accesses those properties).  
            // 
            // We don't want that, so we turn it off.  We want to eagerly load them (using Include)
            // manually.

            _ctx.Configuration.LazyLoadingEnabled = false;

        }
        public IQueryable<Schedule> GetSchedules(string userName)
        {
            return _ctx.Schedules.Where(s => s.User.Name == userName);
        }
        public async Task<Schedule> GetSchedule(int id)
        {
            return await _ctx.Schedules.FirstOrDefaultAsync(e => e.Id == id);
        }

        public RepositoryActionResult<Schedule> InsertSchedule(Schedule e)
        {
            try
            {
                _ctx.Schedules.Add(e);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Schedule>(e, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Schedule>(e, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Schedule>(null, RepositoryActionStatus.Error, ex);
            }
        }




        public RepositoryActionResult<Schedule> UpdateSchedule(Schedule e)
        {
            try
            {

                // you can only update when an expense already exists for this id

                var existingExpense = _ctx.Schedules.FirstOrDefault(exp => exp.Id == e.Id);

                if (existingExpense == null)
                {
                    return new RepositoryActionResult<Schedule>(e, RepositoryActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingExpense).State = EntityState.Detached;

                // attach & save
                _ctx.Schedules.Attach(e);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(e).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Schedule>(e, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Schedule>(e, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Schedule>(null, RepositoryActionStatus.Error, ex);
            }

        }

        public RepositoryActionResult<Schedule> DeleteSchedule(int id)
        {
            try
            {
                var exp = _ctx.Schedules.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Schedules.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Schedule>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Schedule>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Schedule>(null, RepositoryActionStatus.Error, ex);
            }
        }

    }
}
