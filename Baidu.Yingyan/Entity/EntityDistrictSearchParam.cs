using System.Collections.Specialized;
using System.ComponentModel;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    ///  行政区搜索
    /// </summary>
    public class EntityDistrictSearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 行政区划关键字;
        /// 支持中国范围内的国家、省、市、区/县名称。请尽量输入完整的行政区层级和名称，保证名称的唯一性。若输入的行政区名称匹配多个行政区，搜索会失败，将会返回匹配的行政区列表。
        /// 关键字示例： 中国 北京市 湖南省长沙市 湖南省长沙市雨花区
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 设置返回值的内容
        /// </summary>
        public EntityDistrictSearchReturnTypeEnums return_type { get; set; } = EntityDistrictSearchReturnTypeEnums.all;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("return_type", return_type.ToString());
            return nv;
        }
    }

    /// <summary>
    /// 行政区搜索返回结果类型
    /// </summary>
    public enum EntityDistrictSearchReturnTypeEnums
    {
        /// <summary>
        /// 仅返回 total，即符合本次检索条件的所有
        /// </summary>
        [Description("simple")]
        simple,

        /// <summary>
        /// 数量（若仅需行政区内entity数量，建议选择 simple，将提升检索性能）
        /// </summary>
        [Description("entity ")]
        entity,

        /// <summary>
        /// 返回全部结果
        /// </summary>
        [Description("all")]
        all,
    }
}