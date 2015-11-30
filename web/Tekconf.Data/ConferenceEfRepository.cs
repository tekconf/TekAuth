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
    public class ConferenceEfRepository : IConferenceRepository
    {

        // TODO: in a later stage, everything must be user-specific, eg: the
        // userid must always be passed in!  Don't do this in the first stage,
        // this allows us to show what can go wrong if you don't include the
        // user check.

        ConferenceContext _ctx;

        public ConferenceEfRepository(ConferenceContext ctx)
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

        public Conference GetConference(int id)
        {
            return _ctx.Conferences.FirstOrDefault(e => e.Id == id);
        }


        public IQueryable<Conference> GetConferences()
        {
            return _ctx.Conferences;
        }


        public RepositoryActionResult<Conference> InsertConference(Conference e)
        {
          try
                {
                _ctx.Conferences.Add(e);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Conference>(e, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Conference>(e, RepositoryActionStatus.NothingModified, null);
                }

                }
          catch (Exception ex)
          {
              return new RepositoryActionResult<Conference>(null, RepositoryActionStatus.Error, ex);
          }
        }




        public  RepositoryActionResult<Conference> UpdateConference(Conference e)
        {
                try
                {

                    // you can only update when an expense already exists for this id

                    var existingExpense = _ctx.Conferences.FirstOrDefault(exp => exp.Id == e.Id);

                    if (existingExpense == null)
                    {
                        return new RepositoryActionResult<Conference>(e, RepositoryActionStatus.NotFound);
                    }

                    // change the original entity status to detached; otherwise, we get an error on attach
                    // as the entity is already in the dbSet

                    // set original entity state to detached
                    _ctx.Entry(existingExpense).State = EntityState.Detached;

                    // attach & save
                    _ctx.Conferences.Attach(e);

                    // set the updated entity state to modified, so it gets updated.
                    _ctx.Entry(e).State = EntityState.Modified;


                    var result = _ctx.SaveChanges();
                    if (result > 0)
                    {
                         return new RepositoryActionResult<Conference>(e, RepositoryActionStatus.Updated);
                    }
                    else
                    {
                        return new RepositoryActionResult<Conference>(e, RepositoryActionStatus.NothingModified, null);
                    }
                }
                catch (Exception ex)
                {
                    return new RepositoryActionResult<Conference>(null, RepositoryActionStatus.Error, ex);
                }
           
        }

        public RepositoryActionResult<Conference> DeleteConference(int id)
        {
                try
                {
                    var exp = _ctx.Conferences.Where(e => e.Id == id).FirstOrDefault();
                    if (exp != null)
                    {
                        _ctx.Conferences.Remove(exp);
                        _ctx.SaveChanges();
                        return new RepositoryActionResult<Conference>(null, RepositoryActionStatus.Deleted);
                    }
                    return new RepositoryActionResult<Conference>(null, RepositoryActionStatus.NotFound);
                }
                catch (Exception ex)
                {
                    return new RepositoryActionResult<Conference>(null, RepositoryActionStatus.Error, ex);
                }
        }

    }
}
