using ArmaPresetCreator.Web.Models.DTO;
using System.Threading.Tasks;

namespace ArmaPresetCreator.Web.Services
{
    public interface ISteamApiService
    {
        Task<SteamApiResponse<SteamPublishedFileDto>> GetPublishedItemDetailsAsync(params long[] publishedFileIds);
    }
}
