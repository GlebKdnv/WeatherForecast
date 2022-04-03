using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Domain
{
    public class WeatherData
    {
        public Coordinates Coordinates { get; set; }
        public IEnumerable<WeatherCondition> Conditions { get; set; }

    }
}
