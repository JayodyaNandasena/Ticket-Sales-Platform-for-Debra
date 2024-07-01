using Debra_API.Data;
using Debra_API.Entities;

namespace Debra_API.Repositories.TicketRepositories
{
    public class TicketRepository : ITicketRepository
    {
        private AppDBContext _dbContext;

        public TicketRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void addTickets(params Ticket[] tickets)
        {
            if (tickets == null || tickets.Length == 0)
            {
                throw new ArgumentException("At least one ticket must be provided.");
            }
            foreach (var ticket in tickets)
            {
                _dbContext.Tickets.Add(ticket);
            }
        }
    }
}
