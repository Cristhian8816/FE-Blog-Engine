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
using BlogService.Models;
using System;
using BLogService.Models;

namespace BlogClient.Services
{
    public static class BlogServiceExtensions
    {
        public static void AddBlogService(this IServiceCollection services, IConfiguration configuration)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient<IBlogService, BlogService>();
        }
    }

    /// <summary></summary>
    /// <seealso cref="BlogClient.Services.IBlogService" />
    public class BlogService : IBlogService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;
        private readonly string _TodoListScope = string.Empty;
        private readonly string _TodoListBaseAddress = string.Empty;
        private readonly ITokenAcquisition _tokenAcquisition;

        public BlogService(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _contextAccessor = contextAccessor;
            _TodoListScope = configuration["TodoList:TodoListScope"];
            _TodoListBaseAddress = configuration["TodoList:TodoListBaseAddress"];
        }
       
        public async Task<IEnumerable<Blogs>> GetAsync()
        {
            await PrepareAuthenticatedClient();

            var response = await _httpClient.GetAsync($"{ _TodoListBaseAddress}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();

                EntityResponseBlog<Blogs> Bloglist = JsonConvert.DeserializeObject<EntityResponseBlog<Blogs>>(content);

                Bloglist.Authorization = response.RequestMessage.Headers.Authorization.Parameter;

                return Bloglist.ResultData;
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
    }
}