using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [Route("api/capitans/{capitanId}/boats")]
    [ApiController]
    public class BoatController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public BoatController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetBoatsWithHelpCapitan(Guid capitanId)
        {
            var capitan = _repository.Capitan.GetCapitanAsync(capitanId, trackChanges: false);
            if(capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatsFromDB = await _repository.Boat.GetBoatsAsync(capitanId, trackChanges: false);
            var boatsDto = _mapper.Map<IEnumerable<BoatDto>>(boatsFromDB);
            return Ok(boatsDto);
        }

        [HttpGet("{id}", Name = "GetBoatForCapitan")]
        public async Task<ActionResult> GetBoatWithHelpCapitan(Guid capitanId, Guid id)
        {
            var capitan = _repository.Capitan.GetCapitanAsync(capitanId, trackChanges: false);
            if (capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatDB = await _repository.Boat.GetBoatByIdAsync(capitanId,id, trackChanges: false);
            if(boatDB == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var boatDto = _mapper.Map<BoatDto>(boatDB);
            return Ok(boatDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoatForCapitanAsync(Guid capitanId, [FromBody] BoatForCreationDto boat)
        {
            if (boat == null)
            {
                _logger.LogError("BoatForCreationDto object sent from client is  null.");
                return BadRequest("BoatForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the BoatForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
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
        public async Task<IActionResult> DeleteBoatForCapitan(Guid capitanId, Guid id) 
        { 
            var capitan = await _repository.Capitan.GetCapitanAsync(capitanId, trackChanges: false);
            if(capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatForCapitan = await _repository.Boat.GetBoatByIdAsync(capitanId, id, trackChanges: false);
            if (boatForCapitan == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Boat.DeleteBoat(boatForCapitan);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoatForCapitan(Guid capitanId, Guid id, [FromBody] BoatForUpdateDto boat)
        {
            if (boat == null)
            {
                _logger.LogError("BoatForUpdateDto object sent from client is null.");
                return BadRequest("BoatForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the BoatForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var capitan = await _repository.Capitan.GetCapitanAsync(capitanId, trackChanges: false);
            if (capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatEntity = await _repository.Boat.GetBoatByIdAsync(capitanId, id, trackChanges: true);
            if (boatEntity == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(boat, boatEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateBoatForCapitan(Guid capitanId, Guid id, [FromBody] JsonPatchDocument<BoatForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var capitan =await _repository.Capitan.GetCapitanAsync(capitanId, trackChanges: false);
            if (capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatEntity = await _repository.Boat.GetBoatByIdAsync(capitanId, id, trackChanges: true);
            if (boatEntity == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
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
