using Debra_API.Data;
using Debra_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Debra_API.Repositories.PartnerRepositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private AppDBContext _dbContext;
        public PartnerRepository(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public bool Add(Partner partner)
        {
            if (partner == null)
            {
                return false; 
            }

            _dbContext.Partners.Add(partner);
            return Save();
        }

        public bool Delete(Partner partner)
        {
            if (partner == null)
            {
                return false;  
            }

            _dbContext.Partners.Remove(partner);
            return Save();

        }

        public IEnumerable<Partner> GetAll()
        {
            return _dbContext.Partners.ToList();
        }

        public Partner GetByEmail(string email)
        {
            return _dbContext.Partners.FirstOrDefault(
                partner => partner.Email == email
                );
        }

        public Partner GetById(int Id)
        {
            return _dbContext.Partners.FirstOrDefault(
                partner => partner.Id == Id
                );
        }

        public bool Update(Partner partner)
        {
            if (partner == null)
            {
                return false;
            }

            Partner newPartner = new Partner(
                partner.Id,
                partner.Name,
                partner.RegisteredDate,
                partner.Type,
                partner.Email,
                partner.Account                
                );

            return Save();
        }

        private bool Save()
        { 
            return _dbContext.SaveChanges() > 0;
        }

    }
}
