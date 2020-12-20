using CosmicApi.Data;
using CosmicApi.Models;
using CosmicApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicApi.Repository
{
    public class CosmicSpotRepository : ICosmicSpotRepository
    {
        private readonly ApplicationDbContext _db;
        public CosmicSpotRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CosmicSpotExist(string name)
        {
            bool values = _db.GetCosmicSpots.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return values;
        }

        public bool CosmicSpotExist(int Id)
        {
            return _db.GetCosmicSpots.Any(a => a.Id == Id);
        }

        public bool CreateSpot(CosmicSpotDTO cosmicSpot)
        {
            _db.GetCosmicSpots.Add(cosmicSpot);
            return Save();
        }

        public bool DeleteSpot(CosmicSpotDTO cosmicSpot)
        {
            _db.GetCosmicSpots.Remove(cosmicSpot);
            return Save();
        }

        public CosmicSpotDTO GetCosmicSpot(int CosmicSpotId)
        {
            return _db.GetCosmicSpots.FirstOrDefault(g => g.Id == CosmicSpotId);
        }

        public ICollection<CosmicSpotDTO> GetCosmicSpots()
        {
            return _db.GetCosmicSpots.OrderBy(a => a.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateSpot(CosmicSpotDTO cosmicSpot)
        {
            _db.GetCosmicSpots.Update(cosmicSpot);
            return Save();
        }
    }
}
