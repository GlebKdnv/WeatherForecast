// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using WeatherForecast.Domain;

namespace WeatherForecast.Services
{
    internal class OpenMeteoApiConvertor
    {
        public static WeatherData ConvertResponce(string responceString)
        {
            var jsonResp = JsonNode.Parse(responceString);
            var timeArray = jsonResp!["hourly"]!["time"]!.Deserialize<IReadOnlyList<DateTime>>();
            var temperatureArray = jsonResp!["hourly"]!["temperature_2m"]!.Deserialize<IReadOnlyList<float>>();
            var longtitude = (float)jsonResp!["longitude"];
            var latitude = (float)jsonResp!["latitude"];
            var windDirection = jsonResp!["hourly"]!["winddirection_10m"].Deserialize<IReadOnlyList<float>>();
            var windSpeed = jsonResp!["hourly"]!["windspeed_10m"].Deserialize<IReadOnlyList<float>>();
            var percipation = jsonResp!["hourly"]!["precipitation"].Deserialize<IReadOnlyList<float>>();

            var conditions = timeArray.Select((time, indx) =>
            new WeatherCondition
            {
                DateTime = time,
                TempCelcius = temperatureArray[indx],
                Percipation = percipation[indx],
                WindSpeed = windSpeed[indx],
            });

            var result = new WeatherData()
            {
                Coordinates = new Coordinates() { Lat = latitude, Long = longtitude },
                Conditions = conditions
            };
            return result;
        }

    }
}
