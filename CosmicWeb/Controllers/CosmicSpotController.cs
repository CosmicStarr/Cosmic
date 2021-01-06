using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CosmicSpot obj)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count >0)
                {
                    byte[] p1 = null;
                    using(var fileStream1 = files[0].OpenReadStream())
                    {
                        using var memoryStream1 = new MemoryStream();
                        fileStream1.CopyTo(memoryStream1);
                        p1 = memoryStream1.ToArray();
                    }
                    obj.Images = p1;
                }
                else
                {
                    var objfromdb = await _cosmicSpot.GetAsync(StaticDetails.CosmicSpotApiPath, obj.Id);
                    obj.Images = objfromdb.Images;
                }

                if(obj.Id == 0)
                {
                    await _cosmicSpot.CreateAsync(StaticDetails.CosmicSpotApiPath, obj);
                }
                else
                {
                    await _cosmicSpot.UpdateAsync(StaticDetails.CosmicSpotApiPath+obj.Id, obj);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _cosmicSpot.GetAllAsync(StaticDetails.CosmicSpotApiPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var status = await _cosmicSpot.DeleteAysnc(StaticDetails.CosmicSpotApiPath,Id);
            if(status)
            {
                return Json(new { success = true, message = "Wonderful! Its gone." });
            }
            return Json(new { success = false, message = "Unlucky! Something went wrong." });
        }
    }
}
