namespace Baidu.Yingyan
{
    /// <summary>
    /// 坐标点
    /// </summary>
    public class LocationPoint
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public double latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double longitude { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("lat={0}, lng={1}", latitude, longitude);
        }
    }
}