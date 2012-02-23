namespace Imbube.NationalRail.Domain.Models
{
    /// <summary>
    /// Defines the rail query route
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Gets or sets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        public string RouteName { get; set; }

        /// <summary>
        /// Gets or sets from station.
        /// </summary>
        /// <value>From station.</value>
        public Station FromStation { get; set; }
        /// <summary>
        /// Gets or sets to station.
        /// </summary>
        /// <value>To station.</value>
        public Station ToStation { get; set; }
    }
}
