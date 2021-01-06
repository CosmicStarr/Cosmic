using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CosmicWeb.Models;
using CosmicWeb.Models.ViewModels;
using CosmicWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CosmicWeb.Controllers
{
    public class DirectionsController : Controller
    {
        private readonly ICosmicSpotRepository _cosmicSpot;
        private readonly IDirectionsRepository _directions;
        public DirectionsController(ICosmicSpotRepository cosmicSpot,IDirectionsRepository directions)
        {
            _cosmicSpot = cosmicSpot;
            _directions = directions;
        }
        public IActionResult Index()
        {
            return View(new Directions() { });
        }

        public async Task<IActionResult> Upsert(int? Id)
        {
            IEnumerable<CosmicSpot> CosmicList = await _cosmicSpot.GetAllAsync(StaticDetails.CosmicSpotApiPath);

            CosmicSpotVM cosmicSpotVM = new CosmicSpotVM
            {
                CosmicSpotDropDown = CosmicList.Select(i => new SelectListItem 
                {
                    Text = i.Name,
                    Value = i.Id.ToString()            
                }),
                Directions = new Directions()
            };

            if (Id == null)
            {
                return View(cosmicSpotVM);
            }

            cosmicSpotVM.Directions = await _directions.GetAsync(StaticDetails.CosmicDirectionApiPath, Id.GetValueOrDefault());

            if (cosmicSpotVM.Directions == null)
            {
                return NotFound();
            }

            return View(cosmicSpotVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CosmicSpotVM obj)
        {
            if(ModelState.IsValid)
            {

                if(obj.Directions.Id == 0)
                {
                    await _directions.CreateAsync(StaticDetails.CosmicDirectionApiPath,obj.Directions);
                }
                else
                {
                    await _directions.UpdateAsync(StaticDetails.CosmicDirectionApiPath+obj.Directions.Id, obj.Directions);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<CosmicSpot> CosmicList = await _cosmicSpot.GetAllAsync(StaticDetails.CosmicSpotApiPath);

                CosmicSpotVM cosmicSpotVM = new CosmicSpotVM
                {
                    CosmicSpotDropDown = CosmicList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    Directions = obj.Directions
                };

                return View(cosmicSpotVM);
            }
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _directions.GetAllAsync(StaticDetails.CosmicDirectionApiPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var status = await _directions.DeleteAysnc(StaticDetails.CosmicDirectionApiPath,Id);
            if(status)
            {
                return Json(new { success = true, message = "Wonderful! Its gone." });
            }
            return Json(new { success = false, message = "Unlucky! Something went wrong." });
        }
    }
}
