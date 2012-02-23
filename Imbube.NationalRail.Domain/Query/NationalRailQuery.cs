using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Imbube.NationalRail.Domain.Models;
using Imbube.NationalRail.Domain.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Imbube.NationalRail.Domain.Query
{
    public class NationalRailQuery: RailQueryBase
    {
        private const string NetworkRailBaseUri = "http://ojp.nationalrail.co.uk/en/s/ldb/liveTrainsJson?";

        private const string DepartingParameter = "departing={0}";

        private const string TrainsFromParameter = "liveTrainsFrom={0}";

        private const string TrainsToParameter = "liveTrainsTo={0}";

        private const string ServiceIdParameter = "serviceId={0}";

        private readonly string _nationalRailQueryUri = string.Empty;

        /// <summary>
        /// Gets or sets the network rail data.
        /// </summary>
        /// <value>The network rail data.</value>
        public string NetworkRailData { get; set; }

        /// <summary>
        /// Gets or sets the stations.
        /// </summary>
        /// <value>The stations.</value>
        public static List<Station> Stations { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="NationalRailQuery"/> class.
        /// </summary>
        /// <param name="route">The route.</param>
        public NationalRailQuery(Route route)
            :this(route.FromStation, route.ToStation)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalRailQuery"/> class.
        /// </summary>
        /// <param name="fromStation">From station.</param>
        /// <param name="toStation">To station.</param>
        public NationalRailQuery(Station fromStation, Station toStation)
            :this()
        {
            if(fromStation == null && toStation == null)
                throw new ArgumentException(Resource.InvalidStationCode);

            //Validation to occur in implementing class as this is provider specific
            FromStation = fromStation;
            ToStation = toStation;

            //Check Station Exists
            ValidateStations();

            //Compile Uri for Station Query
            _nationalRailQueryUri = CompileUri();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalRailQuery"/> class.
        /// </summary>
        public NationalRailQuery()
        {
            //Check if Stations List has been instanciated and load if necessary
            if (Stations == null)
            {
                lock (this)
                {
                    BuildStationList();
                }
            }
        }

        private void ValidateStations()
        {
            bool stationExists = Stations.Contains(ToStation);

            if (stationExists)
                return;

            stationExists = Stations.Contains(FromStation);

            if(!stationExists)
                throw new ArgumentException(Resource.NonexistantStation);
        }

        /// <summary>
        /// Builds the station list.
        /// </summary>
        public static void BuildStationList()
        {
            XDocument document = XDocument.Load(@"Data\Stations.xml");
            var data = document.Descendants("Station").Select(c => new Station()
                                                                       {
                                                                           Name = c.Attribute("name").Value,
                                                                           Code = c.Attribute("code").Value
                                                                       });

            Stations = data.ToList();

        }

        string CompileUri()
        {     
            //Check station properties if they have not been set
            if(FromStation == null)
                FromStation = new Station();
            if(ToStation == null)
                ToStation = new Station();

            string uri = string.Format("{0}{1}&{2}&{3}&{4}", NetworkRailBaseUri,
                                                 string.Format(DepartingParameter, false), string.Format(TrainsFromParameter, FromStation.Code), string.Format(TrainsToParameter, ToStation.Code), string.Format(ServiceIdParameter, string.Empty));
            return uri;
        }

        /// <summary>
        /// Updates the schedule data by calling the provider specific implementation. <remarks>Network Rail specific implementation </remarks>
        /// </summary>
        public override void UpdateScheduleData()
        {
            RetrievedScheduleSuccessfully = false;
            //Intiate new HTTP Web Request to return to non UI Thread 
            var request = WebRequest.Create(_nationalRailQueryUri);
            request.BeginGetResponse(UpdateScheduleCallback, request);

        }

        private void UpdateScheduleCallback(IAsyncResult result)
        {
            try
            { 
                var request = (HttpWebRequest)result.AsyncState;
                var response = request.EndGetResponse(result);

                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    NetworkRailData = reader.ReadToEnd();
                }

                BuildTimeTableData(NetworkRailData);

                RetrievedScheduleSuccessfully = true;
                
            }
            finally
            {
                RaiseCompletedCallEvent();
            }
        }

        /// <summary>
        /// Gets or sets the station query details.
        /// </summary>
        /// <value>The station query details.</value>
        public StationTimeTable StationQueryDetails { get; set; }

        /// <summary>
        /// Builds the time table data from the returned Json Query Data from Network Rail API.
        /// </summary>
        /// <param name="jsonData">The json data.</param>
        public void BuildTimeTableData(string jsonData)
        {
            //Check data is not null
            if(jsonData == null)
                throw new ArgumentException(Resource.NullJsonData);
            var data = JsonConvert.DeserializeObject<StationTimeTable>(jsonData);
            data.QueryStation = FromStation;
            StationQueryDetails = data;
        }
    }
}
