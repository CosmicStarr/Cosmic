using CosmicWeb.Models;
using CosmicWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CosmicWeb.Repository
{
    public class DirectionRepository:Repository<Directions>,IDirectionsRepository
    {
        private readonly IHttpClientFactory _clientfactory;
        public DirectionRepository(IHttpClientFactory clientFactory):base(clientFactory)
        {
            _clientfactory = clientFactory;
        }
    }
}
