using Debra_API.Entities;

namespace Debra_API.Repositories.PartnerRepositories
{
    public interface IPartnerRepository
    {
        bool Add(Partner partner);
        bool Update(Partner partner);
        bool Delete(Partner partner);
        IEnumerable<Partner> GetAll();
        Partner GetByEmail(string email);
        Partner GetById(int Id);
    }
}
