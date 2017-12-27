using Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;

namespace Baidu.Yingyan
{
    /// <summary>
    /// 坐标点
    /// </summary>
    public class LocationPoint
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public double latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double longitude { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("lat={0}, lng={1}", latitude, longitude);
        }
    }

    /// <summary>
    /// 包含时间的经纬度
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.LocationPoint" />
    public class LocationPointWithTime : LocationPoint
    {
        /// <summary>
        /// 定位时的设备时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime loc_time { get; set; }
    }

    /// <summary>
    /// 包含时间和地址的经纬度
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.LocationPointWithTime" />
    public class LocationPointWithAddress : LocationPointWithTime
    {
        /// <summary>
        /// 点地址
        /// </summary>
        public string address { get; set; }
    }
}