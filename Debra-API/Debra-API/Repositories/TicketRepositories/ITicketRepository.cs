using Debra_API.Entities;

namespace Debra_API.Repositories.TicketRepositories
{
    public interface ITicketRepository
    {
        bool AddTickets(List<Ticket> tickets);
        bool CheckAvailability(int amount, int eventId);
        bool ChangeStatus(int quantity, int eventId, int custId);
    }
}
