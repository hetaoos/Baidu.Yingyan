namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// 行政区搜索结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Entity.EntityListReault" />
    public class EntityDistrictSearchReault : EntityListReault
    {
        /// <summary>
        /// 关键字匹配的行政区划列表
        /// </summary>
        public string[] district_list { get; set; }
    }
}