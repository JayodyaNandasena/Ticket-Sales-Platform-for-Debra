using Debra_API.Entities;

namespace Debra_API.Repositories.TicketRepositories
{
    public interface ITicketRepository
    {
        void addTickets(params Ticket[] tickets);
    }
}
