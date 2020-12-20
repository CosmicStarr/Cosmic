using CosmicApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicApi.Repository.IRepository
{
    public interface ICosmicSpotRepository
    {
        ICollection<CosmicSpotDTO> GetCosmicSpots();

        CosmicSpotDTO GetCosmicSpot(int Id);
        bool CosmicSpotExist(string name);
        bool CosmicSpotExist(int Id);
        bool CreateSpot(CosmicSpotDTO cosmicSpot);
        bool UpdateSpot(CosmicSpotDTO cosmicSpot);
        bool DeleteSpot(CosmicSpotDTO cosmicSpot);
        bool Save();
    }
}
