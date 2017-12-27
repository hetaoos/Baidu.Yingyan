namespace Baidu.Yingyan.Fence
{
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
}