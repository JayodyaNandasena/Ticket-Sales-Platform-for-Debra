﻿using Debra_API.Data;
using Debra_API.Entities;

namespace Debra_API.Repositories.PartnerAccountRepositories
{
    public class PartnerAccountRepository : IPartnerAccountRepository
    {
        private AppDBContext _dbContext;
        public PartnerAccountRepository(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public PartnerAccount? findById(int id)
        {
            return _dbContext.PartnerAccounts.FirstOrDefault(
                partnerAccount => partnerAccount.PartnerId == id
                );
        }

        public PartnerAccount? findByUsername(string username)
        {

            return _dbContext.PartnerAccounts.FirstOrDefault(
                partnerAccount => partnerAccount.Username == username
                );
        }

        public bool Update(PartnerAccount partnerAccount)
        {
            if (partnerAccount == null)
            {
                return false;
            }

            _dbContext.PartnerAccounts.Update(partnerAccount);
            return Save();
        }

        private bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
