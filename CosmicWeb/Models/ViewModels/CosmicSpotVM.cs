using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicWeb.Models.ViewModels
{
    public class CosmicSpotVM
    {
        public IEnumerable<SelectListItem> CosmicSpotDropDown { get; set; }
        public Directions Directions { get; set; }
    }
}
