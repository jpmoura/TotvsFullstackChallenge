using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Extensions.Http
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Tranforma o objeto de payload em uma representação em JSON
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de payload</typeparam>
        /// <param name="payload">Payload da requisição</param>
        /// <returns>Payload da requisição HTTP em JSON</returns>
        private static StringContent SerializePayload<T>(T payload)
        {
            string jsonPayload = JsonSerializer.Serialize(payload);
            return new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Cria a mensagem HTTP da requisição com o payload formatado em JSON
        /// </summary>
        /// <typeparam name="T">Tipo do payload</typeparam>
        /// <param name="url">URL da reuqisição</param>
        /// <param name="payload">Payload da reuqisição</param>
        /// <param name="method">Verbo da requisição</param>
        /// <returns>Mensagem de requisição HTTP com payload formatado em JSON</returns>
        private static HttpRequestMessage CreateHttpRequestMessage<T>(string url, T payload, HttpMethod method)
        {
            return new HttpRequestMessage
            {
                Content = SerializePayload(payload),
                Method = method,
                RequestUri = new Uri(url)
            };
        }

        /// <summary>
        /// Realiza uma chamada asíncrona com o verbo POST
        /// </summary>
        /// <typeparam name="T">Tipo do payload da reuqisição</typeparam>
        /// <param name="httpClient">Cliente HTTP</param>
        /// <param name="endpoint">URI da requisição</param>
        /// <param name="payload">Objeto de payload da requisição</param>
        /// <returns>Operação assíncrona</returns>
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string endpoint, T payload)
        {
            return httpClient.SendAsync(CreateHttpRequestMessage($"{httpClient.BaseAddress}{endpoint}", payload, HttpMethod.Post));
        }

        /// <summary>
        /// Realiza uma chamada asíncrona com o verbo POST
        /// </summary>
        /// <typeparam name="T">Tipo do payload da reuqisição</typeparam>
        /// <param name="httpClient">Cliente HTTP</param>
        /// <param name="endpoint">URI da requisição</param>
        /// <param name="payload">Objeto de payload da requisição</param>
        /// <returns>Operação assíncrona</returns>
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, string endpoint, T payload)
        {
            return httpClient.SendAsync(CreateHttpRequestMessage($"{httpClient.BaseAddress}{endpoint}", payload, HttpMethod.Put));
        }

        /// <summary>
        /// Tenta obter o conteúdo HTTP de JSON no formato  de um objeto tipado
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser construído a partir do JSON</typeparam>
        /// <param name="content">Conteúdo da mensagem HTTP</param>
        /// <returns>True se a conversão foi feita com sucesso e false caso contrário</returns>
        /// <remarks>Esse método foi construído somente para facilitar as asserções em projetos de testes. Não deve ser usado em código de produção</remarks>
        public static bool TryGetContentValue<T>(this HttpContent content, out T result) where T : class
        {
            bool wasSuccessful;

            try
            {
                string dataAsString = content.ReadAsStringAsync().Result;
                result = JsonSerializer.Deserialize<T>(dataAsString);
                wasSuccessful = true;
            }
            catch
            {
                wasSuccessful = false;
                result = null;
            }

            return wasSuccessful;
        }
    }
}
