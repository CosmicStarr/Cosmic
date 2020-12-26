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
    public class DirectionsV2Controller : Controller
    {
        private readonly IDirectionsRepository _directRepository;
        private readonly IMapper _mapper;
        public DirectionsV2Controller(IDirectionsRepository DirectRepository, IMapper mapper)
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

    }
}
