using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        /// <param name="publishedItemIds">The workshop item id.</param>
        /// <returns><see cref="SteamWorkshopItem"/> containing the item name and children items.</returns>
        /// <remarks>Produces a 400 Bad Request when any of the mandatory parameters are not supplied.</remarks>
        [HttpPost("workshop/publisheditems/batch")]
        [Produces("application/json")]
        [ProducesResponseType(400)]
        [ProducesResponseType(406)]
        [ProducesResponseType(typeof(SteamWorkshopItem), 200)]
        public async Task<IActionResult> GetWorkshopPublishedItemDetails([Required] BatchSteamWorkshopRequest model)
        {
            var collectionDetails = await steamApiRepository.GetPublishedItemsDetailsAsync(model.WorkshopItemIds);
            if (collectionDetails == null)
            {
                return BadRequest("Invalid workshop item id");
            }

            return Ok(collectionDetails);
        }
    }
}