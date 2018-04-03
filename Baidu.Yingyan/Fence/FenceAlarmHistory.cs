using Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 报警历史
    /// </summary>
    public class FenceAlarmHistory
    {
        /// <summary>
        /// 围栏 id，唯一标识符
        /// </summary>
        public int fence_id { get; set; }

        /// <summary>
        /// 围栏的名称
        /// </summary>
        public string fence_name { get; set; }

        /// <summary>
        /// 监控对象
        /// </summary>
        public string monitored_person { get; set; }

        /// <summary>
        /// 触发动作
        /// 可能的返回值：
        /// enter：进入围栏
        /// exit：离开围栏
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// 触发围栏报警轨迹点
        /// </summary>
        public FenceAlarmPoint alarm_point { get; set; }

        /// <summary>
        /// 触发围栏报警轨迹点的上一个轨迹点
        /// </summary>
        public FenceAlarmPoint pre_point { get; set; }
    }

    /// <summary>
    /// 围栏报警点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.LocationPointWithTime" />
    public class FenceAlarmPoint : LocationPointWithTime
    {
        /// <summary>
        /// 定位精度，单位：米
        /// </summary>
        public int radius { get; set; }

        /// <summary>
        /// 返回的坐标类型，仅在国外区域返回该字段，返回值为：wgs84
        /// </summary>
        public CoordTypeEnums? coord_type { get; set; }

        /// <summary>
        /// 服务端接收到报警信息的时间，
        /// 由于鹰眼 API 围栏为服务端围栏，即只有当轨迹点上传鹰眼服务端时，才能进行围栏触发判断。
        /// 因此服务端接收到报警的时间可能由于轨迹点上传的不及时性，而晚于围栏实际触发时间 loc_time。
        /// 例如，轨迹点实际触发围栏时间为13:00，但若由于各种原因，轨迹点上传至服务端进行围栏计算的时间为14:00，则该报警的 create_time为14:00。
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime create_time { get; set; }
    }
}