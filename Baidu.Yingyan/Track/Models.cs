using Baidu.Yingyan.Converters;
using Baidu.Yingyan.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Baidu.Yingyan.Track
{
    public class TrackPoint : EntityLocationPoint
    {
        /// <summary>
        /// entity唯一标识
        /// </summary>
        public string entity_name { get; set; }

        /// <summary>
        /// 坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_input { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", entity_name, longitude, latitude, loc_time.ToUtcTicks(), coord_type_input);
        }
    }

    /// <summary>
    /// 批量添加点返回结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class BatchAddPointResult : CommonResult
    {
        /// <summary>
        /// 上传成功的点个数
        /// </summary>
        public int success_num { get; set; }

        /// <summary>
        /// 上传失败的点信息
        /// </summary>
        public BatchAddPointFailInfo fail_info { get; set; }
    }

    /// <summary>
    /// 批量添加的错误点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.TrackPoint" />
    public class TrackErrorPoint : TrackPoint
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string error { get; set; }
    }

    /// <summary>
    /// 上传失败的点信息
    /// </summary>
    public class BatchAddPointFailInfo
    {
        /// <summary>
        /// 输入参数不正确导致的上传失败的点
        /// </summary>
        public TrackErrorPoint[] param_error { get; set; }

        /// <summary>
        /// 服务器内部错误导致上传失败的点
        /// </summary>
        public TrackErrorPoint[] internal_error { get; set; }
    }

    /// <summary>
    /// 纠偏选项
    /// </summary>
    public class TrackHistoryProcessOption
    {
        /// <summary>
        /// 去噪，默认为1
        /// </summary>
        public bool? need_denoise { get; set; }

        /// <summary>
        /// 绑路，之前未开通绑路的service，默认值为0；之前已开通绑路的service，默认值为1
        /// </summary>
        public bool? need_mapmatch { get; set; }

        /// <summary>
        /// 抽稀,默认值为0
        /// </summary>
        public bool? need_vacuate { get; set; }

        /// <summary>
        ///   定位精度过滤，用于过滤掉定位精度较差的轨迹点，默认为0
        /// </summary>
        public int? radius_threshold { get; set; }

        /// <summary>
        /// 交通方式
        /// </summary>
        public TrackHistoryTransportModeEnums? transport_mode { get; set; }

        /// <summary>
        /// 获取选项值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetOption(string name, bool value)
        {
            var t = value == true ? 1 : 0;
            return $"{name}={t}";
        }

        public override string ToString()
        {
            var options = new List<string>();
            if (need_denoise != null)
                options.Add(GetOption(nameof(need_denoise), need_denoise.Value));
            if (radius_threshold > 0)
                options.Add($"{nameof(radius_threshold)}={radius_threshold}");
            if (need_vacuate != null)
                options.Add(GetOption(nameof(need_vacuate), need_vacuate.Value));
            if (need_mapmatch != null)
                options.Add(GetOption(nameof(need_mapmatch), need_mapmatch.Value));
            if (transport_mode != null)
                options.Add($"{nameof(transport_mode)}={(int)transport_mode}");
            return string.Join(",", options);
        }
    }

    /// <summary>
    /// 交通方式
    /// </summary>
    public enum TrackHistoryTransportModeEnums
    {
        /// <summary>
        /// 驾车(默认)
        /// </summary>
        driving = 1,

        /// <summary>
        ///  骑行
        /// </summary>
        riding = 2,

        /// <summary>
        /// 步行
        /// </summary>
        walking = 3,
    }

    /// <summary>
    /// 里程补偿方式
    /// </summary>
    public enum TrackHistorySupplementModeEnums
    {
        /// <summary>
        /// 不补充，中断两点间距离不记入里程。
        /// </summary>
        no_supplement,

        /// <summary>
        ///  使用直线距离补充
        /// </summary>
        straight,

        /// <summary>
        /// 使用最短驾车路线距离补充
        /// </summary>
        driving,

        /// <summary>
        ///  使用最短骑行路线距离补充
        /// </summary>
        riding,

        /// <summary>
        /// 使用最短步行路线距离补充
        /// </summary>
        walking,
    }

    /// <summary>
    /// 实时纠偏参数
    /// </summary>
    public class TrackHistoryGetLatestPointParam : IYingyanParam
    {
        /// <summary>
        /// entity唯一标识
        /// </summary>
        public string entity_name { get; set; }

        /// <summary>
        /// 纠偏选项
        /// </summary>
        public TrackHistoryProcessOption process_option { get; set; }

        /// <summary>
        /// 返回的坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_output { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            nv.Add("entity_name", entity_name);
            var op = process_option?.ToString();
            if (string.IsNullOrWhiteSpace(op) == false)
                nv.Add("process_option", op);

            nv.Add("coord_type_output", coord_type_output.ToString());
            return nv;
        }
    }

    /// <summary>
    /// 实时纠偏结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class TrackHistoryGetLatestPointResult : CommonResult
    {
        /// <summary>
        ///实时位置信息
        /// </summary>
        public EntityLocationPoint latest_point { get; set; }

        /// <summary>
        /// 道路限速,单位：km/h
        /// </summary>
        public double limit_speed { get; set; }
    }

    /// <summary>
    /// 查询轨迹里程参数
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.TrackHistoryGetLatestPointParam" />
    public class TrackHistoryGetDistanceParam : TrackHistoryGetLatestPointParam
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// 结束时间不能大于当前时间，且起止时间区间不超过24小时。为提升响应速度，同时避免轨迹点过多造成请求超时（3s）失败，建议缩短每次请求的时间区间，将一天轨迹拆分成多段进行拼接
        /// </summary>
        public DateTime end_time { get; set; }

        /// <summary>
        /// 是否返回纠偏后里程
        /// </summary>
        public bool is_processed { get; set; }

        /// <summary>
        /// 里程补偿方式
        /// </summary>
        public TrackHistorySupplementModeEnums supplement_mode { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("start_time", start_time.ToUtcTicks().ToString());
            nv.Add("end_time", end_time.ToUtcTicks().ToString());
            nv.Add("is_processed", is_processed ? "1" : "0");
            nv.Add("supplement_mode", supplement_mode.ToString());
            return nv;
        }
    }

    /// <summary>
    /// 查询轨迹里程结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class TrackHistoryGetDistanceResult : CommonResult
    {
        /// <summary>
        /// 轨迹里程
        /// </summary>
        public double distance { get; set; }
    }

    /// <summary>
    /// 轨迹查询与纠偏参数
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.TrackHistoryGetDistanceParam" />
    public class TrackHistoryGetTrackParam : TrackHistoryGetDistanceParam
    {
        /// <summary>
        /// 返回轨迹点的排序规则
        /// </summary>
        public bool asc { get; set; } = true;

        /// <summary>
        /// 可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。
        /// </summary>
        public int page_index { get; set; } = 1;

        /// <summary>
        /// 可选，默认值为100。最大值5000。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录。
        /// </summary>
        public int page_size { get; set; } = 100;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("sort_type", asc ? "asc" : "desc");
            nv.Add("page_index", page_index.ToString());
            nv.Add("page_size", page_size.ToString());
            return nv;
        }
    }

    /// <summary>
    /// 轨迹查询与纠偏结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class TrackHistoryGetTrackResult : CommonResult
    {
        /// <summary>
        /// 忽略掉page_index，page_size后的轨迹点数量
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 返回的结果条数
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 此段轨迹的里程数，单位：米
        /// </summary>
        public double distance { get; set; }

        /// <summary>
        /// 此段轨迹的收费里程数，单位：米
        /// </summary>
        public double toll_distance { get; set; }

        /// <summary>
        /// 起点信息
        /// </summary>
        public LocationPointWithTime start_point { get; set; }

        /// <summary>
        /// 终点信息
        /// </summary>
        public LocationPointWithTime end_point { get; set; }

        /// <summary>
        /// 历史轨迹点列表
        /// </summary>
        public List<EntityLocationPoint> points { get; set; }
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
}