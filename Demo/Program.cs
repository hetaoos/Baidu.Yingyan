using io.nulldata.Baidu.Yingyan;
using io.nulldata.Baidu.Yingyan.Track;
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
            var ak = "jmH6fA2QzDWfyPvkKslL741L";
            var service_id = "105686";
            YingyanApi api = new YingyanApi(ak, service_id);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("col1", "1000");
            dic.Add("col2", "200");
            //api.entity.add("aaaa", dic).GetAwaiter().GetResult();
            //var t = api.entity.addcolumn("hello", "col1", "col1", true).GetAwaiter().GetResult();
            //var t = api.entity.list_column().GetAwaiter().GetResult();
            //var t = api.track.addpoint("hello", new TrackPoint()
            //{
            //    latitude = 11.121,
            //    longitude = 22.222,
            //    coord_type = coord_type.GPS,
            //    loc_time = DateTime.Now,
            //}).Result;


            var t1 = api.track.add_column("col1", "测试col", TrackColumnType.String).Result;
            var t22 = api.track.list_column().Result;
            var t2 = api.track.gethistory_simple("hello", DateTime.Now.AddDays(-0.9), DateTime.Now).Result;
            Console.ReadKey();
        }
    }
}
