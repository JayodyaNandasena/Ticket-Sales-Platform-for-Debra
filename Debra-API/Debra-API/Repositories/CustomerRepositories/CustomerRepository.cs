using Debra_API.Data;
using Debra_API.Entities;

namespace Debra_API.Repositories.CustomerRepositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private AppDBContext _dbContext;
        public CustomerRepository(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public bool Add(Customer customer)
        {
            if (customer == null)
            {
                return false; 
            }

            _dbContext.Customers.Add(customer);
            return Save();
        }

        public bool Delete(Customer customer)
        {
            if (customer == null)
            {
                return false;  
            }

            _dbContext.Customers.Remove(customer);
            return Save();

        }

        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer? GetByMobile(string mobile)
        {
            return _dbContext.Customers.FirstOrDefault(
                customer => customer.Mobile == mobile
                );
        }

        public bool Update(Customer customer)
        {
            if (customer == null)
            {
                return false;
            }

            _dbContext.Customers.Update(customer);
            return Save();
        }

        private bool Save()
        { 
            return _dbContext.SaveChanges() > 0;
        }

    }
}
