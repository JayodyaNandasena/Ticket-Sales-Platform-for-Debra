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

        public Partner GetById(string id)
        {
            return _dbContext.Partners.FirstOrDefault(
                partner => partner.Id == id
                );
        }

        public bool Update(Partner partner)
        {
            if (partner == null)
            {
                return false;
            }

            _dbContext.Partners.Update(partner);
            return Save();
        }

        private string getLastId()
        {
            var lastPartner = _dbContext.Partners
                                 .OrderByDescending(p => p.Id)
                                 .FirstOrDefault();

            if (lastPartner is null)
            {
                return "P000";
            }

            return lastPartner.Id;
        }

        public string generateNextId()
        {
            var lastId = getLastId();
            var numericPart = int.Parse(lastId.Substring(1));
            var nextNumericPart = numericPart + 1;
            return $"P{nextNumericPart:D3}";
        }

        private bool Save()
        { 
            return _dbContext.SaveChanges() > 0;
        }

    }
}
