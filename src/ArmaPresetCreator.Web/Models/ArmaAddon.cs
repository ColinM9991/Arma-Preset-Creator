using System.ComponentModel.DataAnnotations;

namespace ArmaPresetCreator.Web.Models
{
    /// <summary>
    /// Contains information for an Arma addon.
    /// </summary>
    public class ArmaAddon
    {
        /// <summary>
        /// The name of the addon.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        /// The URL of the addon. This should be a Steam file or workshop item URL.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"https:\/\/steamcommunity.com\/sharedfiles\/filedetails\/\?id=[0-9]+", ErrorMessage = "Only Steam Workshop URL's can be used for addon URL's.")]
        public string Url { get; set; }
    }
}
