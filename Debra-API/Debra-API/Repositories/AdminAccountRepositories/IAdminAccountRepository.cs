using Debra_API.Entities;

namespace Debra_API.Repositories.AdminAccountRepositories
{
    public interface IAdminAccountRepository
    {
        bool CreateAccount(AdminAccount adminAccount);
        bool UpdateAccount(AdminAccount adminAccount);
        bool DeleteAccount(AdminAccount adminAccount);
        IEnumerable<AdminAccount> GetAllAccounts();
        AdminAccount GetAdminAccount(string Username);
    }
}
