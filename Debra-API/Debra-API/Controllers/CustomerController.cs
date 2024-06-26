using AutoMapper;
using Debra_API.DTOs.AdminAccountDTOs;
using Debra_API.Entities;
using Debra_API.Repositories.AdminAccountRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Debra_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public ActionResult Create(CustomerDTO customerDTO)
        {
            var model = _mapper.Map<Customer>(customerDTO);

            if (_customerRepository.Add(model))
            {
                return Ok(model);
            }

            return BadRequest(model);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetAll()
        {
            var allCustomers = _customerRepository.GetAll();
            return Ok(allCustomers);
        }

        [HttpGet("ByMobile")]
        public ActionResult FindByMobile([FromQuery] string mobile)
        {
            var customer = _customerRepository.GetByMobile(mobile);

            if (customer is null)
            {
                var notFoundResponse = new CustomerOperationResultResponseDTO
                {
                    Status = "Not Found",
                    Customer = null
                };
                return NotFound(notFoundResponse);
            }

            return Ok(customer);
        }


        [HttpPut]
        public ActionResult update([FromQuery] string mobile, CustomerDTO customerDTO) 
        {
            Customer searchedCustomer = _customerRepository.GetByMobile(mobile);
            
            if (searchedCustomer is null)
            {
                var notFoundResponse = new CustomerOperationResultResponseDTO
                {
                    Status = "Not Found",
                    Customer = null
                };
                return NotFound(notFoundResponse);
            }

            searchedCustomer.Name = customerDTO.Name;
            searchedCustomer.Mobile = customerDTO.Mobile;
            searchedCustomer.Email = customerDTO.Email;

            Customer customer = _mapper.Map<Customer>(searchedCustomer);

            if (_customerRepository.Update(customer))
            {
                var successResponse = new CustomerOperationResultResponseDTO
                {
                    Status = "Success",
                    Customer = customerDTO
                };
                return Ok(successResponse);
            }

            var failedResponse = new CustomerOperationResultResponseDTO
            {
                Status = "Failed",
                Customer = customerDTO
            };
            return BadRequest(failedResponse);

        }

        [HttpDelete]
        public ActionResult delete([FromQuery] string mobile)
        {
            Customer searchedCustomer = _customerRepository.GetByMobile(mobile);

            if (searchedCustomer is null)
            {
                var notFoundResponse = new CustomerOperationResultResponseDTO
                {
                    Status = "Not Found",
                    Customer = null
                };
                return NotFound(notFoundResponse);
            }

            Customer customer = _mapper.Map<Customer>(searchedCustomer);

            if (_customerRepository.Delete(customer))
            {
                var successResponse = new CustomerOperationResultResponseDTO
                {
                    Status = "Deleted",
                    Customer = null
                };
                return Ok(successResponse);
            }

            var failedResponse = new CustomerOperationResultResponseDTO
            {
                Status = "Failed",
                Customer = customer
            };
            return BadRequest(failedResponse);
        }
    }
}
