using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baidu.Yingyan
{
    public enum CoordType
    {
        /// <summary>
        /// GPS经纬度坐标
        /// </summary>
        GPS = 1,
        /// <summary>
        /// 国测局加密经纬度坐标
        /// </summary>
        Gov = 2,
        /// <summary>
        /// 百度加密经纬度坐标
        /// </summary>
        Baidu = 3
    }
}
