using CosmicApi.Data;
using CosmicApi.Models;
using CosmicApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicApi.Repository
{
    public class DirectionsRepository : IDirectionsRepository
    {
        private readonly ApplicationDbContext _db;
        public DirectionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateDirection(Directions Directions)
        {
            _db.GetDirections.Add(Directions);
            return Save();
        }

        public bool DeleteDirection(Directions Directions)
        {
            _db.GetDirections.Remove(Directions);
            return Save();
        }

        public Directions GetDirection(int DirectId)
        {
            return _db.GetDirections.Include(c => c.CosmicSpot).FirstOrDefault(a => a.Id == DirectId);
        }

        public ICollection<Directions> GetDirection()
        {
            return _db.GetDirections.Include(c => c.CosmicSpot).OrderBy(a => a.Name).ToList();
        }

        public bool DirectionExists(string name)
        {
            bool value = _db.GetDirections.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool DirectionExists(int id)
        {
            return _db.GetDirections.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateDirection(Directions Directions)
        {
            _db.GetDirections.Update(Directions);
            return Save();
        }

        public ICollection<Directions> GetDirectionsToCosmicSpot(int DirectId)
        {
            return _db.GetDirections.Include(c => c.CosmicSpot).Where(c => c.CosmicSpotId == DirectId).ToList();
        }
    }
}
