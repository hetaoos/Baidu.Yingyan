namespace Baidu.Yingyan.Fence
{
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
}