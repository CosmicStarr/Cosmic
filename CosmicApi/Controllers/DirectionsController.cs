using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CosmicApi.Models;
using CosmicApi.Models.DTOs;
using CosmicApi.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CosmicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "CosmicOpenApiSpecDirections")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class DirectionsController : Controller
    {
        private readonly IDirectionsRepository _directRepository;
        private readonly IMapper _mapper;
        public DirectionsController(IDirectionsRepository DirectRepository, IMapper mapper)
        {
            _directRepository = DirectRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get list of all Directions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<DirectionsDTO>))]
        public IActionResult GetAllDirections()
        {
            var objformdb = _directRepository.GetDirection();
            var objDto = new List<DirectionsDTO>();
            foreach (var item in objformdb)
            {
                objDto.Add(_mapper.Map<DirectionsDTO>(item));
            }
            return Ok(objDto);
        }
        /// <summary>
        /// Get a single Direction.
        /// </summary>
        /// <param name="DirectId">Id of the Direction</param>
        /// <returns></returns>
        [HttpGet("{DirectId:int}", Name ="GetDirection")]
        [ProducesResponseType(200, Type = typeof(DirectionsDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetDirection(int DirectId)
        {
            var ojb = _directRepository.GetDirection(DirectId);
            if(ojb == null)
            {
                return NotFound();
            }
            var objdto = _mapper.Map<DirectionsDTO>(ojb);
            return Ok(objdto);
        }
        /// <summary>
        /// create a new Direction.
        /// </summary>
        /// <param name="DirectDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(DirectionsDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] CreateDirectionsDTO DirectDTO)
        {
            if (DirectDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (_directRepository.DirectionExists(DirectDTO.Name))
            {
                ModelState.AddModelError("", "Trail Exists!");
                return StatusCode(404, ModelState);
            }
            var trailObj = _mapper.Map<Directions>(DirectDTO);
            if (!_directRepository.CreateDirection(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetDirection", new { trailId = trailObj.Id }, trailObj);
        }
        /// <summary>
        /// Update a Direction
        /// </summary>
        /// <param name="DirectionsId"></param>
        /// <param name="DirectDTO"></param>
        /// <returns></returns>
        [HttpPatch("{DirectionsId:int},", Name = "UpdateDirection")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateDirection(int DirectionsId, [FromBody] UpdateDirectionsDTO DirectDTO)
        {
            if(DirectDTO == null || DirectionsId != DirectDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var cosmicDTO = _mapper.Map<Directions>(DirectDTO);

            if(!_directRepository.UpdateDirection(cosmicDTO))
            {
                ModelState.AddModelError("", $"{cosmicDTO.Name} was not Updated! Something went wrong on server side!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        /// <summary>
        /// Delete a Direction
        /// </summary>
        /// <param name="DirectId"></param>
        /// <returns></returns>
        [HttpDelete("{DirectId:int},", Name = "DeleteDirection")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteDirection(int DirectId)
        {
            if(!_directRepository.DirectionExists(DirectId))
            {
                return NotFound();
            }

            var objfromdb = _directRepository.GetDirection(DirectId);

            if (!_directRepository.DeleteDirection(objfromdb))
            {
                ModelState.AddModelError("", $"{objfromdb.Name} was not Deleted! Something went wrong on server side!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
