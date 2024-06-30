using Debra_API.Data;
using Debra_API.Entities;

namespace Debra_API.Repositories.AdminAccountRepositories
{
    public class AdminAccountRepository : IAdminAccountRepository
    {
        private AppDBContext _dbContext;
        public AdminAccountRepository(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public bool CreateAccount(AdminAccount adminAccount)
        {
            if (adminAccount == null)
            {
                return false;
            }

            _dbContext.AdminAccounts.Add(adminAccount);
            return Save();
        }

        public bool DeleteAccount(AdminAccount adminAccount)
        {
            if (adminAccount == null)
            {
                return false;
            }

            _dbContext.AdminAccounts.Remove(adminAccount);
            return Save();
        }

        public AdminAccount? GetAdminAccount(string Username)
        {
            return _dbContext.AdminAccounts.FirstOrDefault(
                adminaccount => adminaccount.Username == Username);
        }

        public IEnumerable<AdminAccount> GetAllAccounts()
        {
            return _dbContext.AdminAccounts.ToList();
        }

        public bool UpdateAccount(AdminAccount adminAccount)
        {
            if (adminAccount == null)
            {
                return false;
            }

            _dbContext.AdminAccounts.Update(adminAccount);
            return Save();
        }

        private bool Save()
        { 
            return _dbContext.SaveChanges() > 0;
        }

    }
}
