using Microsoft.AspNetCore.JsonPatch;
using RESTFulSense.Clients;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace vtb.Testing.Rest
{
    public interface IRestClient : IRESTFulApiFactoryClient
    {
        HttpRequestHeaders DefaultRequestHeaders { get; }

        ValueTask<TResponse> PostAsync<TRequest, TResponse>(string relativeUrl, TRequest content);

        ValueTask PostAsync<TRequest>(string relativeUrl, TRequest content);

        ValueTask PatchAsync<TRequest>(string relativeUrl,
            JsonPatchDocument<TRequest> content) where TRequest : class;
    }
}