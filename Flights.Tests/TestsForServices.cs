﻿using Flights.Models;
using Flights.Services;
using Flights.Services.DataModels;
using Flights.Services.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flights.Tests
{
    [TestClass]
    public class TestsForServices
    {
        #region Countries.GetCountries tests
        public void CountriesTest(string expected, string assertMessage)
        {
            var actual = new Countries().GetCountries();
            Assert.AreNotEqual(expected, actual, assertMessage);
        }

        [TestMethod]
        [TestCategory("Countries.GetCountries")]
        public void GetCountriesTest()
        {
            CountriesTest(
                   null,
                   "Countries.GetCountries should return list of countries"
                );
        }
        #endregion Countries.GetCountries tests

        #region HttpService.GetRequestAsync tests
        public void HttpServiceTest(string url, string expected, string assertMessage)
        {
            var actual = new HttpService().GetRequestAsync(url).Result;
            Assert.AreNotEqual(expected, actual, assertMessage);
        }

        [TestMethod]
        [TestCategory("HttpService.GetRequestAsync")]
        public void GetRequestAsyncTest()
        {
            HttpServiceTest(
                "http://flybaseapi.azurewebsites.net/odata/code_iata('NoCity')",
                null,
                "HttpService.GetRequestAsync should return not null"
                );

            HttpServiceTest(
                "http://flybaseapi.azurewebsites.net/odata/code_iata('Minsk')",
                null,
                "HttpService.GetRequestAsync should return not null"
                );
        }
        #endregion HttpService.GetRequestAsync tests

        #region JsonConverter.Deserialize tests
        public void JsonConverterDeserializeTest<T>(string str, string expected, string assertMessage)
        {
            var actual = new JsonConverter().Deserialize<T>(str);
            Assert.AreNotEqual(expected, actual, assertMessage);
        }

        [TestMethod]
        [TestCategory("JsonConverter.Deserialize")]
        public void DeserializeTest()
        {
            JsonConverterDeserializeTest<AirportInfo>(
                "{'@odata.context':'http://flybaseapi.azurewebsites.net/odata/$metadata#code_iata', 'value':[{'Iata':'BES','City':'Brest'},{'Iata':'BQT','City':'Brest'}]}",
                null,
                "JsonConverter.Deserialize should return not null"
                );
        }
        #endregion JsonConverter.Deserialize tests

        #region JsonConverter.Serialize tests
        public void JsonConverterSerializeTest<T>(object obj, string expected, string assertMessage)
        {
            var actual = new JsonConverter().Serialize(obj);
            Assert.AreEqual(expected, actual, assertMessage);
        }

        [TestMethod]
        [TestCategory("JsonConverter.Serialize")]
        public void SerializeTest()
        {
            JsonConverterSerializeTest<AirportInfo>(
                new FlyInfoShow
                {
                    Arrival = "Rrrival",
                    Duration = "Duration",
                    ArrivalTerminal = "ArrivalTerminal",
                    From = "From",
                    ThreadCarrierTitle = "ThreadCarrierTitle",
                    ThreadVehicle = "ThreadVehicle",
                    ThreadNumber = "ThreadNumber",
                    Departure = "Departure",
                    To = "To",
                    IsReservedFlight = true
                },
                "{\"Arrival\":\"Rrrival\",\"Duration\":\"Duration\",\"ArrivalTerminal\":\"ArrivalTerminal\",\"ThreadCarrierTitle\":\"ThreadCarrierTitle\",\"ThreadVehicle\":\"ThreadVehicle\",\"ThreadNumber\":\"ThreadNumber\",\"Departure\":\"Departure\",\"To\":\"To\",\"From\":\"From\",\"IsReservedFlight\":true}",
                "JsonConverter.Serialize should return not null"
                );
        }
        #endregion JsonConverter.Serialize tests
    }
}