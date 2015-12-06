using io.nulldata.Baidu.Yingyan;
using io.nulldata.Baidu.Yingyan.Track;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Tests.Track
{
    [TestFixture()]
    public class TrackApiTests
    {
        TrackApi api;
        string entity_name1;
        string entity_name2;
        string column_key;
        [OneTimeSetUp]
        public void Init()
        {
            var ak = "jmH6fA2QzDWfyPvkKslL741L";
            var service_id = "105686";
            var yingyan = new YingyanApi(ak, service_id);
            api = yingyan.track;
            entity_name1 = "data1";
            entity_name2 = "data2";

            var r1 = yingyan.entity.add(entity_name1).Result;
            var r2 = yingyan.entity.delete(entity_name2).Result;
            var r3 = yingyan.entity.add(entity_name2).Result;


        }
        /// <summary>
        /// 上传数据
        /// </summary>
        [Test()]
        public void uploadData1()
        {
            uploadData(entity_name1, 100000);
        }
        /// <summary>
        /// 上传数据
        /// </summary>
        [Test()]
        public void uploadData2()
        {
            uploadData(entity_name2, 0);
        }

        private void uploadData(string entity_name, int skip = 0)
        {
            var path = Path.GetFullPath(Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "../../../",
                string.Format("data/{0}.txt", entity_name)));

            var lines = File.ReadAllLines(path);
            var points = lines.Select(o => o.Split(','))
                 .Where(o => o.Length >= 3)
                 .Select(o => new TrackPoint()
                 {
                     coord_type = CoordType.GPS,
                     loc_time = DateTime.Parse(o[0]),
                     longitude = double.Parse(o[1]),
                     latitude = double.Parse(o[2])
                 }).ToList(); ;
            var r = (DateTime.Now.AddDays(-1) - points[0].loc_time).TotalDays;
            points.ForEach(o => o.loc_time = o.loc_time.AddDays(r));
            int count = skip;
            int total = points.Count;
            foreach (var p in points.Skip(count))
            {
                var r2 = api.addpoint(entity_name, p).Result;
                count++;
                if (count % 100 == 0)
                    Console.WriteLine("count: {0} {1:p}", count, count / 1.0 / total);
            }
        }

    }
}
