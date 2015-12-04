using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan
{
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

        public override string ToString()
        {
            return string.Format("lat={0}, lng={1}", latitude, longitude);
        }
    }
}
