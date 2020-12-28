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
    [Route("api/v{version:apiVersion}/cosmicspot")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "CosmicOpenApiSpec")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CosmicSpotController : Controller
    {
        private readonly ICosmicSpotRepository _spotRepository;
        private readonly IMapper _mapper;
        public CosmicSpotController(ICosmicSpotRepository spotRepository, IMapper mapper)
        {
            _spotRepository = spotRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get list of all spots.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<CosmicSpotDTO>))]
        public IActionResult GetAllSpots()
        {
            var objformdb = _spotRepository.GetCosmicSpots();
            var objDto = new List<CosmicSpotDTO>();
            foreach (var item in objformdb)
            {
                objDto.Add(_mapper.Map<CosmicSpotDTO>(item));
            }
            return Ok(objDto);
        }
        /// <summary>
        /// Get a single spot.
        /// </summary>
        /// <param name="CosmicSpotId">Id of the Spot</param>
        /// <returns></returns>
        [HttpGet("{CosmicSpotId:int}", Name ="GetSpot")]
        [ProducesResponseType(200, Type = typeof(CosmicSpotDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetSpot(int CosmicSpotId)
        {
            var ojb = _spotRepository.GetCosmicSpot(CosmicSpotId);
            if(ojb == null)
            {
                return NotFound();
            }
            var objdto = _mapper.Map<CosmicSpotDTO>(ojb);
            return Ok(objdto);
        }
        /// <summary>
        /// create a new spot.
        /// </summary>
        /// <param name="cosmicSpotDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CosmicSpotDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateSpot([FromBody] CosmicSpotDTO cosmicSpotDTO)
        {
            if(cosmicSpotDTO == null)
            {
                return BadRequest(ModelState);
            }
            if(_spotRepository.CosmicSpotExist(cosmicSpotDTO.Name))
            {
                ModelState.AddModelError("", "CosmicSpot Exist in the system!");
                return StatusCode(404, ModelState);
            }

            var cosmicDTO = _mapper.Map<CosmicSpot>(cosmicSpotDTO);

            if(!_spotRepository.CreateSpot(cosmicDTO))
            {
                ModelState.AddModelError("", $"{cosmicDTO.Name} was not created! Something went wrong on server side!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetSpot",new { Version=HttpContext.GetRequestedApiVersion().ToString(), CosmicSpotId = cosmicDTO.Id },cosmicDTO);
        }
        /// <summary>
        /// Update a spot
        /// </summary>
        /// <param name="CosmicSpotId"></param>
        /// <param name="cosmicSpotDTO"></param>
        /// <returns></returns>
        [HttpPatch("{CosmicSpotId:int},", Name = "UpdateSpot")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateSpot(int CosmicSpotId, [FromBody] CosmicSpotDTO cosmicSpotDTO)
        {
            if(cosmicSpotDTO == null || CosmicSpotId != cosmicSpotDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var cosmicDTO = _mapper.Map<CosmicSpot>(cosmicSpotDTO);

            if(!_spotRepository.UpdateSpot(cosmicDTO))
            {
                ModelState.AddModelError("", $"{cosmicDTO.Name} was not Updated! Something went wrong on server side!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        /// <summary>
        /// Delete a spot
        /// </summary>
        /// <param name="CosmicSpotId"></param>
        /// <returns></returns>
        [HttpDelete("{CosmicSpotId:int},", Name = "DeleteSpot")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteSpot(int CosmicSpotId)
        {
            if(!_spotRepository.CosmicSpotExist(CosmicSpotId))
            {
                return NotFound();
            }

            var objfromdb = _spotRepository.GetCosmicSpot(CosmicSpotId);

            if (!_spotRepository.DeleteSpot(objfromdb))
            {
                ModelState.AddModelError("", $"{objfromdb.Name} was not Deleted! Something went wrong on server side!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
