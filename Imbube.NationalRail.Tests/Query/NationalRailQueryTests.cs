using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Imbube.NationalRail.Domain.Models;
using Imbube.NationalRail.Domain.Query;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imbube.NationalRail.Tests.Query
{

    public class NationalRailQueryTests
    {
        [TestClass]
        public class ObjectIntsanciation
        {
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

            [Ignore]
            [TestMethod]
            [Tag("Deprecated")] //Changed class structure
            [ExpectedException(typeof(ArgumentException))]
            public void ThrowErrorOnInvalidStationInputGreaterThan3Chars()
            {
                //Arrange
                const string invalidStation = "$%$123";
                NationalRailQuery.Stations.First().Code = invalidStation;
                //Act
                var nationalRailQuery = new NationalRailQuery(_stoneleigh, null);
                NationalRailQuery.Stations.First().Code = invalidStation;

            }

            [TestMethod]
            [Tag("Queries")]
            [ExpectedException(typeof(ArgumentException))]
            public void ThrowErrorOnInvalidStationInputNotOnList()
            {
                //Arrange
                var invalidStation = new Station(){Name = "Test", Code = "Test"};
                //Act
                var nationalRailQuery = new NationalRailQuery(invalidStation, null);

            }

            [TestMethod]
            [Tag("Queries")]
            public void ReadStationXmlandHaveStoneleighStationPresent()
            {
                //Arrange
                NationalRailQuery.BuildStationList();
                IEnumerable<Station> stationQuery =
        from station in NationalRailQuery.Stations
        where station.Code == StoneleighRailCode
        select station;
                //Act
                _stoneleigh = stationQuery.First();
                //Assert
                Assert.IsTrue(NationalRailQuery.Stations.Count == 1, "No Stations have been loaded");
                Assert.IsTrue(_stoneleigh.Code == StoneleighRailCode, "Invalid station loaded in unit test data");
            }
        }

        [TestClass]
        public class DataReponses
        {
            private string _jsonData;

            [TestInitialize]
            public void SetupData()
            {
                //Setup test response data
                var resource =
                    Application.GetResourceStream(new Uri(@"/Imbube.NationalRail.Tests;component/TestJsonResponse.txt",
                                                          UriKind.Relative));
                var streamReader = new StreamReader(resource.Stream);
                _jsonData = streamReader.ReadToEnd();
            }

            [TestMethod]
            [Tag("Response")]
            public void ParseJsonReponseAndCreateTimeTable()
            {
                //Arrange
                var query = new NationalRailQuery();
                //Act
                query.BuildTimeTableData(_jsonData);
                //Assert
                Assert.IsNotNull(query.StationQueryDetails, "Time Table not created correctly");
                Assert.IsTrue(query.StationQueryDetails.Trains.Count == 17, "Train count from sample data is incorrect");
            }

        }
    }
}
