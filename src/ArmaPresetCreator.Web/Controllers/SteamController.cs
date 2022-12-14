using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ArmaPresetCreator.Web.Models;
using ArmaPresetCreator.Web.Services;
using Microsoft.AspNetCore.Mvc;

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

        public SteamController(ISteamApiRepository steamApiRepository)
        {
            this.steamApiRepository = steamApiRepository;
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
        public async Task<IActionResult> GetWorkshopPublishedItemDetails([Required] long publishedItemId)
        {
            var collectionDetails = await steamApiRepository.GetPublishedItemDetailsAsync(publishedItemId);
            if (collectionDetails == null)
            {
                return BadRequest("Invalid workshop item id");
            }

            return Ok(collectionDetails);
        }
    }
}