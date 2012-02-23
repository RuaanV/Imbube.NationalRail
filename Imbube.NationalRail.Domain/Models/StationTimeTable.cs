using System.Collections.Generic;

namespace Imbube.NationalRail.Domain.Models
{
    /// <summary>
    /// Main POCO from deserialized Json Response returned from Network Rail API
    /// </summary>
    public class StationTimeTable
    {
        /// <summary>
        /// Gets or sets the query station.
        /// </summary>
        /// <value>The query station.</value>
        public Station QueryStation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StationTimeTable"/> is error.
        /// </summary>
        /// <value><c>true</c> if error; otherwise, <c>false</c>.</value>
        public bool error { get; set; }

        /// <summary>
        /// Gets or sets the error MSG.
        /// </summary>
        /// <value>The error MSG.</value>
        public string errorMsg { get; set; }

        /// <summary>
        /// Gets or sets the error field.
        /// </summary>
        /// <value>The error field.</value>
        public string errorField { get; set; }

        /// <summary>
        /// Gets or sets the trains.
        /// </summary>
        /// <value>The trains.</value>
        public List<List<string>> trains { get; set; }

        private List<Train> _trains;
        /// <summary>
        /// Gets the full train object model .
        /// </summary>
        /// <value>The trains.</value>
        public List<Train> Trains { 
            
            get
            {
                if (_trains != null)
                    return _trains;
                _trains = new List<Train>();
                foreach (var train in trains)
                {
                    _trains.Add(new Train(){StartTime = train[0], ExpectedTime = train[1], Name = train[2], Status = train[3], Spare = train[4], UriForCurrentStatus = train[5]});
                }
                return _trains;
            }
        } 

        /// <summary>
        /// Gets or sets the buses.
        /// </summary>
        /// <value>The buses.</value>
        public List<List<string>> buses { get; set; }

        /// <summary>
        /// Gets or sets the ferries.
        /// </summary>
        /// <value>The ferries.</value>
        public List<List<string>> ferries { get; set; }

        /// <summary>
        /// Gets or sets the updates.
        /// </summary>
        /// <value>The updates.</value>
        public List<List<string>> updates { get; set; }
    }
}
