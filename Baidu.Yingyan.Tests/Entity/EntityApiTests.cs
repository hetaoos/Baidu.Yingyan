using NUnit.Framework;
using io.nulldata.Baidu.Yingyan.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace io.nulldata.Baidu.Yingyan.Entity.Tests
{
    [TestFixture()]
    public class EntityApiTests
    {
        EntityApi api;
        string entity_name;
        string column_key;
        [OneTimeSetUp]
        public void Init()
        {
            var ak = "jmH6fA2QzDWfyPvkKslL741L";
            var service_id = "105686";
            api = new YingyanApi(ak, service_id).entity;
            entity_name = BitConverter.ToString(Guid.NewGuid().ToByteArray()).Replace("-", "");
            column_key = BitConverter.ToString(Guid.NewGuid().ToByteArray()).Replace("-", "");
        }
        /// <summary>
        /// 添加和删除实体
        /// </summary>
        [Test()]
        public void addAndDeleteTest()
        {
            var t = api.add(entity_name).Result;
            Assert.AreEqual(t.status, 0, "add entity fail.");
            Thread.Sleep(1000);
            t = api.add(entity_name).Result;
            Assert.AreNotEqual(t.status, 0, "add duplicate entity fail.");
            t = api.delete(entity_name).Result;
            Assert.AreEqual(t.status, 0, "delete entity fail.");
        }

        /// <summary>
        /// 添加和删除列
        /// </summary>
        [Test()]
        public void addAndDeleteColumn()
        {
            var t = api.add_column(column_key, "测试列", true).Result;
            Assert.AreEqual(t.status, 0, "add column fail.");
            Thread.Sleep(1000);
            t = api.add_column(column_key, "测试列", true).Result;
            Assert.AreNotEqual(t.status, 0, "add duplicate column fail.");

            var cols = api.list_column().Result;
            Assert.True(cols.columns.Any(o => o.column_key == column_key), "list error");

            t = api.delete_column(column_key).Result;
            Assert.AreEqual(t.status, 0, "delete column fail.");
            cols = api.list_column().Result;
            Assert.False(cols.columns.Any(o => o.column_key == column_key), "list error");
        }


    }
}