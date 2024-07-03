using Debra_API.Entities;

namespace Debra_API.Repositories.TicketRepositories
{
    public interface ITicketRepository
    {
        bool AddTickets(List<Ticket> tickets);
    }
}
