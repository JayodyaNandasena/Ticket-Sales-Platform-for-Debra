using Debra_API.Data;
using Debra_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Debra_API.Repositories.TicketDetailsRepositories
{
    public class TicketDetailsRepository : ITicketDetailsRepository
    {
        private AppDBContext _dbContext;

        public TicketDetailsRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(TicketDetails ticketDetails)
        {
            _dbContext.TicketDetails.Add(ticketDetails);

            if (Save())
            {
                return _dbContext.TicketDetails
                .OrderByDescending(t => t.Id)
                .Select(t => t.Id)
                .FirstOrDefault();
            }

            return 0;
            
        }

        private bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

    }
}
