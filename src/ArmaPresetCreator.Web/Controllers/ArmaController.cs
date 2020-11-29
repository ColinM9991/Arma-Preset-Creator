using ArmaPresetCreator.Web.Models;
using ArmaPresetCreator.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArmaPresetCreator.Web.Controllers
{
    /// <summary>
    /// Contains all of the Arma API operations.
    /// </summary>
    [ApiController]
    [Route("api/arma")]
    public class ArmaController : Controller
    {
        private readonly IArmaPresetRequestCreator armaPresetRequestCreator;
        private readonly ILogger<ArmaController> logger;

        /// <summary>
        /// Initializes an instance of <see cref="ArmaController"/>.
        /// </summary>
        /// <param name="armaPresetRequestCreator"></param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public ArmaController(IArmaPresetRequestCreator armaPresetRequestCreator, ILogger<ArmaController> logger)
        {
            this.armaPresetRequestCreator = armaPresetRequestCreator;
            this.logger = logger;
        }

        /// <summary>
        /// Generates an Arma launcher preset file with the specified <paramref name="steamWorkshopItem"/>.
        /// </summary>
        /// <param name="steamWorkshopItem"><see cref="SteamWorkshopItem"/> containing the Steam published item details.</param>
        /// <returns>The launcher HTML preset.</returns>
        /// <remarks>Produces a 400 Bad Request if any of the mandatory parameters are not supplied.</remarks>
        [HttpPost("preset/generate")]
        [Produces("text/html", "application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(406)]
        public IActionResult Generate([FromBody]SteamWorkshopItem steamWorkshopItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            logger.LogInformation("Preset Generation Request for {@steamWorkshopItem}", steamWorkshopItem);

            try
            {
                var armaPresetRequest = armaPresetRequestCreator.Create(steamWorkshopItem);
                return View("/Pages/PresetTemplate.cshtml", armaPresetRequest);
            }
            catch (UnsupportedWorkshopItemException)
            {
                return BadRequest("Only mods, missions and collections with dependencies are supported");
            }
        }
    }
}