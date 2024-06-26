using Debra_API.Entities;

namespace Debra_API.Repositories.AdminAccountRepositories
{
    public interface ICustomerRepository
    {
        bool Add(Customer customer);
        bool Update(Customer customer);
        bool Delete(Customer customer);
        IEnumerable<Customer> GetAll();
        Customer GetByMobile(string mobile);
    }
}
