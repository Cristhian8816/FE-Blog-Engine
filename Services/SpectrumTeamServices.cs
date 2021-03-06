// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SpectrumTeamService.Models;
using System;

namespace SpectrumTeamClient.Services
{
    public static class SpectrumTeamServiceExtensions
    {
        public static void AddSpectrumTeamService(this IServiceCollection services, IConfiguration configuration)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient<ISpectrumTeamService, SpectrumTeamService>();
        }
    }

    /// <summary></summary>
    /// <seealso cref="SpectrumTeamClient.Services.ISpectrumTeamService" />
    public class SpectrumTeamService : ISpectrumTeamService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;
        private readonly string _TodoListScope = string.Empty;
        private readonly string _TodoListBaseAddress = string.Empty;
        private readonly ITokenAcquisition _tokenAcquisition;

        public SpectrumTeamService(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _contextAccessor = contextAccessor;
            _TodoListScope = configuration["TodoList:TodoListScope"];
            _TodoListBaseAddress = configuration["TodoList:TodoListBaseAddress"];
        }
       
        public async Task<IEnumerable<Languages>> GetAsync()
        {
            await PrepareAuthenticatedClient();

            var response = await _httpClient.GetAsync($"{ _TodoListBaseAddress}​api/BlogEngine​/Insert");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                EntityResponse<Languages> todolist = JsonConvert.DeserializeObject<EntityResponse<Languages>>(content);

                todolist.Authorization = response.RequestMessage.Headers.Authorization.Parameter;

                return todolist.ResultData;
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }       

        private async Task PrepareAuthenticatedClient()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _TodoListScope });
            Debug.WriteLine($"access token-{accessToken}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Languages> GetAsync(int id)
        {
            await PrepareAuthenticatedClient();
            //var endPoint = new Uri(string.Concat("http://localhost:57961​/api/Languages/GetById?Id=", id));

            var response = await _httpClient.GetAsync("http://localhost:57961​/api/Languages/GetById?Id=1");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                Languages languages = JsonConvert.DeserializeObject<Languages>(content);

                return languages;
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }

    }
}