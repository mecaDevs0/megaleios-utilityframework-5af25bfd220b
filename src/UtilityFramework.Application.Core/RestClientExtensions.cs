using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using RestSharp;
using RestSharp.Extensions;

namespace UtilityFramework.Application.Core
{

    public static class RestClientExtensions
    {

        /// <summary>
        /// EXCECUTE WITHOUT DESERIALIZE OBJECT
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static RestResponse Execute(this RestClient client, RestRequest request)
        {
            var taskCompletion = new TaskCompletionSource<IRestResponse>();
            var handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            return (RestResponse)taskCompletion.Task.Result;
        }

        /// <summary>
        /// EXCECUTE WITHOUT DESERIALIZE OBJECT
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<RestResponse> ExecuteAsync(this RestClient client, RestRequest request)
        {
            var taskCompletion = new TaskCompletionSource<IRestResponse>();
            var handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            return (RestResponse)await taskCompletion.Task;
        }

        /// <summary>
        /// EXCECUTE WITH DESERIALIZE OBJECT
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IRestResponse<T> Execute<T>(this RestClient client, RestRequest request)
        {
            var taskCompletion = new TaskCompletionSource<IRestResponse>();
            var handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            var result = (RestResponse)taskCompletion.Task.Result;


            var response = result.ToAsyncResponse<T>();

            try
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                };

                response.Data = JsonConvert.DeserializeObject<T>(result.Content, settings /*settings*/);
            }
            catch
            {
                //unused
            }
            return response;
        }

        /// <summary>
        /// EXCECUTE WITH DESERIALIZE OBJECT
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<IRestResponse<T>> ExecuteAsync<T>(this RestClient client, RestRequest request)
        {
            var taskCompletion = new TaskCompletionSource<IRestResponse>();
            var handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            var result = (RestResponse)await taskCompletion.Task;


            var response = result.ToAsyncResponse<T>();

            try
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                };

                response.Data = JsonConvert.DeserializeObject<T>(result.Content, settings /*settings*/);
            }
            catch
            {
                //unused
            }
            return response;
        }
    }
}