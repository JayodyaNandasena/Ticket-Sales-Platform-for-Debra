using AutoMapper;
using Debra_API.DTOs;
using Debra_API.DTOs.AdminAccountDTOs;
using Debra_API.Entities;
using Debra_API.Repositories.AdminAccountRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Debra_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminAccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAdminAccountRepository _adminAccountRepository;

        public AdminAccountController(IMapper mapper, IAdminAccountRepository adminAccountRepository)
        {
            _mapper = mapper;
            _adminAccountRepository = adminAccountRepository;
        }

        [HttpPost]
        public ActionResult Create(AdminAccountDTO adminAccountDTO)
        {
            var model = _mapper.Map<AdminAccount>(adminAccountDTO);

            if (_adminAccountRepository.CreateAccount(model))
            {
				var okResponse = new OperationResultResponseDTO<AdminAccountDTO>
				{
					Status = Status.Success,
					Result = _mapper.Map<AdminAccountDTO>(model)
				};

				return Ok(okResponse);
            }

			var badResponse = new OperationResultResponseDTO<AdminAccountDTO>
			{
				Status = Status.Success,
				Result = _mapper.Map<AdminAccountDTO>(model)
			};

			return BadRequest(badResponse);
        }

        [HttpGet]
        public ActionResult<IEnumerable<AdminAccountDTO>> GetAll()
        {
            var allAdminAccount = _adminAccountRepository.GetAllAccounts();
            return Ok(allAdminAccount);
        }

        [HttpPost("ValidateLogin")]
        public ActionResult ValidateLogin(AdminAccountDTO adminAccountDTO)
        {
            var allAdminAccounts = _adminAccountRepository.GetAllAccounts();

            foreach (var adminAccount in allAdminAccounts)
            {
                if (adminAccountDTO.Username == adminAccount.Username &&
                    adminAccountDTO.Password == adminAccount.Password)
                {
                    var validAdminAccountDTO =
                        _mapper.Map<AdminAccountDTO>(adminAccount);

                    var okResponse = new OperationResultResponseDTO<AdminAccountDTO>
                    {
                        Status = Status.Success,
                        Result = validAdminAccountDTO
                    };

                    return Ok(okResponse);
                }
            }

            var failedResponse = new OperationResultResponseDTO<string>
            {
                Status = Status.Failed,
                Result = "Invalid Credentials"
            };
            return Unauthorized(failedResponse);
        }

        [HttpPut]
        public ActionResult updatePassword([FromQuery] string username, AdminUpdatePasswordDTO password) 
        {
            AdminAccount? adminAccount = _adminAccountRepository.GetAdminAccount(username);
            
            if (adminAccount is null)
            {
                var notFoundResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
					Result = "Not Found"
                };
                return NotFound(notFoundResponse);
            }

            adminAccount.Password = password.NewPassword;

            if (_adminAccountRepository.UpdateAccount(adminAccount))
            {
                var successResponse = new OperationResultResponseDTO<AdminAccountDTO>
                {
                    Status = Status.Success,
					Result = _mapper.Map<AdminAccountDTO>(adminAccount)
                };
                return Ok(successResponse);
            }

            var failedResponse = new OperationResultResponseDTO<AdminAccountDTO>
            {
                Status = Status.Failed,
				Result = _mapper.Map<AdminAccountDTO>(adminAccount)
            };
            return BadRequest(failedResponse);

        }
    }
}
