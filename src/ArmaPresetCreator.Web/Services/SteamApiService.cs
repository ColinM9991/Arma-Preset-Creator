using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ArmaPresetCreator.Web.Models;
using ArmaPresetCreator.Web.Models.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ArmaPresetCreator.Web.Services
{
    public class SteamApiService : ISteamApiService
    {
        private readonly HttpClient httpClient;
        private readonly SteamOptions steamOptions;
        private const string SteamRemoteStorageGetDetails = "IPublishedFileService/GetDetails/v1";

        public SteamApiService(IHttpClientFactory httpClientFactory, IOptions<SteamOptions> steamOptions)
        {
            this.httpClient = httpClientFactory.CreateClient(nameof(SteamApiService));
            this.steamOptions = steamOptions.Value;
        }

        public async Task<SteamApiResponse<SteamPublishedFileDto>> GetPublishedItemDetailsAsync(params long[] publishedFileIds)
        {
            var requestUrl = CreateRequestUrl(publishedFileIds);
            var response = await httpClient.GetAsync(requestUrl);
            return JsonConvert.DeserializeObject<SteamApiResponse<SteamPublishedFileDto>>(await response.Content.ReadAsStringAsync());
        }

        private string CreateRequestUrl(params long[] publishedFileIds)
        {
            var queryParameters = new Dictionary<string, string> {
                ["key"] = steamOptions.ApiKey,
                ["includechildren"] = "1",
                ["strip_description_bbcode"] = "1"
            };

            for(int i = 0; i < publishedFileIds.Length; i++)
            {
                queryParameters.Add($"publishedfileids[{i}]", publishedFileIds[i].ToString());
            }

            return $"{SteamRemoteStorageGetDetails}?{string.Join('&', queryParameters.Select(queryParameter => $"{queryParameter.Key}={queryParameter.Value}"))}";
        }
    }
}