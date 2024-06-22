using AutoMapper;
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
                return Ok(model);
            }

            return BadRequest(model);
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

                    var response = new AdminOperationResultResponseDTO
                    {
                        Status = "Success",
                        Admin = validAdminAccountDTO
                    };

                    return Ok(response);
                }
            }

            var failedResponse = new AdminOperationResultResponseDTO
            {
                Status = "Failed",
                Admin = null
            };
            return Unauthorized(failedResponse);
        }

        [HttpPut("(username)", Name = "updatePassword")]
        public ActionResult updatePassword(string username, AdminUpdatePasswordDTO password) 
        {
            AdminAccount adminAccount = _adminAccountRepository.GetAdminAccount(username);
            
            if (adminAccount is null)
            {
                var notFoundResponse = new AdminOperationResultResponseDTO
                {
                    Status = "Failed",
                    Admin = null
                };
                return NotFound(notFoundResponse);
            }

            adminAccount.Password = password.NewPassword;

            if (_adminAccountRepository.UpdateAccount(adminAccount))
            {
                var successResponse = new AdminOperationResultResponseDTO
                {
                    Status = "Success",
                    Admin = _mapper.Map<AdminAccountDTO>(adminAccount)
                };
                return Ok(successResponse);
            }

            var failedResponse = new AdminOperationResultResponseDTO
            {
                Status = "Failed",
                Admin = _mapper.Map<AdminAccountDTO>(adminAccount)
            };
            return BadRequest(failedResponse);

        }
    }
}
