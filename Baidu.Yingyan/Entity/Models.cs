using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan.Entity
{

    public class EntityListReault : CommonResult
    {
        public int size { get; set; }
        public int total { get; set; }
        public EntityItem[] entities { get; set; }
    }

    public class EntityListSimpleReault : CommonResult
    {
        public int size { get; set; }
        public int total { get; set; }
        public string[] entities { get; set; }
    }

    public class EntityItem
    {
        public EntityRealtimePoint realtime_point { get; set; }

        public string entity_name { get; set; }
        public DateTime create_time { get; set; }
        public DateTime modify_time { get; set; }

        public int? direction { get; set; }

        public double? speed { get; set; }

        public double? radius { get; set; }


        [JsonExtensionData]
        public IDictionary<string, object> columns { get; set; }
    }

    public class EntityRealtimePoint
    {
        /// <summary>
        /// 该entity最新定位时间
        /// </summary>
        public int loc_time { get; set; }
        /// <summary>
        /// 百度加密坐标
        /// </summary>
        public double[] location { get; set; }
        /// <summary>
        /// 坐标类型，=3
        /// </summary>
        public CoordType coord_type { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> columns { get; set; }
    }




    public class EntityColumnListResult : CommonResult
    {
        public EntityColumn[] columns { get; set; }
    }

    public class EntityColumn
    {
        public int is_search { get; set; }
        public DateTime create_time { get; set; }
        public DateTime modify_time { get; set; }
        public string column_key { get; set; }
        public string column_desc { get; set; }
    }

}
