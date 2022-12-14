using ArmaPresetCreator.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArmaPresetCreator.Web.Controllers
{
    /// <summary>
    /// Contains all of the Arma API operations.
    /// </summary>
    [ApiController]
    [Route("api/arma")]
    public class ArmaController : Controller
    {
        /// <summary>
        /// Generates an Arma launcher preset file with the specified <paramref name="request"/>.
        /// </summary>
        /// <param name="request"><see cref="ArmaPresetRequest"/> containing the Steam published item details.</param>
        /// <returns>The launcher HTML preset.</returns>
        /// <remarks>Produces a 400 Bad Request if any of the mandatory parameters are not supplied.</remarks>
        [HttpPost("preset/generate")]
        [Produces("text/html", "application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(406)]
        public IActionResult Generate([FromBody] ArmaPresetRequest request)
        {
            try
            {
                return View("/Pages/PresetTemplate.cshtml", request);
            }
            catch (UnsupportedWorkshopItemException)
            {
                return BadRequest("Only mods, missions and collections with dependencies are supported");
            }
        }
    }
}