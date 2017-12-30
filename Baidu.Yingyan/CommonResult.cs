using System.ComponentModel;

namespace Baidu.Yingyan
{
    /// <summary>
    /// 通用返回结果
    /// </summary>
    public class CommonResult
    {
        /// <summary>
        /// 返回状态，0为成功
        /// </summary>
        public StatusCodeEnums status { get; set; }

        /// <summary>
        /// 对status的中文描述
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("status={0}, message={1}", status, message);
        }
    }

    /// <summary>
    /// 状态码
    /// </summary>
    public enum StatusCodeEnums
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        ok = 0,

        /// <summary>
        /// 服务器内部错误
        /// </summary>
        [Description("服务器内部错误")]
        error1 = 1,

        /// <summary>
        /// http method错误
        /// </summary>
        [Description("http method错误")]
        error3 = 3,

        /// <summary>
        /// 上传内容超过8M
        /// </summary>
        [Description("上传内容超过8M")]
        error10 = 10,

        /// <summary>
        /// AK参数不存在
        /// </summary>
        [Description("AK参数不存在")]
        error101 = 101,

        /// <summary>
        /// MCODE参数不存在，mobile类型mcode参数必需
        /// </summary>
        [Description("MCODE参数不存在，mobile类型mcode参数必需")]
        error102 = 102,

        /// <summary>
        /// APP不存在，AK有误请检查再重试
        /// </summary>
        [Description("APP不存在，AK有误请检查再重试")]
        error200 = 200,

        /// <summary>
        /// APP被用户自己禁用，请在控制台解禁
        /// </summary>
        [Description("APP被用户自己禁用，请在控制台解禁	")]
        error201 = 201,

        /// <summary>
        /// APP被管理员删除
        /// </summary>
        [Description("APP被管理员删除")]
        error202 = 202,

        /// <summary>
        /// APP类型错误
        /// </summary>
        [Description("APP类型错误")]
        error203 = 203,

        /// <summary>
        /// APP IP校验失败
        /// </summary>
        [Description("APP IP校验失败")]
        error210 = 210,

        /// <summary>
        /// APP SN校验失败
        /// </summary>
        [Description("APP SN校验失败")]
        error211 = 211,

        /// <summary>
        /// APP Referer校验失败
        /// </summary>
        [Description("APP Referer校验失败")]
        error220 = 220,

        /// <summary>
        /// APP Mcode码校验失败
        /// </summary>
        [Description("APP Mcode码校验失败")]
        error230 = 230,

        /// <summary>
        /// APP 服务被禁用
        /// </summary>
        [Description("APP 服务被禁用")]
        error240 = 240,

        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("用户不存在")]
        error250 = 250,

        /// <summary>
        /// 用户被自己删除
        /// </summary>
        [Description("用户被自己删除")]
        error251 = 251,

        /// <summary>
        /// 用户被管理员删除
        /// </summary>
        [Description("用户被管理员删除")]
        error252 = 252,

        /// <summary>
        /// 服务不存在
        /// </summary>
        [Description("服务不存在")]
        error260 = 260,

        /// <summary>
        /// 服务被禁用
        /// </summary>
        [Description("服务被禁用")]
        error261 = 261,

        /// <summary>
        /// 永久配额超限，限制访问
        /// </summary>
        [Description("永久配额超限，限制访问")]
        error301 = 301,

        /// <summary>
        /// 天配额超限，限制访问
        /// </summary>
        [Description("天配额超限，限制访问")]
        error302 = 302,

        /// <summary>
        /// 当前并发量已经超过约定并发配额，限制访问
        /// </summary>
        [Description("当前并发量已经超过约定并发配额，限制访问")]
        error401 = 401,

        /// <summary>
        /// 当前并发量已经超过约定并发配额，并且服务总并发量也已经超过设定的总并发配额，限制访问
        /// </summary>
        [Description("当前并发量已经超过约定并发配额，并且服务总并发量也已经超过设定的总并发配额，限制访问")]
        error402 = 402,

        /// <summary>
        /// 指定 entity 不存在
        /// </summary>
        [Description("指定 entity 不存在")]
        error3003 = 3003,

        /// <summary>
        /// entity_name已存在
        /// </summary>
        [Description("entity_name已存在")]
        error3005 = 3005,

        /// <summary>
        /// 查询时间段内的轨迹点过多，无法进行纠偏，请缩短查询时间
        /// </summary>
        [Description("查询时间段内的轨迹点过多，无法进行纠偏，请缩短查询时间")]
        error3006 = 3006,

        /// <summary>
        /// 数据解析失败，数据中包含非utf8编码字符
        /// </summary>
        [Description("数据解析失败，数据中包含非utf8编码字符")]
        error3007 = 3007,

        /// <summary>
        /// loc_time 不可晚于当前服务端时间10分钟以上，即不支持上传未来的轨迹点
        /// </summary>
        [Description("loc_time 不可晚于当前服务端时间10分钟以上，即不支持上传未来的轨迹点")]
        error3100 = 3100,

        /// <summary>
        /// 上传轨迹点数量太大
        /// </summary>
        [Description("上传轨迹点数量太大")]
        error3101 = 3101,

        /// <summary>
        /// 起止时间差不可超过24小时
        /// </summary>
        [Description("起止时间差不可超过24小时")]
        error3200 = 3200,

        /// <summary>
        /// 结束时间不可小于开始时间
        /// </summary>
        [Description("结束时间不可小于开始时间")]
        error3201 = 3201,

        /// <summary>
        /// 监控对象的围栏个数超出范围，最多100个
        /// </summary>
        [Description("监控对象的围栏个数超出范围，最多100个")]
        error5101 = 5101,

        /// <summary>
        /// 监控对象不存在
        /// </summary>
        [Description("监控对象不存在")]
        error5102 = 5102,

        /// <summary>
        /// 监控对象上没有fence_id为XXX的围栏
        /// </summary>
        [Description("监控对象上没有fence_id为XXX的围栏")]
        error5103 = 5103,

        /// <summary>
        /// 指定fence_id不存在
        /// </summary>
        [Description("指定fence_id不存在")]
        error5104 = 5104,

        /// <summary>
        /// 查询时间段内的轨迹点过多，无法进行轨迹分析，请缩短查询时间
        /// </summary>
        [Description("查询时间段内的轨迹点过多，无法进行轨迹分析，请缩短查询时间")]
        error9001 = 9001,

        /// <summary>
        /// HTTP请求错误
        /// </summary>
        [Description("HTTP请求错误")]
        error999 = 999,
    }
}