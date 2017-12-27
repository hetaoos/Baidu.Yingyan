using Baidu.Yingyan.Converters;
using Newtonsoft.Json;

namespace Baidu.Yingyan.Fence
{
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
        public FenceBaseData[] fences { get; set; }
    }
}