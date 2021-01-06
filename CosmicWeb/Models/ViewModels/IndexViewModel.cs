using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicWeb.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CosmicSpot> CosmicSpotsLists { get; set; }
        public IEnumerable<Directions> DirectionsLists { get; set; }
    }
}
