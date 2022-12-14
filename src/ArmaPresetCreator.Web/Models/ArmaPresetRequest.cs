using System.ComponentModel.DataAnnotations;

namespace ArmaPresetCreator.Web.Models
{
    /// <summary>
    /// Contains information required for the generating an Arma launcher preset.
    /// </summary>
    public class ArmaPresetRequest
    {
        /// <summary>
        /// The name of the preset which will be generated.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        /// The addons to add to the preset.
        /// </summary>
        [Required]
        [MinLength(1)]
        public ArmaAddon[] Items { get; set; }
    }
}
