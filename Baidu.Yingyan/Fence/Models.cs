using Baidu.Yingyan.Converters;
using Baidu.Yingyan.Track;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 围栏信息
    /// </summary>
    public abstract class FenceBaseInfo : IYingyanParam
    {
        /// <summary>
        /// 围栏类型
        /// </summary>
        public abstract FenceShapeEnums shape { get; }

        /// <summary>
        /// 创建围栏时为空
        /// </summary>
        public int fence_id { get; set; }

        /// <summary>
        /// 围栏名称
        /// </summary>
        public string fence_name { get; set; }

        /// <summary>
        /// 监控对象,支持指定一个entity或针对所有entity设置围栏
        /// 1、对指定一个entity_name创建围栏。
        /// 规则：monitored_person=entity_name
        /// 示例：monitored_person=张三
        /// 如设置为#allentity（monitored_person=#allentity），则对整个service下的所有entity创建围栏
        /// </summary>
        public string monitored_person { get; set; }

        /// <summary>
        /// 坐标类型
        /// </summary>
        public CoordType coord_type { get; set; } = CoordType.bd09ll;

        /// <summary>
        /// 围栏去噪参数
        /// 单位：米。每个轨迹点都有一个定位误差半径radius，这个值越大，代表定位越不准确，可能是噪点。
        /// 围栏计算时，如果噪点也参与计算，会造成误报的情况。设置denoise可控制，当轨迹点的定位误差半径大于设置值时，就会把该轨迹点当做噪点，不参与围栏计算。
        /// denoise默认值为0，不去噪。
        /// </summary>
        public int denoise { get; set; }

        /// <summary>
        /// 围栏创建时间，返回是有该字段
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 围栏修改时间，返回是有该字段
        /// </summary>
        public DateTime modify_time { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            if (fence_id > 0)
                nv["fence_id"] = fence_id.ToString();
            if (string.IsNullOrWhiteSpace(fence_name) == false)
                nv["fence_name"] = fence_name;
            if (string.IsNullOrWhiteSpace(monitored_person))
                monitored_person = "#allentity";
            nv["monitored_person"] = monitored_person;
            nv["coord_type"] = coord_type.ToString();
            if (denoise > 0)
                nv["denoise"] = denoise.ToString();
            return nv;
        }
    }

    /// <summary>
    /// 创建圆形围栏
    /// </summary>
    public class FenceCreateCircleFenceParam : FenceBaseInfo
    {
        /// <summary>
        /// 围栏类型
        /// </summary>
        public override FenceShapeEnums shape => FenceShapeEnums.circle;

        /// <summary>
        /// 围栏圆心
        /// </summary>
        public LocationPoint center { get; set; }

        /// <summary>
        /// 围栏半径
        /// 单位：米，取值范围(0,5000]
        /// </summary>
        public double radius { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv["longitude"] = center?.longitude.ToString();
            nv["latitude"] = center?.latitude.ToString();
            if (radius <= 0)
                radius = 1;
            else if (radius > 5000)
                radius = 5000;
            nv["radius"] = radius.ToString();
            return nv;
        }
    }

    /// <summary>
    /// 创建多边形围栏
    /// </summary>
    public class FenceCreatePolygonFenceParam : FenceBaseInfo
    {
        /// <summary>
        /// 围栏类型
        /// </summary>
        public override FenceShapeEnums shape => FenceShapeEnums.polygon;

        /// <summary>
        /// 多边形围栏形状点,
        /// 经纬度顺序为：纬度,经度；
        /// 顶点顺序可按顺时针或逆时针排列；
        /// 顶点个数在3-100个之间
        /// </summary>
        public LocationPoint[] vertexes { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            if (vertexes?.Any() == true)
            {
                var vs = string.Join(";", vertexes.Select(o => $"{o?.latitude},{o?.longitude}"));
                nv["vertexes"] = vs;
            }
            return nv;
        }
    }

    /// <summary>
    /// 创建线型围栏
    /// </summary>
    public class FenceCreatePolylineFenceParam : FenceBaseInfo
    {
        /// <summary>
        /// 围栏类型
        /// </summary>
        public override FenceShapeEnums shape => FenceShapeEnums.polyline;

        /// <summary>
        /// 线型围栏坐标点
        /// 经纬度顺序为：纬度,经度；
        /// 坐标点个数在2-100个之间
        /// </summary>
        public LocationPoint[] vertexes { get; set; }

        /// <summary>
        /// 偏离距离
        /// 偏移距离（若偏离折线距离超过该距离即报警），单位：米   示例：200
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            if (vertexes?.Any() == true)
            {
                var vs = string.Join(";", vertexes.Select(o => $"{o?.latitude},{o?.longitude}"));
                nv["vertexes"] = vs;
            }
            nv["offset"] = offset.ToString();
            return nv;
        }
    }

    /// <summary>
    /// 创建行政区划围栏
    /// </summary>
    public class FenceCreateDistrictFenceParam : FenceBaseInfo
    {
        /// <summary>
        /// 围栏类型
        /// </summary>
        public override FenceShapeEnums shape => FenceShapeEnums.district;

        /// <summary>
        /// 行政区划关键字
        /// 支持中国国家、省、市、区/县名称。请尽量输入完整的行政区层级和名称，保证名称的唯一性。
        /// 若输入的行政区名称匹配多个行政区，围栏将创建失败。
        /// 示例： 中国 北京市 湖南省长沙市 湖南省长沙市雨花区
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv["keyword"] = keyword;
            return nv;
        }
    }

    /// <summary>
    /// 创建圆形围栏结果
    /// </summary>
    public class FenceCreateFenceResult : CommonResult
    {
        /// <summary>
        /// 围栏的唯一标识
        /// fence_id由系统自动生成，按正整数递增
        /// </summary>
        public int fence_id { get; set; }
    }

    /// <summary>
    /// 创建行政区划围栏结果
    /// </summary>
    public class FenceCreateDistrictFenceResult : FenceCreateFenceResult
    {
        /// <summary>
        /// 结构化的行政区划描述
        /// status=0，围栏创建成功时返回该字段
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// 关键字匹配的行政区划列表
        /// status=5108：围栏创建失败，关键字匹配至多个行政区时，返回该字段
        /// </summary>
        public string[] district_list { get; set; }
    }

    /// <summary>
    /// 删除围栏参数
    /// </summary>
    public class FenceDeleteFenceParam : IYingyanParam
    {
        /// <summary>
        /// 监控对象
        /// 1、仅填写monitored_person字段：根据监控对象删除围栏，仅适用于删除“指定entity创建的围栏”，并删除该entity上的所有围栏（兼容旧版本）。
        /// 2、仅填写fence_ids字段：根据围栏id删除（针对该service下所有entity创建的围栏，使用此方法删除）
        /// 3、二字段均填写：根据该监控对象上的指定围栏删除
        /// </summary>
        public string monitored_person { get; set; }

        /// <summary>
        /// 围栏id列表
        /// 1、仅填写monitored_person字段：根据监控对象删除围栏，仅适用于删除“指定entity创建的围栏”，并删除该entity上的所有围栏（兼容旧版本）。
        /// 2、仅填写fence_ids字段：根据围栏id删除（针对该service下所有entity创建的围栏，使用此方法删除）
        /// 3、二字段均填写：根据该监控对象上的指定围栏删除
        /// </summary>
        public int[] fence_ids { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            if (string.IsNullOrWhiteSpace(monitored_person) == false)
                nv["monitored_person"] = monitored_person;
            if (fence_ids?.Any() == true)
            {
                nv["fence_ids"] = string.Join(",", fence_ids);
            }
            return nv;
        }
    }

    /// <summary>
    /// 删除围栏结果
    /// </summary>
    public class FenceDeleteFenceResult : CommonResult
    {
        /// <summary>
        /// 围栏id列表
        /// </summary>
        public int[] fence_ids { get; set; }
    }

    /// <summary>
    /// 查询围栏参数
    /// </summary>
    public class FenceListFenceParam : FenceDeleteFenceParam
    {
        /// <summary>
        /// 输出坐标类型
        /// </summary>
        public CoordType coord_type_output { get; set; } = CoordType.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv["coord_type_output"] = coord_type_output.ToString();
            return nv;
        }
    }

    /// <summary>
    /// 删除围栏结果
    /// </summary>
    public class FenceListFenceResult : CommonResult
    {
        /// <summary>
        /// 满足条件并返回的围栏个数
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 围栏列表
        /// </summary>
        [JsonConverter(typeof(FenceBaseInfoConverter))]
        public FenceBaseInfo[] fences { get; set; }
    }

    /// <summary>
    /// 围栏形状
    /// </summary>
    public enum FenceShapeEnums
    {
        /// <summary>
        /// 圆形
        /// </summary>
        circle,

        /// <summary>
        /// 多边形
        /// </summary>
        polygon,

        /// <summary>
        /// 线型
        /// </summary>
        polyline,

        /// <summary>
        /// 行政区划
        /// </summary>
        district
    }

    /// <summary>
    /// 围栏报警查询参数
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.IYingyanParam" />
    public class FenceQueryStatusParam : IYingyanParam
    {
        /// <summary>
        /// 监控对象
        /// </summary>
        public string monitored_person { get; set; }

        /// <summary>
        /// 围栏实体的id列表
        /// </summary>
        public int[] fence_ids { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            if (fence_ids?.Any() == true)
                nv["fence_ids"] = string.Join(",", fence_ids);
            nv["monitored_person"] = monitored_person;
            return nv;
        }
    }

    public class FenceQueryStatusResult:CommonResult
    {
        /// <summary>
        /// 返回结果的数量
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 报警的数量
        /// </summary>
        public FenceAlarmMonitoredStatus[] monitored_statuses { get; set; }
    }

    /// <summary>
    /// 围栏状态
    /// </summary>
    public class FenceAlarmMonitoredStatus
    {
        /// <summary>
        /// 围栏 id
        /// </summary>
        public string fence_id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public FenceAlarmMonitoredStatusEnums monitored_status { get; set; }
    }

    /// <summary>
    /// 围栏状态
    /// </summary>
    public enum FenceAlarmMonitoredStatusEnums
    {    /// <summary>
         /// 未知状态
         /// </summary>
        unknown,

        /// <summary>
        /// 在围栏内
        /// </summary>
        @in,

        /// <summary>
        /// 在围栏外
        /// </summary>
        @out,
    }

    /// <summary>
    /// 查询某监控对象的围栏报警信息
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Fence.FenceQueryStatusParam" />
    public class FenceHistoryAlarmParam : FenceQueryStatusParam
    {
        /// <summary>
        /// 开始时间,
        /// 若不填，则返回7天内所有报警信息
        /// </summary>
        public DateTime? start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// 若不填，则返回7天内所有报警信息
        /// </summary>
        public DateTime? end_time { get; set; }

        /// <summary>
        /// 返回坐标类型
        /// </summary>
        public CoordType coord_type_output { get; set; } = CoordType.bd09ll;

        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            if (start_time != null)
                nv["start_time"] = start_time.Value.ToUtcTicks().ToString();
            if (end_time != null)
                nv["end_time"] = end_time.Value.ToUtcTicks().ToString();
            nv["coord_type_output"] = coord_type_output.ToString();
            return nv;
        }
    }

    public class FenceAlarmHistoryQueryResult:CommonResult
    {
        /// <summary>
        /// 返回结果的数量
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 报警的数量
        /// </summary>
        public FenceAlarmHistory[] alarms { get; set; }
    }

    /// <summary>
    /// 报警历史
    /// </summary>
    public class FenceAlarmHistory
    {
        /// <summary>
        /// 围栏 id，唯一标识符
        /// </summary>
        public string fence_id { get; set; }

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
    /// <seealso cref="Baidu.Yingyan.Track.LocationPointWithTime" />
    public class FenceAlarmPoint : LocationPointWithTime
    {
        /// <summary>
        /// 定位精度，单位：米
        /// </summary>
        public int radius { get; set; }

        /// <summary>
        /// 返回的坐标类型，仅在国外区域返回该字段，返回值为：wgs84
        /// </summary>
        public CoordType? coord_type { get; set; }

        /// <summary>
        /// 服务端接收到报警信息的时间，
        /// 由于鹰眼 API 围栏为服务端围栏，即只有当轨迹点上传鹰眼服务端时，才能进行围栏触发判断。
        /// 因此服务端接收到报警的时间可能由于轨迹点上传的不及时性，而晚于围栏实际触发时间 loc_time。
        /// 例如，轨迹点实际触发围栏时间为13:00，但若由于各种原因，轨迹点上传至服务端进行围栏计算的时间为14:00，则该报警的 create_time为14:00。
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime create_time { get; set; }
    }

    /// <summary>
    /// 批量查询所有围栏报警信息
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Fence.FenceQueryStatusParam" />
    public class FenceBatcHistoryAlarmParam : IYingyanParam
    {
        /// <summary>
        /// 开始时间,
        /// 若不填，则返回7天内所有报警信息
        /// </summary>
        public DateTime? start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// 若不填，则返回7天内所有报警信息
        /// </summary>
        public DateTime? end_time { get; set; }

        /// <summary>
        /// 可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。
        /// </summary>
        public int page_index { get; set; } = 1;

        /// <summary>
        /// 可选，默认值为100。最大值1000。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录。
        /// </summary>
        public int page_size { get; set; } = 500;

        /// <summary>
        /// 返回坐标类型
        /// </summary>
        public CoordType coord_type_output { get; set; } = CoordType.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();

            if (start_time != null)
                nv["start_time"] = start_time.Value.ToUtcTicks().ToString();
            if (end_time != null)
                nv["end_time"] = end_time.Value.ToUtcTicks().ToString();
            nv["coord_type_output"] = coord_type_output.ToString();
            nv["page_index"] = page_index.ToString();
            nv["page_size"] = page_size.ToString();
            return nv;
        }
    }

    /// <summary>
    /// 批量查询所有围栏报警信息结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Fence.FenceAlarmHistoryQueryResult" />
    public class FenceBatcHistoryAlarmResult : FenceAlarmHistoryQueryResult
    {
        /// <summary>
        /// 符合条件的总报警数
        /// </summary>
        public int total { get; set; }
    }
}