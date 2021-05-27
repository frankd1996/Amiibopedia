using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Amiibopedia.Helpers
{
    public class HttpHelper<T>
    {
        /// <summary>
        /// Clase genérica asíncrona que consume el servicio REST y que 
        /// retorna una colección del tipo genérico T
        /// </summary>
        /// <param name="serviceAdress"></param>
        /// <returns></returns>
        public async Task<T> GetRestServiceDataAsync(string serviceAdress)
        {
            var client = new HttpClient();

            //Obtiene o establece el intervalo de tiempo de espera antes de que
            //se agote el tiempo de espera de la solicitud
            client.Timeout = TimeSpan.FromSeconds(40);

            client.BaseAddress = new Uri(serviceAdress);
            var response = await client.GetAsync(client.BaseAddress);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;

        }
    }
}
