﻿using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using RESTFulSense.Clients;
using RESTFulSense.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace vtb.Testing.Rest
{
    public class RestClient : RESTFulApiFactoryClient, IRestClient
    {
        private readonly HttpClient _httpClient;

        public RestClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpRequestHeaders DefaultRequestHeaders { get => _httpClient.DefaultRequestHeaders; }

        public async ValueTask<Uri> PatchAsync<TPatchDoc>(string relativeUrl, JsonPatchDocument<TPatchDoc> content)
            where TPatchDoc : class
        {
            var contentString = StringifyJsonifyContent(content);

            var responseMessage =
                await _httpClient.PatchAsync(relativeUrl, contentString);

            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return responseMessage.Headers.Location;
        }

        public async ValueTask<TResponse> PostAsync<TRequest, TResponse>(string relativeUrl, TRequest content)
        {
            var contentString = StringifyJsonifyContent(content);

            var responseMessage =
                await _httpClient.PostAsync(relativeUrl, contentString);

            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return await DeserializeResponseContent<TResponse>(responseMessage);
        }

        public async ValueTask<Uri> PostAsync<TRequest>(string relativeUrl, TRequest content)
        {
            var contentString = StringifyJsonifyContent(content);

            var responseMessage =
                await _httpClient.PostAsync(relativeUrl, contentString);

            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return responseMessage.Headers.Location;
        }

        public async ValueTask<Uri> PostAsync(string relativeUrl)
        {
            var responseMessage =
                await _httpClient.PostAsync(relativeUrl, null);

            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return responseMessage.Headers.Location;
        }

        public async ValueTask<Uri> PutAsync<TRequest>(string relativeUrl, TRequest content)
        {
            var contentString = StringifyJsonifyContent(content);

            var responseMessage =
                await _httpClient.PutAsync(relativeUrl, contentString);

            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return responseMessage.Headers.Location;
        }

        public async ValueTask<Uri> PutAsync(string relativeUrl)
        {
            var responseMessage =
                await _httpClient.PutAsync(relativeUrl, null);

            await ValidationService.ValidateHttpResponseAsync(responseMessage);
            return responseMessage.Headers.Location;
        }

        private static async ValueTask<T> DeserializeResponseContent<T>(HttpResponseMessage responseMessage)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseString);
        }

        private static StringContent StringifyJsonifyContent<T>(T content)
        {
            var serializedRestrictionRequest = JsonConvert.SerializeObject(content);

            var contentString =
                new StringContent(serializedRestrictionRequest, Encoding.UTF8, "text/json");

            return contentString;
        }
    }
}