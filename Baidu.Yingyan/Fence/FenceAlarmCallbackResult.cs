namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 报警服务端回调
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/geofencealarm">详情</a>
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class FenceAlarmCallbackResult
    {
        /// <summary>
        /// 消息类型
        /// 	取值1或者2
        /// 	1：当type=1时，校验该URL为有效URL。此时，content为空或不存在。开发者无需对消息内容做任何处理，只需正常返回即可。
        /// 	2：当type=2时，推送报警信息，开发者可取content中的内容做其他业务处理。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// service的唯一标识，当前报警信息所属的鹰眼服务ID
        /// </summary>
        public int service_id { get; set; }

        /// <summary>
        /// 报警信息数值
        /// </summary>
        public FenceAlarmHistory[] content { get; set; }
    }
}