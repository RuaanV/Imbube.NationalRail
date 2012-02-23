using System;
using System.Collections.Generic;
using Imbube.NationalRail.Domain.Interface;
using Imbube.NationalRail.Domain.Models;

namespace Imbube.NationalRail.Domain.Query
{
    public abstract class RailQueryBase : IRailQuery
    {

        protected RailQueryBase()
        {
            //Initialise variables for base class
            ScheduledTrains = new List<TrainJourney>();


        }

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

        /// <summary>
        /// Gets the passenger station code <remarks> code different per information provider</remarks>.
        /// </summary>
        /// <value>The passenger station.</value>
        public string PassengerStation { get; private set; }

        /// <summary>
        /// Gets the scheduled trains based on the passenger station.
        /// </summary>
        /// <value>The scheduled trains.</value>
        public List<TrainJourney> ScheduledTrains { get; private set; }

        /// <summary>
        /// Updates the schedule data by calling the provider specific implementation. <remarks>provider specific implementation required</remarks>
        /// </summary>
        public abstract void UpdateScheduleData();

        /// <summary>
        /// Gets or sets a value indicating whether a schedule has been retrieved successfully.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [retrieved schedule successfully]; otherwise, <c>false</c>.
        /// </value>
        public bool RetrievedScheduleSuccessfully { get; set; }

        /// <summary>
        /// Occurs when internal calls are completed to allow the UI to update to a not processing state.
        /// </summary>
        public event CompletedCallDelegate CompletedAsynchCall;

        /// <summary>
        /// Raises the completed call event.
        /// </summary>
        protected void RaiseCompletedCallEvent()
        {
            if (CompletedAsynchCall != null)
                CompletedAsynchCall();
        }
    }
}
