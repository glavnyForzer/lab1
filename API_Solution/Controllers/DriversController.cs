using API_Solution.ModelBinders;
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
    public class CapitansController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CapitansController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
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

        [HttpGet("{id}", Name = "CapitanById")]
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

        [HttpPost]
        public IActionResult CreateCapitan([FromBody] CapitanForCreatonDto capitan) 
        {
            if(capitan == null)
            {
                _logger.LogError("CapitanForCreatonDto object sent from client is  null.");
                return BadRequest("CapitanForCreatonDto object is null");
            }
            var capitanEntity = _mapper.Map<Capitan>(capitan);
            _repository.Capitan.CreateCapitan(capitanEntity);
            _repository.Save();
            var capitanToReturn = _mapper.Map<CapitanDto>(capitanEntity);
            return CreatedAtRoute("CapitanById", new { id = capitanToReturn.Id }, capitanToReturn);
        }

        [HttpGet("collection/({ids})", Name = "CapitanCollection")]
        public IActionResult GetCapitanCollection(IEnumerable<Guid> ids) 
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var capitanEntities = _repository.Capitan.GetByIds(ids, trackChanges: false);
            if (ids.Count() != capitanEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var capitansToReturn = _mapper.Map<IEnumerable<CapitanDto>> (capitanEntities);
            return Ok(capitansToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateCapitanCollection([ModelBinder (BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> capitanCollection)
        {
            if (capitanCollection == null)
            {
                _logger.LogError("Capitan collection sent from client is null.");
                return BadRequest("Capitan collection is null");
            }
            var capitanEntitiees = _mapper.Map<IEnumerable<Capitan>>(capitanCollection);
            foreach (var capitan in capitanEntitiees)
            {
                _repository.Capitan.CreateCapitan(capitan);
            }
            _repository.Save();
            var capitanCollectionToReturn = _mapper.Map<IEnumerable<CapitanDto>>(capitanEntitiees);
            var ids = string.Join(",", capitanCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("CapitanCollection", new { ids }, capitanCollectionToReturn);
        }
    }
}
