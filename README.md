# Baidu.Yingyan
百度 [鹰眼轨迹服务](https://lbsyun.baidu.com/index.php?title=yingyan) C# 版本 SDK，基于 [鹰眼 Web服务 API v3.0](https://lbsyun.baidu.com/index.php?title=yingyan/api/v3/all) 实现。

## 已实现接口
- [终端管理](https://lbsyun.baidu.com/index.php?title=yingyan/api/v3/entity)
- [实时位置搜索](https://lbsyun.baidu.com/index.php?title=yingyan/api/v3/entitysearch)
- [轨迹上传](https://lbsyun.baidu.com/index.php?title=yingyan/api/v3/trackupload)
- [轨迹查询和纠偏](https://lbsyun.baidu.com/index.php?title=yingyan/api/v3/trackprocess)
- [轨迹分析](https://lbsyun.baidu.com/index.php?title=yingyan/api/v3/analysis)
- [地理围栏管理](https://lbsyun.baidu.com/index.php?title=yingyan/api/v3/geofence)
- [地理围栏报警](https://lbsyun.baidu.com/index.php?title=yingyan/api/v3/geofencealarm)
- [批量导出轨迹](https://lbsyun.baidu.com/index.php?title=yingyan/api/v3/trackexport)

## NuGet

[https://www.nuget.org/packages/Baidu.Yingyan/1.1.0-alpha2](https://www.nuget.org/packages/Baidu.Yingyan/1.1.0-alpha2)
```powershell
PM> Install-Package Baidu.Yingyan -Version 1.1.0-alpha2
```

## 使用

```csharp
var ak = "YOUR_AK";
var service_id = "YOUR_SERVICE_ID";
// 初始化api
var api = new YingyanApi(ak, service_id);
// entity name
var name = "name001";
// 新增一个 entity
var r1 = await api.entity.add(name, "测试");
// 添加一个位置点
var r2 = await api.track.addpoint(new TrackPoint()
{
    latitude = 11.111,
    longitude = 22.333,
    coord_type_input = CoordTypeEnums.bd09ll,
    entity_name = name,
    loc_time = DateTime.Now,
});
// 查询轨迹
var r3 = await api.track.gettrack(new TrackHistoryGetTrackParam() {
    entity_name = name, 
    start_time = DateTime.Now.AddDays(-1), 
    end_time = DateTime.Now 
});
```

## 最近更新
- 2017/12/30
  - 增加sn验证，官方的[sn计算算法](https://lbsyun.baidu.com/index.php?title=webapi/appendix)的示例代码是个坑！
  - 将参数集合类型从 NameValueCollection 改为 Dictionary

- 2017/12/27
  - 基于 [鹰眼 Web服务 API v3.0](http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/all) 全新实现，与上一个版本不兼容，需要适配。
  - 目前还未测试完全。
