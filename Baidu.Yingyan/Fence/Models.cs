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
        public List<FenceMonitoredPersonStatus> monitored_person_statuses { get; set; }
    }

    public class FenceMonitoredPersonStatus
    {
        public string monitored_person { get; set; }
        public FenceMonitoredStatus monitored_status { get; set; }
    }

    public enum FenceMonitoredStatus
    {
        Unknown = 0,
        In,
        Out
    }

    public class FenceHistoryAlarmResult : CommonResult
    {
        public int size { get; set; }
        public List<FenceMonitoredPersonAlarm> monitored_person_alarms { get; set; }
    }

    public class FenceMonitoredPersonAlarm
    {
        public string monitored_person { get; set; }
        public int alarm_size { get; set; }
        public List<FenceAlarmItem> alarms { get; set; }
    }

    public class FenceAlarmItem
    {
        public FenceMonitoredStatus action { get; set; }
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime time { get; set; }
    }

    /// <summary>
    /// 围栏列表
    /// </summary>
    public class FenceListResult : CommonResult
    {
        public int size { get; set; }

        public List<FenceItemAsResult> fences { get; set; }
    }
}
