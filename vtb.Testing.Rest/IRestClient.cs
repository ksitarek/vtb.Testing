using Microsoft.AspNetCore.JsonPatch;
using RESTFulSense.Clients;
using System.Threading.Tasks;

namespace vtb.Testing.Rest
{
    public interface IRestClient : IRESTFulApiFactoryClient
    {
        ValueTask PatchAsync<TRequest>(string relativeUrl,
            JsonPatchDocument<TRequest> content) where TRequest : class;

        ValueTask<TResponse> PostAsync<TRequest, TResponse>(string relativeUrl, TRequest content);

        ValueTask PostAsync<TRequest>(string relativeUrl, TRequest content);

        ValueTask PostAsync(string relativeUrl);

        ValueTask PutAsync<TRequest>(string relativeUrl, TRequest content);

        ValueTask PutAsync(string relativeUrl);
    }
}