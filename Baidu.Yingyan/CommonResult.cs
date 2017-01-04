using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baidu.Yingyan
{
    public class CommonResult
    {
        /// <summary>
        /// 返回状态，0为成功
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 对status的中文描述
        /// </summary>
        public string message { get; set; }

        public override string ToString()
        {
            return string.Format("status={0}, message={1}", status, message);
        }
    }
}
