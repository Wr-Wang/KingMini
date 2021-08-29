using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LinqConsole
{
    public class WeatherForecast
    {
        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("TemperatureC")]
        public int TemperatureC { get; set; }

        [DisplayName("TemperatureF")]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [DisplayName("Summary")]
        public string Summary { get; set; }
    }
}
