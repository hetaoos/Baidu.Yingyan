namespace Baidu.Yingyan
{
    /// <summary>
    /// 坐标系
    /// </summary>
    public enum CoordTypeEnums
    {
        /// <summary>
        /// 一种大地坐标系，也是目前广泛使用的GPS全球卫星定位系统使用的坐标系
        /// </summary>
        wgs84 = 1,

        /// <summary>
        /// 是由中国国家测绘局制订的地理信息系统的坐标系统。由WGS84坐标系经加密后的坐标系
        /// </summary>
        gcj02 = 2,

        /// <summary>
        /// 为百度坐标系，在GCJ02坐标系基础上再次加密。
        /// </summary>
        bd09ll = 3
    }

    /// <summary>
    /// 坐标系，等效于 CoordTypeEnums
    /// </summary>
    public enum CoordType2Enums
    {
        /// <summary>
        /// 一种大地坐标系，也是目前广泛使用的GPS全球卫星定位系统使用的坐标系
        /// </summary>
        COORD_TYPE_WGS84LL = 1,

        /// <summary>
        /// 是由中国国家测绘局制订的地理信息系统的坐标系统。由WGS84坐标系经加密后的坐标系
        /// </summary>
        COORD_TYPE_GCJ02LL = 2,

        /// <summary>
        /// 为百度坐标系，在GCJ02坐标系基础上再次加密。
        /// </summary>
        COORD_TYPE_BD09LL = 3
    }
}