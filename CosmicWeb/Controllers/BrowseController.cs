using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmicWeb.Models;
using CosmicWeb.Models.ViewModels;
using CosmicWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CosmicWeb.Controllers
{
    public class BrowseController : Controller
    {
        private readonly ICosmicSpotRepository _spot;
        private readonly IDirectionsRepository _directs;
        public BrowseController(ICosmicSpotRepository cosmicSpotRepository, IDirectionsRepository directionsRepository)
        {
            _directs = directionsRepository;
            _spot = cosmicSpotRepository;
        }
        public async Task<IActionResult> Index()
        {
            IndexViewModel ListOfSpots = new IndexViewModel
            {
                CosmicSpotsLists = await _spot.GetAllAsync(StaticDetails.CosmicSpotApiPath),
                DirectionsLists = await _directs.GetAllAsync(StaticDetails.CosmicDirectionApiPath),
            };
            return View(ListOfSpots);
        }
    }
}
