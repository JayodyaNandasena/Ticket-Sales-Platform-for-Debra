using Debra_API.Data;
using Debra_API.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Debra_API.Repositories.EventRepositories
{
    public class EventRepository : IEventRepository
    {
        private AppDBContext _dbContext;

        public EventRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Event? Add(Event newEvent)
        {
            if (newEvent == null)
            {
                return null;
            }

            _dbContext.Events.Add(newEvent);

            if (Save())
            {
                return newEvent;
            }

            return null;
        }

		public List<Event> GetAll()
		{
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            return _dbContext.Events
                .Where(e => e.Date > today)
                .OrderBy(e => e.Date)
                .ToList();
		}

        public List<Event> GetUpcomingEvents()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            return _dbContext.Events
                .Where(e => e.Date > today)
                .OrderBy(e => e.Date)
                .Take(3)
                .ToList();
        }

        public List<dynamic> GetUpcomingEventTitles()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            return _dbContext.Events
                .Where(e => e.Date > today)
                .OrderBy(e => e.Date)
                .Select(e => new
                {
                    Id = e.Id,
                    Title = e.Title
                })
                .ToList<dynamic>();
        }



        public Event? GetById(int Id)
		{
            return _dbContext.Events.FirstOrDefault(e => e.Id == Id);
		}

		private bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
