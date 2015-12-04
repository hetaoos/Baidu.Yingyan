using io.nulldata.Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan.Fence
{

    public class FenceCreateResult : CommonResult
    {
        public int fence_id { get; set; }
    }


    public class FenceStatusResult : CommonResult
    {
        public int size { get; set; }
        public List<monitored_person_status> monitored_person_statuses { get; set; }
    }

    public class monitored_person_status
    {
        public string monitored_person { get; set; }
        public monitored_status monitored_status { get; set; }
    }

    public enum monitored_status
    {
        Unknown = 0,
        In,
        Out
    }

    public class FenceHistoryAlarmResult : CommonResult
    {
        public int size { get; set; }
        public List<monitored_person_alarm> monitored_person_alarms { get; set; }
    }

    public class monitored_person_alarm
    {
        public string monitored_person { get; set; }
        public int alarm_size { get; set; }
        public List<alarmItem> alarms { get; set; }
    }

    public class alarmItem
    {
        public monitored_status action { get; set; }
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime time { get; set; }
    }
}
