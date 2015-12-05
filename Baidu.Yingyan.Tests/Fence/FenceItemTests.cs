using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using io.nulldata.Baidu.Yingyan.Fence;
using io.nulldata.Baidu.Yingyan;
using Newtonsoft.Json;

namespace Baidu.Yingyan.Tests.Fence
{
    [TestFixture()]
    public class FenceItemTests
    {
        /// <summary>
        /// 添加和删除实体
        /// </summary>
        [Test()]
        public void FenceItemAsArgsTest()
        {
            var t = new FenceItemAsArgs()
            {
                center = new LocationPoint() { longitude = 110, latitude = 40 },
                vertexes = new LocationPoint[] {
                     new LocationPoint() { longitude = 111, latitude = 41},
                      new LocationPoint() { longitude = 112, latitude = 42 }
                }.ToList(),
                valid_date = DateTime.Now,
            };

            var json = JsonConvert.SerializeObject(t);

            Assert.True(json.Contains("\"110,40\""), "LocationPoint serialize fail.");
            Assert.True(json.Contains("\"111,41;112,42\""), "List<LocationPoint> serialize fail.");

            var t2 = JsonConvert.DeserializeObject<FenceItemAsArgs>(json);

            Assert.True(t2.center.latitude == t.center.latitude, "seserialize fail.");
            Assert.True(t2.vertexes[1].latitude == t.vertexes[1].latitude, "seserialize fail.");
        }
    }
}
