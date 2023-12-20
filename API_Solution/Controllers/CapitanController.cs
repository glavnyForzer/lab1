using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [Route("api/capitans")]
    [ApiController]
    public class CapitanController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CapitanController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCapitans()
        {
            var capitans = _repository.Capitan.GetAllCapitans(trackChanges: false);
            var capitansDto = _mapper.Map<IEnumerable<Capitan>>(capitans);
            return Ok(capitansDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetCapitan(Guid id)
        {
            var capitan =_repository.Capitan.GetCapitan(id, trackChanges: false);
            if(capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var capitanDto = _mapper.Map<CapitanDto>(capitan);
            return Ok(capitanDto);
        }
    }
}
