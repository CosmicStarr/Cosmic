using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmicWeb.Models;
using CosmicWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;


namespace CosmicWeb.Controllers
{
    public class CosmicSpotController : Controller
    {
        private readonly ICosmicSpotRepository _cosmicSpot;
        public CosmicSpotController(ICosmicSpotRepository cosmicSpot)
        {
            _cosmicSpot = cosmicSpot;
        }
        public IActionResult Index()
        {
            return View(new CosmicSpot() { });
        }

        public async Task<IActionResult> Upsert(int? Id)
        {
            CosmicSpot obj = new CosmicSpot();
            if (Id == null)
            {
                return View(obj);
            }

            obj = await _cosmicSpot.GetAsync(StaticDetails.CosmicSpotApiPath, Id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _cosmicSpot.GetAllAsync(StaticDetails.CosmicSpotApiPath) });
        }
    }
}
