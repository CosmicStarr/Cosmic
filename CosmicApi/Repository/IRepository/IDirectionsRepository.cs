using CosmicApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicApi.Repository.IRepository
{
    public interface IDirectionsRepository
    {
        ICollection<Directions> GetDirection();
        ICollection<Directions> GetDirectionsToCosmicSpot(int CosmicId);
        Directions GetDirection(int DirectId);
        bool DirectionExists(string name);
        bool DirectionExists(int id);
        bool CreateDirection(Directions Direction);
        bool UpdateDirection(Directions Direction);
        bool DeleteDirection(Directions Direction);
        bool Save();
    }
}
