using Debra_API.Entities;

namespace Debra_API.Repositories.EventRepositories
{
    public interface IEventRepository
    {
        Event? Add(Event newEvent);
        List<Event> GetAll();
		Event? GetById(int Id);
	}
}
