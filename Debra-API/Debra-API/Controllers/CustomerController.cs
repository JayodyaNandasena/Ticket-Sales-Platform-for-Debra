using AutoMapper;
using Debra_API.DTOs;
using Debra_API.DTOs.CustomerDTOs;
using Debra_API.Entities;
using Debra_API.Repositories.CustomerRepositories;
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

            if (_customerRepository.Add(model) != 0)
            {
				var okResponse = new OperationResultResponseDTO<CustomerDTO>
				{
					Status = Status.Success,
					Result = _mapper.Map<CustomerDTO>(model)
				};
				return Ok(okResponse);
            }

			var badResponse = new OperationResultResponseDTO<CustomerDTO>
			{
				Status = Status.Failed,
				Result = _mapper.Map<CustomerDTO>(model)
			};
			return Ok(badResponse);

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
                var notFoundResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Not Found"
				};
                return NotFound(notFoundResponse);
            }

			var okResponse = new OperationResultResponseDTO<CustomerDTO>
			{
				Status = Status.Success,
				Result = _mapper.Map<CustomerDTO>(customer)
			};
			return Ok(okResponse);
        }


        [HttpPut]
        public ActionResult update([FromQuery] string mobile, CustomerDTO customerDTO) 
        {
            Customer searchedCustomer = _customerRepository.GetByMobile(mobile);
            
            if (searchedCustomer is null)
            {
                var notFoundResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
					Result = "Not Found"
				};
                return NotFound(notFoundResponse);
            }

            searchedCustomer.Name = customerDTO.Name;
            searchedCustomer.Mobile = customerDTO.Mobile;
            searchedCustomer.Email = customerDTO.Email;

            Customer customer = _mapper.Map<Customer>(searchedCustomer);

            if (_customerRepository.Update(customer))
            {
                var successResponse = new OperationResultResponseDTO<CustomerDTO>
                {
                    Status = Status.Success,
					Result = customerDTO
                };
                return Ok(successResponse);
            }

            var failedResponse = new OperationResultResponseDTO<CustomerDTO>
            {
                Status = Status.Failed,
				Result = customerDTO
            };
            return BadRequest(failedResponse);

        }

        [HttpDelete]
        public ActionResult delete([FromQuery] string mobile)
        {
            Customer searchedCustomer = _customerRepository.GetByMobile(mobile);

            if (searchedCustomer is null)
            {
                var notFoundResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Not Found"
                };
                return NotFound(notFoundResponse);
            }

            CustomerDTO customer = _mapper.Map<CustomerDTO>(searchedCustomer);

            if (_customerRepository.Delete(searchedCustomer))
            {
                var successResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Success,
					Result = "Deleted"
				};
                return Ok(successResponse);
            }

            var failedResponse = new OperationResultResponseDTO<CustomerDTO>
            {
                Status = Status.Failed,
				Result = customer
            };
            return BadRequest(failedResponse);
        }
    }
}
