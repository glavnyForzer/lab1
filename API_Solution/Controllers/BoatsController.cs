using API_Solution.ActionFilters;
using API_Solution.ModelBinders;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_Solution.Controllers
{
    [Route("api/capitans/{capitanId}/boats")]
    [ApiController]
    public class BoatsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<BoatDto> _dataShaper;

        public BoatsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<BoatDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<ActionResult> GetBoatsWithHelpCapitan(Guid capitanId, [FromQuery] BoatParameters boatParameters)
        {
            var capitan = await _repository.Capitan.GetCapitanAsync(capitanId, trackChanges: false);
            if(capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatsFromDB = await _repository.Boat.GetBoatsAsync(capitanId, boatParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(boatsFromDB.MetaData));
            var boatsDto = _mapper.Map<IEnumerable<BoatDto>>(boatsFromDB);
            return Ok(_dataShaper.ShapeData(boatsDto, boatParameters.Fields));
        }

        [HttpGet("{id}", Name = "GetBoatForCapitan")]
        public async Task<ActionResult> GetBoatWithHelpCapitan(Guid capitanId, Guid id)
        {
            var capitan = await _repository.Capitan.GetCapitanAsync(capitanId, trackChanges: false);
            if (capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatDB = await _repository.Boat.GetBoatByIdAsync(capitanId, id, trackChanges: false);
            if(boatDB == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var boatDto = _mapper.Map<BoatDto>(boatDB);
            return Ok(boatDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateBoatForCapitanAsync(Guid capitanId, [FromBody] BoatForCreationDto boat)
        {           
            var capitan = await _repository.Capitan.GetCapitanAsync(capitanId, trackChanges: false);
            if(capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatEntity = _mapper.Map<Boat>(boat);
            _repository.Boat.CreateBoatForCapitan(capitanId,boatEntity);
            await _repository.SaveAsync();
            var boatToReturn = _mapper.Map<BoatDto>(boatEntity);
            return CreatedAtRoute("GetBoatForCapitan", new { capitanId, id = boatToReturn.Id }, boatToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateBoatForCapitanExistsAttribute))]
        public async Task<IActionResult> DeleteBoatForCapitan(Guid capitanId, Guid id) 
        {
            var boatForCapitan = HttpContext.Items["boat"] as Boat;            
            _repository.Boat.DeleteBoat(boatForCapitan);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateBoatForCapitanExistsAttribute))]
        public async Task<IActionResult> UpdateBoatForCapitan(Guid capitanId, Guid id, [FromBody] BoatForUpdateDto boat)
        {   
            var boatEntity = HttpContext.Items["boat"] as Boat;            
            _mapper.Map(boat, boatEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateBoatForCapitanExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateBoatForCapitan(Guid capitanId, Guid id, [FromBody] JsonPatchDocument<BoatForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }           
            var boatEntity = HttpContext.Items["boat"] as Boat;            
            var boatToPatch = _mapper.Map<BoatForUpdateDto>(boatEntity);
            patchDoc.ApplyTo(boatToPatch, ModelState);
            TryValidateModel(boatToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the BoatForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(boatToPatch, boatEntity);
            await _repository.SaveAsync();
            return NoContent(); 
        }
    }
}
