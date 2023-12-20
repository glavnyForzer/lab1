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
        public async Task<IActionResult> GetCapitans()
        {
            var capitans = await _repository.Capitan.GetAllCapitansAsync(trackChanges: false);
            var capitansDto = _mapper.Map<IEnumerable<Capitan>>(capitans);
            return Ok(capitansDto);
        }

        [HttpGet("{id}", Name = "CapitanById")]
        public async Task<IActionResult> GetCapitanAsync(Guid id)
        {
            var capitan = await _repository.Capitan.GetCapitanAsync(id, trackChanges: false);
            if(capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var capitanDto = _mapper.Map<CapitanDto>(capitan);
            return Ok(capitanDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCapitanAsync([FromBody] CapitanForCreatonDto capitan) 
        {
            if(capitan == null)
            {
                _logger.LogError("CapitanForCreatonDto object sent from client is  null.");
                return BadRequest("CapitanForCreatonDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the BoatForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var capitanEntity = _mapper.Map<Capitan>(capitan);
            _repository.Capitan.CreateCapitan(capitanEntity);
            await _repository.SaveAsync();
            var capitanToReturn = _mapper.Map<CapitanDto>(capitanEntity);
            return CreatedAtRoute("CapitanById", new { id = capitanToReturn.Id }, capitanToReturn);
        }

        [HttpGet("collection/({ids})", Name = "CapitanCollection")]
        public async Task<IActionResult> GetCapitanCollection(IEnumerable<Guid> ids) 
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var capitanEntities = await _repository.Capitan.GetByIdsAsync(ids, trackChanges: true);
            if (ids.Count() != capitanEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var capitansToReturn = _mapper.Map<IEnumerable<CapitanDto>> (capitanEntities);
            return Ok(capitansToReturn);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCapitanCollection([ModelBinder (BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> capitanCollection)
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
            await _repository.SaveAsync();
            var capitanCollectionToReturn = _mapper.Map<IEnumerable<CapitanDto>>(capitanEntitiees);
            var ids = string.Join(",", capitanCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("CapitanCollection", new { ids }, capitanCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCapitan(Guid id)
        {
            var capitan = _repository.Capitan.GetCapitanAsync(id, trackChanges: false);
            if(capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Capitan.DeleteCapitan(capitan.Result);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CapitanForUpdateDto capitan)
        {
            if (capitan == null)
            { 
                _logger.LogError("CapitanForUpdateDto object sent from client is null.");
                return BadRequest("CapitanForUpdateDto object is null");
            }
            var capitanEntity =await _repository.Capitan.GetCapitanAsync(id, trackChanges: true);
            if (capitanEntity == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(capitan, capitanEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
