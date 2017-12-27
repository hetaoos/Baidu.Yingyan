using System.Collections.Generic;

namespace Baidu.Yingyan.Track
{
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
}