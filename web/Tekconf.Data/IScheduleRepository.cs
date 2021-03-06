﻿using System.Linq;
using System.Threading.Tasks;
using Tekconf.Data.Entities;

namespace Tekconf.Data
{
    public interface IScheduleRepository
    {
        RepositoryActionResult<Schedule> DeleteSchedule(int id);
        IQueryable<Schedule> GetSchedules(string userName);
        Task<Schedule> GetSchedule(int id);

        RepositoryActionResult<Schedule> InsertSchedule(Schedule e);
        RepositoryActionResult<Schedule> UpdateSchedule(Schedule e);

    }
}