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

        public bool AddTickets(List<Ticket> tickets)
        {
            if (tickets == null || tickets.Count == 0)
            {
                throw new ArgumentException("At least one ticket must be provided.");
            }
            foreach (var ticket in tickets)
            {
                _dbContext.Tickets.Add(ticket);

                if (!Save())
                {
                    return false;
                }
            }
			return true;
		}

        public bool CheckAvailability(int amount, int eventId)
        {
            if (amount <= 0)
            {
                return false;
            }

            int availableTicketsCount = _dbContext.Tickets
                .Where(t => t.EventId == eventId && !t.IsSold)
                .Count(); 

            return availableTicketsCount >= amount;
        }

        public bool ChangeStatus(int quantity, int eventId, int custId)
        {
            if (quantity <= 0)
            {
                return false;
            }

            // Retrieve the next available tickets for the specified event
            var ticketsToUpdate = _dbContext.Tickets
                .Where(t => t.EventId == eventId && !t.IsSold) // Filter by eventId and availability
                .OrderBy(t => t.Id) // Assuming tickets are ordered by their ID or creation date
                .Take(quantity) // Get the specified quantity of tickets
                .ToList(); // Retrieve tickets as a list

            // Check if the number of available tickets is less than the requested quantity
            if (ticketsToUpdate.Count < quantity)
            {
                return false; // Not enough tickets available
            }

            // Update the status and CustomerId for the selected tickets
            foreach (var ticket in ticketsToUpdate)
            {
                ticket.IsSold = true;
                ticket.CustomerId = custId;
            }

            // Save changes to the database
            return Save();
        }

        private bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
