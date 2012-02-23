namespace Imbube.NationalRail.Domain.Interface
{
    public interface ITrain
    {
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        string StartTime { get; set; }
        /// <summary>
        /// Gets or sets the expected time.
        /// </summary>
        /// <value>The expected time.</value>
        string ExpectedTime { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        string Status { get; set; }
        /// <summary>
        /// Gets or sets the spare.
        /// </summary>
        /// <value>The spare.</value>
        string Spare { get; set; }
        /// <summary>
        /// Gets or sets the URI for current status.
        /// </summary>
        /// <value>The URI for current status.</value>
        string UriForCurrentStatus { get; set; }
    }
}
