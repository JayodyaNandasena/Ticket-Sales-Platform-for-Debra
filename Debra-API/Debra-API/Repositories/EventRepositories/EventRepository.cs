using Debra_API.Data;
using Debra_API.Entities;

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



        private bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
