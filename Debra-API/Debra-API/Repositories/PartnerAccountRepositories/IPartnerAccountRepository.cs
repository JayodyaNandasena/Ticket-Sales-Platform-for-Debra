using Debra_API.Entities;

namespace Debra_API.Repositories.PartnerAccountRepositories
{
    public interface IPartnerAccountRepository
    {
        PartnerAccount? findByUsername(string username);
        PartnerAccount? findById(int id);
        public bool Update(PartnerAccount partnerAccount);
    }
}
