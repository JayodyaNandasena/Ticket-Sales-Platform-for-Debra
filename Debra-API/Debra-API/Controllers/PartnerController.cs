using AutoMapper;
using Debra_API.DTOs;
using Debra_API.DTOs.AdminAccountDTOs;
using Debra_API.DTOs.CustomerDTOs;
using Debra_API.DTOs.PartnerDTOs;
using Debra_API.Entities;
using Debra_API.Repositories.PartnerAccountRepositories;
using Debra_API.Repositories.PartnerRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Debra_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartnerController : Controller
    {
        private IMapper _mapper;
        private IPartnerRepository _partnerRepository;
        private IPartnerAccountRepository _partnerAccountRepository;

        public PartnerController(IMapper mapper,
            IPartnerRepository partnerRepository,
            IPartnerAccountRepository partnerAccountRepository)
        {
            _mapper = mapper;
            _partnerRepository = partnerRepository;
            _partnerAccountRepository = partnerAccountRepository;
        }


        [HttpPost]
        public IActionResult Add([FromBody] PartnerDTO partnerDTO)
        {
            //check for username duplication

            PartnerAccount partnerAccount = _partnerAccountRepository
                .findByUsername(partnerDTO.Account.Username);

            if (partnerAccount != null)
            {
                var usernameErrorResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Duplicate Username"
                };

                return BadRequest(usernameErrorResponse);
            }

            //check for email duplication

            if (_partnerRepository.GetByEmail(partnerDTO.Email) != null)
            {
                var emailErrorResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Duplicate Email"
                };
                return NotFound(emailErrorResponse);
            }

            //adding partner

            Partner partner = _mapper.Map<Partner>(partnerDTO);

            if (_partnerRepository.Add(partner))
            {
                var okResponse = new OperationResultResponseDTO<PartnerDTO>
                {
                    Status = Status.Success,
                    Result = partnerDTO
                };

                return Ok(okResponse);
            }

            return BadRequest(partner);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PartnerDTO>> GetAll()
        {
            IEnumerable<Partner> allPartners = _partnerRepository.GetAll();
            List<PartnerDTO> allPartnerDTOs = [];

            foreach (var partner in allPartners)
            {
                PartnerDTO mappedPartnerDTO = _mapper.Map<PartnerDTO>(partner);
                allPartnerDTOs.Add(mappedPartnerDTO);
            }

            return Ok(allPartnerDTOs);
        }

        [HttpGet("ByEmail")]
        public ActionResult GetByEmail([FromQuery] string email)
        {
            var partner = _partnerRepository.GetByEmail(email);

            if (partner is null)
            {
                var notFoundResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Not Found"
                };
                return NotFound(notFoundResponse);
            }

            PartnerDTO partnerDTO = _mapper.Map<PartnerDTO>(partner);

            var foundResponse = new OperationResultResponseDTO<PartnerDTO>
            {
                Status = Status.Success,
                Result = partnerDTO
            };

            return Ok(foundResponse);
        }

        [HttpPost("ValidateLogin")]
        public ActionResult ValidateLogin(PartnerAccountDTO partnerAccountDTO)
        {
            var partnerAccount = _partnerAccountRepository.findByUsername(
                partnerAccountDTO.Username);

            if (partnerAccount is null)
            {
                var notFoundResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Not Found"
                };
                return NotFound(notFoundResponse);
            }

            if (partnerAccount.Password == partnerAccountDTO.Password)
            {
                Partner partner = _partnerRepository.GetById(partnerAccount.Partner.Id);

                PartnerDTO partnerDTO = _mapper.Map<PartnerDTO>(partner);

                var response = new OperationResultResponseDTO<PartnerDTO>
                {
                    Status = Status.Success,
                    Result = partnerDTO
                };

                return Ok(response);
            }

            return Unauthorized("Invalid Credentials");
        }

        [HttpPut("UpdatePassword")]
        public ActionResult updatePassword([FromQuery] string username,
            PartnerAccountDTO newAccount)
        {
            PartnerAccount partnerAccount = _partnerAccountRepository
                .findByUsername(username);

            if (partnerAccount is null)
            {
                var notFoundResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Not Found"
                };
                return NotFound(notFoundResponse);
            }

            partnerAccount.Password = newAccount.Password;

            if (_partnerAccountRepository.Update(partnerAccount))
            {
                var successResponse = new OperationResultResponseDTO<PartnerAccountDTO>
                {
                    Status = Status.Success,
                    Result = _mapper.Map<PartnerAccountDTO>(partnerAccount)
                };
                return Ok(successResponse);
            }

            var failedResponse = new OperationResultResponseDTO<PartnerAccountDTO>
            {
                Status = Status.Failed,
                Result = newAccount
            };
            return BadRequest(failedResponse);

        }


        //------ ERROR ------    

        [HttpPut]
        public ActionResult update([FromQuery] int id, PartnerDTO newPartner)
        {

            //check whether the partner exists
            Partner partner = _partnerRepository.GetById(id);

            if (partner is null)
            {
                var notFoundResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Not Found"
                };
                return NotFound(notFoundResponse);
            }

            //check for email duplication

            Partner byEmail = _partnerRepository.GetByEmail(newPartner.Email);

            if (byEmail != null)
            {
                if (byEmail.Id != partner.Id)
                {
                    var emailErrorResponse = new OperationResultResponseDTO<string>
                    {
                        Status = Status.Failed,
                        Result = "Duplicate Email"
                    };
                    return BadRequest(emailErrorResponse);
                }
            }

            //check for username duplication


            PartnerAccount byUsername = _partnerAccountRepository.findByUsername(newPartner.Account.Username);
            
            if (byUsername != null)
            {
                if (byUsername.Partner.Id != partner.Id)
                {
                    var usernameErrorResponse = new OperationResultResponseDTO<string>
                    {
                        Status = Status.Failed,
                        Result = "Duplicate Username"
                    };
                    return BadRequest(usernameErrorResponse);

                }
            }            

            //updating the partner

            Partner mappedPartner = _mapper.Map<Partner>(newPartner);

            if (_partnerRepository.Update(mappedPartner))
            {
                var successResponse = new OperationResultResponseDTO<PartnerDTO>
                {
                    Status = Status.Success,
                    Result = newPartner
                };
                return Ok(successResponse);
            }

            /*var failedResponse = new OperationResultResponseDTO<PartnerDTO>
            {
                Status = "Failed",
                Result = newPartner
            };
            return BadRequest(failedResponse);*/

            return BadRequest();

        }

        [HttpDelete]
        public ActionResult delete([FromQuery] int id)
        {

            //check whether the partner exists
            Partner partner = _partnerRepository.GetById(id);

            if (partner is null)
            {
                var notFoundResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Not Found"
                };
                return NotFound(notFoundResponse);
            }

            //deleting the partner

            if (_partnerRepository.Delete(partner))
            {
                var successResponse = new OperationResultResponseDTO<string>
                {
                    Status = Status.Success,
                    Result = "Deleted"
                };
                return Ok(successResponse);
            }

            var failedResponse = new OperationResultResponseDTO<PartnerDTO>
            {
                Status = Status.Failed,
                Result = _mapper.Map<PartnerDTO>(partner)
            };
            return BadRequest(failedResponse);

        }


    }
}

/*
 {
  "name": "Emilia White",
  "registeredDate": "2024-06-27T06:36:09.331Z",
  "type": "Individual",
  "email": "emiliawhite@yahoomail.com",
  "account": {
    "username": "Emilia",
    "password": "Emilia"
  }
}
*/
