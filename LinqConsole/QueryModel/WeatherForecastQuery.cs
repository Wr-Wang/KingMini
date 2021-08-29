using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LinqConsole
{
    /// <summary>
    /// 查询实体
    /// </summary>
    public class WeatherForecastQuery
    {
        [DisplayName("DateMin")]
        [CompareName("Date")]
        public DateTime? DateMin { get; set; }

        [DisplayName("DateMax")]
        [CompareName("Date")]
        public DateTime? DateMax { get; set; }

        [DisplayName("TemperatureC")]
        public int TemperatureC { get; set; }

        [DisplayName("TemperatureF")]
        public int TemperatureF { get; set; }

        [DisplayName("Summary")]
        public string Summary { get; set; }
    }
}
