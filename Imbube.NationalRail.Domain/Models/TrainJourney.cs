using System;
using System.Collections.Generic;

namespace Imbube.NationalRail.Domain.Models
{
    /// <summary>
    /// Data Entity Containing Train Journey Data
    /// </summary>
    public class TrainJourney
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrainJourney"/> class.
        /// </summary>
        public TrainJourney()
        {
            //Initialise variables
            CallingStations = new List<string>();
        }

        /// <summary>
        /// Gets or sets the name of the Train Journey.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date that the train journey is scheduled to start.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the calling stations for the journey.
        /// </summary>
        /// <value>The calling stations.</value>
        public List<string> CallingStations { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancelled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is cancelled; otherwise, <c>false</c>.
        /// </value>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is on journey.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is on journey; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnJourney { get; set; }

        /// <summary>
        /// Gets or sets the current location.
        /// </summary>
        /// <value>The current location.</value>
        public string CurrentLocation { get; set; }

    }
}
