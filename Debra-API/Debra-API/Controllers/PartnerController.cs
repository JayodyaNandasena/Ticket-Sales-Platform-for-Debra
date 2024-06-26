using AutoMapper;
using Debra_API.DTOs.PartnerDTOs;
using Debra_API.Entities;
using Debra_API.Repositories.PartnerRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Debra_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartnerController : Controller
    {
        private IMapper _mapper;
        private IPartnerRepository _repository;

        public PartnerController(IMapper mapper, IPartnerRepository repository)
        { 
            _mapper = mapper;
            _repository = repository;
        }


        [HttpPost]
        public IActionResult Add(PartnerDTO partnerDTO)
        {
            partnerDTO.Id = _repository.generateNextId();

            Partner partner = _mapper.Map<Partner>(partnerDTO);

            if (_repository.Add(partner))
            {
                return Ok(partner);
            }

            return BadRequest(partner);
        }
    }
}
