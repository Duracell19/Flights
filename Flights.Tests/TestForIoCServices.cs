using Flights.Infrastructure.Interfaces;
using Flights.Services;
using Flights.Services.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmCross.Test.Core;

namespace Flights.Tests
{
    [TestClass]
    public class TestsForIoCServices : MvxIoCSupportingTest
    {
        /// <summary>
        /// This test check if method return list of cities
        /// </summary>
        [TestMethod]
        [TestCategory("CitiesService.GetCitiesAsync")]
        public void GetCitiesAsyncTest()
        {
            base.ClearAll();

            var mockHttpService = new HttpService();
            Ioc.RegisterSingleton<IHttpService>(mockHttpService);

            var mockJsonConverter = new JsonConverter();
            Ioc.RegisterSingleton<IJsonConverter>(mockJsonConverter);

            var service = new CitiesService(mockHttpService, mockJsonConverter);
            NUnit.Framework.Assert.IsNotNull(service.GetCitiesAsync("Belarus").Result);
        }
        /// <summary>
        /// This method check if method return list of iatas
        /// </summary>
        [TestMethod]
        [TestCategory("IatasService.GetIataAsync")]
        public void GetIatasAsyncTest()
        {
            base.ClearAll();

            var mockHttpService = new HttpService();
            Ioc.RegisterSingleton<IHttpService>(mockHttpService);

            var mockJsonConverter = new JsonConverter();
            Ioc.RegisterSingleton<IJsonConverter>(mockJsonConverter);

            var service = new IataService(mockHttpService, mockJsonConverter);
            NUnit.Framework.Assert.IsNotNull(service.GetIataAsync("Minsk").Result);
        }
        /// <summary>
        /// This method check if method get flights
        /// </summary>
        [TestMethod]
        [TestCategory("FlightsService.GetFlightAsync")]
        public void GetFlightAsyncTest()
        {
            base.ClearAll();

            var mockHttpService = new HttpService();
            Ioc.RegisterSingleton<IHttpService>(mockHttpService);

            var mockJsonConverter = new JsonConverter();
            Ioc.RegisterSingleton<IJsonConverter>(mockJsonConverter);

            var service = new FlightsService(mockHttpService, mockJsonConverter);
            NUnit.Framework.Assert.IsNotNull(service.GetFlightAsync("2017-01-19", "DME", "MSQ"));
        }
    }
}