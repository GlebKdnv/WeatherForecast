using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherForecast.Services;

namespace WeatherForecast.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestApi()
        {
            var meteoApi = new OpenMeteoApi(new System.Net.Http.HttpClient());
            var results = meteoApi.GetForecast(30,30).GetAwaiter().GetResult();
            Assert.IsNotNull(results);
        }
    }
}
