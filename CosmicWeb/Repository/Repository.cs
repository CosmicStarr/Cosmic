using CosmicWeb.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CosmicWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;
        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory; 
        }
        public async Task<bool> CreateAsync(string url, T objtoCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if(objtoCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objtoCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }
            var client = _clientFactory.CreateClient();
            HttpResponseMessage reponse = await client.SendAsync(request);
            if(reponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAysnc(string url, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url+Id);

            var client = _clientFactory.CreateClient();
            HttpResponseMessage reponse = await client.SendAsync(request);
            if(reponse.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _clientFactory.CreateClient();
            HttpResponseMessage reponse = await client.SendAsync(request);
            if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var JsonString = await reponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(JsonString);
            }
            return null;
        }

        public async Task<T> GetAsync(string url, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url+Id);

            var client = _clientFactory.CreateClient();
            HttpResponseMessage reponse = await client.SendAsync(request);
            if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var JsonString = await reponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(JsonString);
            }
            return null;

        }

        public async Task<bool> UpdateAsync(string url, T objtoUpdate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objtoUpdate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objtoUpdate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }
            var client = _clientFactory.CreateClient();
            HttpResponseMessage reponse = await client.SendAsync(request);
            if (reponse.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
