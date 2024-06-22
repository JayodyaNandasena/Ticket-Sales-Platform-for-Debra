using AutoMapper;
using Debra_API.DTO;
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

        //validate login
        //change username
        //change password

    }
}
