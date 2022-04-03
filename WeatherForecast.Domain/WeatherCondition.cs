namespace WeatherForecast.Domain
{
    public class WeatherCondition
    {
        public double TempCelcius { get; set; }
        public double WindSpeed { get; set; }
        public double WindDirection { get; set; }
        public double Percipation { get; set; }
        public DateTime DateTime { get; set; }
    }
}
