using AutoMapper;
using CosmicApi.Models;
using CosmicApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicApi.CosmicApiMapper
{
    public class CosmicMappings:Profile
    {
        public CosmicMappings()
        {
            CreateMap<CosmicSpot, CosmicSpotDTO>().ReverseMap();
            CreateMap<Directions, CreateDirectionsDTO>().ReverseMap();
            CreateMap<Directions, UpdateDirectionsDTO>().ReverseMap();
            CreateMap<Directions, DirectionsDTO>().ReverseMap();

        }
    }
}
