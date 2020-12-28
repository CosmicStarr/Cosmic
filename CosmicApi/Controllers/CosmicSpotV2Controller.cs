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
    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "CosmicOpenApiSpec")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CosmicSpotV2Controller : Controller
    {
        private readonly ICosmicSpotRepository _spotRepository;
        private readonly IMapper _mapper;
        public CosmicSpotV2Controller(ICosmicSpotRepository spotRepository, IMapper mapper)
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
            var objformdb = _spotRepository.GetCosmicSpots().FirstOrDefault();

            return Ok(_mapper.Map<CosmicSpotDTO>(objformdb));
        }

    }
}
