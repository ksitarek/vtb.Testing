using Microsoft.AspNetCore.JsonPatch;
using RESTFulSense.Clients;
using System;
using System.Threading.Tasks;

namespace vtb.Testing.Rest
{
    public interface IRestClient : IRESTFulApiFactoryClient
    {
        ValueTask<Uri> PatchAsync<TRequest>(string relativeUrl,
            JsonPatchDocument<TRequest> content) where TRequest : class;

        ValueTask<TResponse> PostAsync<TRequest, TResponse>(string relativeUrl, TRequest content);

        ValueTask<Uri> PostAsync<TRequest>(string relativeUrl, TRequest content);

        ValueTask<Uri> PostAsync(string relativeUrl);

        ValueTask<Uri> PutAsync<TRequest>(string relativeUrl, TRequest content);

        ValueTask<Uri> PutAsync(string relativeUrl);
    }
}