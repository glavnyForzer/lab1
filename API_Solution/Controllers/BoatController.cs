using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [Route("api/drivers/{driverId}/boats")]
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
        public ActionResult GetBoatsWithHelpDriver(Guid driverId)
        {
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var boatsFromDB = _repository.Boat.GetBoats(driverId, trackChanges: false);
            var boatsDto = _mapper.Map<IEnumerable<BoatDto>>(boatsFromDB);
            return Ok(boatsDto);
        }

        [HttpGet("{id}", Name = "GetBoatForDriver")]
        public ActionResult GetBoatWithHelpDriver(Guid driverId, Guid id)
        {
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var boatDB = _repository.Boat.GetBoatById(driverId,id, trackChanges: false);
            if(boatDB == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var boatDto = _mapper.Map<BoatDto>(boatDB);
            return Ok(boatDto);
        }

        [HttpPost]
        public IActionResult CreateBoatForDriver(Guid driverId, [FromBody] BoatForCreationDto boat)
        {
            if (boat == null)
            {
                _logger.LogError("BoatForCreationDto object sent from client is  null.");
                return BadRequest("BoatForCreationDto object is null");
            }
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var boatEntity = _mapper.Map<Boat>(boat);
            _repository.Boat.CreateBoatForDriver(driverId,boatEntity);
            _repository.Save();
            var boatToReturn = _mapper.Map<BoatDto>(boatEntity);
            return CreatedAtRoute("GetBoatForDriver", new { driverId, id = boatToReturn.Id }, boatToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBoatForDriver(Guid driverId, Guid id) 
        { 
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var boatForDriver = _repository.Boat.GetBoatById(driverId, id, trackChanges: false);
            if (boatForDriver == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Boat.DeleteBoat(boatForDriver);
            _repository.Save();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBoatForDriver(Guid driverId, Guid id, [FromBody] BoatForUpdateDto boat)
        {
            if (boat == null)
            {
                _logger.LogError("BoatForUpdateDto object sent from client is null.");
                return BadRequest("BoatForUpdateDto object is null");
            }
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var boatEntity = _repository.Boat.GetBoatById(driverId, id, trackChanges: true);
            if (boatEntity == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(boat, boatEntity);
            _repository.Save();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateBoatForDriver(Guid driverId, Guid id, [FromBody] JsonPatchDocument<BoatForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var boatEntity = _repository.Boat.GetBoatById(driverId, id, trackChanges: true);
            if (boatEntity == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var boatToPatch = _mapper.Map<BoatForUpdateDto>(boatEntity);
            patchDoc.ApplyTo(boatToPatch);
            _mapper.Map(boatToPatch, boatEntity);
            _repository.Save();
            return NoContent(); 
        }
    }
}
