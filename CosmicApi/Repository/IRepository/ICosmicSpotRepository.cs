using CosmicApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicApi.Repository.IRepository
{
    public interface ICosmicSpotRepository
    {
        ICollection<CosmicSpot> GetCosmicSpots();

        CosmicSpot GetCosmicSpot(int Id);
        bool CosmicSpotExist(string name);
        bool CosmicSpotExist(int Id);
        bool CreateSpot(CosmicSpot cosmicSpot);
        bool UpdateSpot(CosmicSpot cosmicSpot);
        bool DeleteSpot(CosmicSpot cosmicSpot);
        bool Save();
    }
}
