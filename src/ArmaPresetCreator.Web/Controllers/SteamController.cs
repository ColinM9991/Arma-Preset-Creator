using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ArmaPresetCreator.Web.Models;
using ArmaPresetCreator.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArmaPresetCreator.Web.Controllers
{
    /// <summary>
    /// Contains all of the Steam API operations.
    /// </summary>
    [ApiController]
    [Route("api/steam")]
    public class SteamController : ControllerBase
    {
        private readonly ISteamApiRepository steamApiRepository;
        private readonly ILogger<SteamController> logger;

        public SteamController(ISteamApiRepository steamApiRepository, ILogger<SteamController> logger)
        {
            this.steamApiRepository = steamApiRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Contacts the Steam Web API and retrieves the workshop item information including the children items..
        /// </summary>
        /// <param name="publishedItemId">The workshop item id.</param>
        /// <returns><see cref="SteamWorkshopItem"/> containing the item name and children items.</returns>
        /// <remarks>Produces a 400 Bad Request when any of the mandatory parameters are not supplied.</remarks>
        [HttpGet("workshop/publisheditems/{publishedItemId}")]
        [Produces("application/json")]
        [ProducesResponseType(400)]
        [ProducesResponseType(406)]
        [ProducesResponseType(typeof(SteamWorkshopItem), 200)]
        public async Task<IActionResult> GetWorkshopPublishedItemDetails([Required]long publishedItemId)
        {
            logger.LogInformation("Steam Workshop Item Request for {publishedItemId}", publishedItemId);

            var collectionDetails = await steamApiRepository.GetPublishedItemDetailsAsync(publishedItemId);
            if (collectionDetails == null)
            {
                return BadRequest();
            }

            return Ok(collectionDetails);
        }
    }
}