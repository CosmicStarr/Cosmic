using CosmicWeb.Models;
using CosmicWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CosmicWeb.Repository
{
    public class CosmicSpotRepository:Repository<CosmicSpot>,ICosmicSpotRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public CosmicSpotRepository(IHttpClientFactory clientFactory):base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
