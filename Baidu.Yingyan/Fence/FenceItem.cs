using io.nulldata.Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan.Fence
{
    /// <summary>
    /// 围栏的基本信息
    /// </summary>
    public abstract class FenceItemBase
    {
        /// <summary>
        /// 围栏ID，作为其唯一标识
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? fence_id { get; set; }

        /// <summary>
        /// 围栏名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 围栏描述
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 围栏创建者,必选，创建者的entity_name
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 监控对象列表,必选，被监控者的entity_name，使用英文逗号”,”分割，至少一个，最多五个。
        /// </summary>
        public abstract List<string> monitored_persons { get; set; }
        /// <summary>
        /// 观察者列表,必选，观察者的entity_name，使用英文逗号”,”分割，至少一个，最多五个。
        /// </summary>
        public abstract List<string> observers { get; set; }
        /// <summary>
        /// 围栏生效时间列表,必选，一天中的几点几分到 几点几分生效。至少含有一段生效时间，多个时间段使用分号”;”分隔。比如：“0820,0930;1030,1130”
        /// </summary>
        public abstract List<TimeRang> valid_times { get; set; }
        /// <summary>
        /// 围栏生效周期,必选，标识valid_times是否周期性生效，可以使用如下数值：1：不重复 2：工作日循环 3：周末循环 4：每天循环 5：自定义 当为5时，需要定义valid_days，标识在周几生效。
        /// </summary>
        public FenceValidCycle valid_cycle { get; set; }
        /// <summary>
        /// 围栏生效日期
        /// </summary>
        [JsonConverter(typeof(SimpleDateConverter))]
        public DateTime valid_date { get; set; }
        /// <summary>
        /// 围栏生效日期列表,1到7，分别表示周一到周日，当valid_cycle为5时必选。
        /// </summary>
        [JsonConverter(typeof(ArrayConverter<int>))]
        public List<int> valid_days { get; set; }
        /// <summary>
        /// 围栏的形状,必选，围栏有两种形状：1代表圆形和2代表多边形。目前只支持圆形。
        /// </summary>
        public FenceShape shape { get; set; }
        /// <summary>
        /// 坐标类型
        /// </summary>
        public CoordType coord_type { get; set; }
        /// <summary>
        /// 围栏圆心经纬度,shape为1时必选。格式为：经度,纬度。示例：116.4321,38.76623
        /// </summary>
        public abstract LocationPoint center { get; set; }
        /// <summary>
        /// 围栏半径,当shape=1时必选。单位：米，取值范围(0,5000]
        /// </summary>
        public double? radius { get; set; }
        /// <summary>
        /// 多边形
        /// </summary>
        public abstract List<LocationPoint> vertexes { get; set; }

        /// <summary>
        /// 围栏报警条件,可选。1：进入时触发提醒 2：离开时触发提醒 3：进入离开均触发提醒。默认值为3
        /// </summary>
        public FenceAlarmCondition alarm_condition { get; set; }

        public T CopyTo<T>(T t = null)
            where T : FenceItemBase, new()
        {
            if (t == null)
                t = new T();

            t.fence_id = this.fence_id;
            t.name = this.name;
            t.desc = this.desc;
            t.creator = this.creator;
            t.monitored_persons = this.monitored_persons;
            t.observers = this.observers;
            t.valid_times = this.valid_times;
            t.valid_cycle = this.valid_cycle;
            t.valid_date = this.valid_date;
            t.valid_days = this.valid_days;
            t.shape = this.shape;
            t.coord_type = this.coord_type;
            t.center = this.center;
            t.radius = this.radius;
            t.alarm_condition = this.alarm_condition;

            return t;
        }

    }

    /// <summary>
    /// 作为参数提交
    /// </summary>
    public class FenceItemAsArgs : FenceItemBase
    {
        /// <summary>
        /// 监控对象列表,必选，被监控者的entity_name，使用英文逗号”,”分割，至少一个，最多五个。
        /// </summary>
        [JsonConverter(typeof(ArrayConverter<string>))]
        public override List<string> monitored_persons { get; set; }
        /// <summary>
        /// 观察者列表,必选，观察者的entity_name，使用英文逗号”,”分割，至少一个，最多五个。
        /// </summary>
        [JsonConverter(typeof(ArrayConverter<string>))]
        public override List<string> observers { get; set; }
        /// <summary>
        /// 围栏生效时间列表,必选，一天中的几点几分到 几点几分生效。至少含有一段生效时间，多个时间段使用分号”;”分隔。比如：“0820,0930;1030,1130”
        /// </summary>
        [JsonConverter(typeof(TimeRangConverter))]
        public override List<TimeRang> valid_times { get; set; }

        /// <summary>
        /// 围栏圆心经纬度,shape为1时必选。格式为：经度,纬度。示例：116.4321,38.76623
        /// </summary>
        [JsonConverter(typeof(LocationPointToStringConverter))]
        public override LocationPoint center { get; set; }
        /// <summary>
        /// 多边形
        /// </summary>
        [JsonConverter(typeof(LocationPointToStringConverter))]
        public override List<LocationPoint> vertexes { get; set; }

        public Dictionary<string, string> ToDictionary(bool removeEmptyValue = false)
        {
            var str = JsonConvert.SerializeObject(this);
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
            if (removeEmptyValue)
                dic.Where(o => o.Value == null).Select(o => o.Key).ToList()
                      .ForEach(o => dic.Remove(o));
            return dic;

        }
    }
    /// <summary>
    /// 作为返回结果
    /// </summary>
    public class FenceItemAsResult : FenceItemBase
    {
        /// <summary>
        /// 监控对象列表,必选，被监控者的entity_name，使用英文逗号”,”分割，至少一个，最多五个。
        /// </summary>
        public override List<string> monitored_persons { get; set; }
        /// <summary>
        /// 观察者列表,必选，观察者的entity_name，使用英文逗号”,”分割，至少一个，最多五个。
        /// </summary>
        public override List<string> observers { get; set; }
        /// <summary>
        /// 围栏生效时间列表,必选，一天中的几点几分到 几点几分生效。至少含有一段生效时间，多个时间段使用分号”;”分隔。比如：“0820,0930;1030,1130”
        /// </summary>
        public override List<TimeRang> valid_times { get; set; }

        /// <summary>
        /// 围栏圆心经纬度,shape为1时必选。格式为：经度,纬度。示例：116.4321,38.76623
        /// </summary>
        public override LocationPoint center { get; set; }
        /// <summary>
        /// 多边形
        /// </summary>
        public override List<LocationPoint> vertexes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? create_time { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? modify_time { get; set; }
    }

    /// <summary>
    /// 围栏生效周期
    /// </summary>
    public enum FenceValidCycle
    {
        /// <summary>
        /// 不重复
        /// </summary>
        Not = 1,
        /// <summary>
        /// 工作日循环
        /// </summary>
        Weekday = 2,
        /// <summary>
        /// 周末循环
        /// </summary>
        Weekend = 3,
        /// <summary>
        /// 每天循环
        /// </summary>
        Day = 4,
        /// <summary>
        /// 自定义
        /// </summary>
        Custom = 5
    }
    /// <summary>
    /// 围栏报警条件
    /// </summary>
    public enum FenceAlarmCondition
    {
        /// <summary>
        /// 进入时触发提醒
        /// </summary>
        In = 1,
        /// <summary>
        /// 离开时触发提醒
        /// </summary>
        Out = 2,
        /// <summary>
        /// 进入离开均触发提醒
        /// </summary>
        InOrOut = 3
    }
    /// <summary>
    /// 围栏的形状
    /// </summary>
    public enum FenceShape
    {
        /// <summary>
        /// 圆形
        /// </summary>
        Circular = 1,
        /// <summary>
        /// 多边形(暂时不支持)
        /// </summary>
        Vertexes = 2
    }
    public class TimeRang
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeOfDay begin_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeOfDay end_time { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", begin_time == null ? "0000" : begin_time.ToString(), end_time == null ? "0000" : end_time.ToString());
        }
        public TimeRang()
        { }

        public TimeRang(string s)
        {
            Parse(s);
        }

        public void Parse(string s)
        {
            var arr = s.Split(',');
            begin_time = TimeOfDay.Parse(arr[0]);
            end_time = TimeOfDay.Parse(arr[1]);
        }
    }
    [JsonConverter(typeof(TimeOfDayConverter))]
    public class TimeOfDay
    {
        /// <summary>
        /// 小时
        /// </summary>
        public byte hour { get; set; }
        /// <summary>
        /// 分钟
        /// </summary>
        public byte minute { get; set; }

        public override string ToString()
        {
            if (hour < 0)
                hour = 0;
            else if (hour > 24)
                hour = 24;

            if (minute < 0)
                minute = 0;
            else if (minute > 59)
                minute = 59;
            return string.Format("{0:00}{1:00}", hour, minute);
        }

        public static TimeOfDay Parse(string s)
        {
            var v = int.Parse(s);
            v = v % 10000;
            return new TimeOfDay()
            {
                hour = (byte)(v / 100),
                minute = (byte)(v % 100),
            };

        }
    }
}
