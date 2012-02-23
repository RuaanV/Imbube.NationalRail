using System.Collections.Generic;
using Imbube.NationalRail.Domain.Models;

namespace Imbube.NationalRail.Domain.Interface
{
    /// <summary>
    /// Delegate for implementation
    /// </summary>
    public delegate void CompletedCallDelegate();

    /// <summary>
    /// Abstraction of Schedule Retriever
    /// </summary>
    public interface IRailQuery
    {
        /// <summary>
        /// Occurs when [completed asynch call].
        /// </summary>
        event CompletedCallDelegate CompletedAsynchCall; 

        /// <summary>
        /// Gets the passenger station code <remarks> code different per information provider</remarks>.
        /// </summary>
        /// <value>The passenger station.</value>
        string PassengerStation { get; }

        /// <summary>
        /// Gets the scheduled trains based on the passenger station.
        /// </summary>
        /// <value>The scheduled trains.</value>
        List<TrainJourney> ScheduledTrains { get; }

        /// <summary>
        /// Gets or sets a value indicating whether a schedule has been retrieved successfully.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if retrieved schedule successfully; otherwise, <c>false</c>.
        /// </value>
        bool RetrievedScheduleSuccessfully { get; set; }

        /// <summary>
        /// Updates the schedule data by calling the provider specific implementation.
        /// </summary>
        void UpdateScheduleData();
    }
}
