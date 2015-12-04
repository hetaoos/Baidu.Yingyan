using io.nulldata.Baidu.Yingyan;
using io.nulldata.Baidu.Yingyan.Entity;
using io.nulldata.Baidu.Yingyan.Track;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = new Fence.TimeRang();
            var js = new io.nulldata.Baidu.Yingyan.Fence.FenceItemAsResult()
            {
                coord_type = CoordType.Baidu,
                monitored_persons = new List<string>(new string[] { "111", "222", "222" }),
                valid_times = new List<Fence.TimeRang>(new Fence.TimeRang[] { new Fence.TimeRang() { begin_time = new Fence.TimeOfDay() { hour = 10, minute = 1 } } }),
                valid_date = DateTime.Now,
                valid_days = new List<int>(new int[] { 1, 2, 3, 4 }),
                center = new LocationPoint() { latitude = 10, longitude = 100 }
            };
            var str = JsonConvert.SerializeObject(js);
            js = JsonConvert.DeserializeObject<io.nulldata.Baidu.Yingyan.Fence.FenceItemAsResult>(str);
            var dic2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
            var ak = "jmH6fA2QzDWfyPvkKslL741L";
            var service_id = "105686";
            YingyanApi api = new YingyanApi(ak, service_id);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("col1", "1000");
            dic.Add("col2", "200");
            //api.entity.add("aaaa", dic).GetAwaiter().GetResult();
            //var t = api.entity.addcolumn("hello", "col1", "col1", true).GetAwaiter().GetResult();
            //var t = api.entity.list_column().GetAwaiter().GetResult();
            var t = api.track.addpoints("hello", new TrackPoint()
            {
                latitude = 11.121,
                longitude = 22.222,
                coord_type = CoordType.GPS,
                loc_time = DateTime.Now,
            }).Result;

            var t1 = api.track.add_column("col1", "测试col", TrackColumnType.String).Result;
            var t22 = api.track.list_column().Result;
            var t2 = api.track.gethistory_simple("hello", DateTime.Now.AddDays(-0.9), DateTime.Now).Result;
            Console.ReadKey();
        }
    }
}
