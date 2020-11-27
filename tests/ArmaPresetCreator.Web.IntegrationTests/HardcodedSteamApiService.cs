using ArmaPresetCreator.Web.Models.DTO;
using ArmaPresetCreator.Web.Services;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace ArmaPresetCreator.Web.IntegrationTests
{
    public class HardcodedSteamApiService : ISteamApiService
    {
        private readonly IFileReader fileReader;

        public HardcodedSteamApiService(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public Task<SteamApiResponse<SteamPublishedFileDto>> GetPublishedItemDetailsAsync(params long[] publishedFileIds)
        {
            var steamPublishedFileDetails = publishedFileIds.Select(id => fileReader.Read(id.ToString())).Select(JsonConvert.DeserializeObject<SteamPublishedFileDetailDto>).ToList();

            var steamApiResponse = new SteamApiResponse<SteamPublishedFileDto>
            {
                Response = new SteamPublishedFileDto
                {
                    PublishedFilesDetails = steamPublishedFileDetails.ToArray(),
                    ResultCount = steamPublishedFileDetails.Count,
                    Result = steamPublishedFileDetails.Count > 0 ? 0 : 1
                }
            };

            return Task.FromResult(steamApiResponse);
        }
    }
}
