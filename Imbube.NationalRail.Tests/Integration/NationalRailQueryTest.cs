using System;
using System.Collections.Generic;
using System.Linq;
using Imbube.NationalRail.Domain.Models;
using Imbube.NationalRail.Domain.Query;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imbube.NationalRail.Tests.Integration
{
    /// <summary>
    /// Summary description for NationalRailQueryTest
    /// </summary>
    [TestClass]
    public class NationalRailQueryTest : SilverlightTest
    {
        public const string NetworkRailUri = "http://ojp.nationalrail.co.uk/en/s/ldb/liveTrainsJson?departing=false&liveTrainsFrom=Stoneleigh&liveTrainsTo=&serviceId=";

        private Station _stoneleigh;
        private const string StoneleighRailCode = "SNL";

        [TestInitialize]
        public void Setup()
        {
            NationalRailQuery.BuildStationList();

            IEnumerable<Station> stationQuery =
                    from station in NationalRailQuery.Stations
                    where station.Code == StoneleighRailCode
                    select station;

            _stoneleigh = stationQuery.First();
            if (_stoneleigh.Code != StoneleighRailCode)
                throw new ArgumentException("Invaild data loaded in test setup");
        }

        [Asynchronous]
        [TestMethod]
        [Tag("Integration Tests")]
        public void GetRailQueryJason()
        {
           //Arrange
            var route = new Route(){FromStation = _stoneleigh};
            var networkRailRetriever = new NationalRailQuery(route);
            //Assert
            networkRailRetriever.CompletedAsynchCall += () =>
                                                            {
                                                                try
                                                                {
                                                                    Assert.IsTrue(
                                                                    networkRailRetriever.RetrievedScheduleSuccessfully,
                                                                    "Failed to retrieve JSON from Uri");
                                                                    Assert.IsTrue(networkRailRetriever.StationQueryDetails.Trains.Count > 0, "Invalid data returned for Stoneleigh station from network rail");
                                                                    Assert.IsTrue(networkRailRetriever.StationQueryDetails.QueryStation == _stoneleigh, "Invalid query station returned");
                                                                }
                                                                finally
                                                                {
                                                                    EnqueueTestComplete();
                                                                }
                                                            };
            //Act
            networkRailRetriever.UpdateScheduleData();

        }


    }
}
