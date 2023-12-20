﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [Route("api/capitans/{capitanId}/boats")]
    [ApiController]
    public class BoatsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public BoatsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetBoatsWithHelpCapitan(Guid capitanId)
        {
            var capitan = _repository.Capitan.GetCapitan(capitanId, trackChanges: false);
            if(capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatsFromDB = _repository.Boat.GetBoats(capitanId, trackChanges: false);
            var boatsDto = _mapper.Map<IEnumerable<BoatDto>>(boatsFromDB);
            return Ok(boatsDto);
        }

        [HttpGet("{id}")]
        public ActionResult GetBoatWithHelpCapitan(Guid capitanId, Guid id)
        {
            var capitan = _repository.Capitan.GetCapitan(capitanId, trackChanges: false);
            if (capitan == null)
            {
                _logger.LogInfo($"Capitan with id: {capitanId} doesn't exist in the database.");
                return NotFound();
            }
            var boatDB = _repository.Boat.GetBoatById(capitanId,id, trackChanges: false);
            if(boatDB == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var boatDto = _mapper.Map<BoatDto>(boatDB);
            return Ok(boatDto);
        }
    }
}
