using Baidu.Yingyan.Entity;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 轨迹点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Entity.EntityLocationPoint" />
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
}