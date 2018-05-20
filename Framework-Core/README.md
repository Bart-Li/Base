.Net Core Framework Guide Line
======
简单描述： 
-------
>Newegg.EC.Core是基于.net core2.0 开发的一套公司内部使用的Framework API。在使用过程中欢迎提出更多的意见，以完善我们的Framework。

包含内容 ：
---------
>当前只包含以下内容，后续可根据实际项目需要再进行拓展，添加新的功能。每个功能的详细描述参考下方。
 1. IOC。
 2. Configuration。
 3. ServerMapping。
 4. ServiceList。
 5. RestfulClient。
 6. DataAccess。 

IOC：
---------
> ICO容器使用的是.net core自带的容器，系统在启动的时候，通过在Startup类的ConfigurationServices中，引入Framework的BaseService。可以自动实现IOC容器的注入。
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddBaseService()
        .AddMvc();            
}
```
> AddBaseService是IServiceCollection的一个扩展方法，里面当前包括两部分内容的注入：
```c#
public static IServiceCollection AddBaseService(this IServiceCollection services)
        {
            services.AddAutoSetupService().
                AddConfigurationService();
            return services;
        }
```
> 所有添加了AutoSetupService Attribute的类，都会被自动注入容器中。其中AutoSetupServiceAttribute有三个参数，
> LifeTime 包括（Singleton，Scope,Transient），默认Singleton。
> Priority 是优先级，依赖注入的时候，会选择优先级最高的对象。
> Service 具体的Service对象类型。
如：
```c#
    [AutoSetupService(typeof(IRestfulClient))]
    internal class DefaultRestfulClient : IRestfulClient
```    
> 如果需要使用一个Service，可以通过构造函数注入，或直接使用ECLibraryContainer.Get<TService>();TService是你需要的Service声明。


Configuration：
---------
> 框架目前支持从文件读取，或从Config Service（BTS提供的分布式配置服务）读取配置。由于.net core的项目后续发布基本都是Build成镜像，通过Docker部署，因此建议配置文件全部放置到Config Service中，在外部进行修改。如果使用文件配置，当每次更改配置项后，都需要重新Build Image，此方法不可取。
> 要使用配置，首先在appsettings.json中需要定义以下节点，appsettings.json自身会根据不同的环境加载各自的配置（如：appsettings.GQC.json，appsettings.E11.json）其中：
>> DefaultSystemName是Config service中定义的默认SystemName。
>> ZookeeperConfig是链接Config service的相关配置。
>> ConfigFileList是配置具体的配置文件列表，通过Key，Name设置，如果配置是从文件读取，从定义FilePath，反之，定义SystemName。
```json
{
  "SystemConfig": {
    "DefaultSystemName": "OrderService",
    "ZookeeperConfig": {
      "ConnectionString": "10.16.75.23:8481,10.16.75.25:8481,10.16.75.26:8481",
      "SessionTimeout": 20,
      "ConnectionTimeout": 10,
      "OperatingTimeout" :  60 
    },
    "ConfigFileList": [
      {
        "ConfigKey": "BusinessConfig",
        "ConfigName": "Business",
        "SystemName": "" 
      },
      {
        "ConfigKey": "SomeConfig",
        "ConfigName": "XXXX",
        "FilePath": "/Configuration/XXX.json"
      }
    ]
  }
}
```
> 框架对外提供IConfigurationManager接口，用于获取配置，相关接口如下，内部已经实现对配置文件的Watch，如果配置项有更改，会自动同步更新配置内容, Config Service支持Json，Xml，Text三种格式，默认是Json。
```C#
namespace Newegg.EC.Core.Configuration
{
    public interface IConfigurationManager
    {
        TConfig GetConfigFromServiceByKey<TConfig>(string configKey, NodeDataType nodeDataType = NodeDataType.Json) where TConfig : class, new();
        TConfig GetConfigFromService<TConfig>(string configName, NodeDataType nodeDataType = NodeDataType.Json) where TConfig : class, new();
        TConfig GetConfigFromService<TConfig>(string configName, string systemName, NodeDataType nodeDataType = NodeDataType.Json) where TConfig : class, new();
        TConfig GetConfigFromFileByKey<TConfig>(string configKey) where TConfig : class, new();
        TConfig GetSection<TConfig>(string sectionName) where TConfig : class, new();
    }
}
```

ServerMapping：
---------
> 由于考虑到我们会在同一台服务器上面部署运行多个Docker Container。并且Container内HostName可以自由定义，与宿主机并非一致。所以没有参考Newegg.EC的方式，使用ServerName进行Mapping。而是通过在创建每个Container的时候，指定一个Channel参数，来决定当前Container使用哪个Mapping。
> 获取Channel的方法已经封装，会从环境变量读取：Environment.GetEnvironmentVariable("Channel")，如果没有读取到，默认是TestChannel。
> ServerMapping的配置参考如下：
```json
{
    "ServerList":[
        { "Channel": "TestChannel", "QueryDB": "NEWSQL3", "HisQueryDB": "EHISSQL" },
        { "Channel": "E4Channel1", "QueryDB": "E4SHP01", "HisQueryDB": "E4HisQuery1" },
        { "Channel": "E4Channel2", "QueryDB": "E4SHP02", "HisQueryDB": "E4HisQuery2" },
        { "Channel": "E4Channel3", "QueryDB": "E4SHP03", "HisQueryDB": "E4HisQuery3" },
        { "Channel": "E4Channel4", "QueryDB": "E4SHP04", "HisQueryDB": "E4HisQuery4" },
        { "Channel": "E4Channel5", "QueryDB": "E4SHP01", "HisQueryDB": "E4HisQuery1" },
        { "Channel": "E4Channel6", "QueryDB": "E4SHP02", "HisQueryDB": "E4HisQuery2" }
    ]
}
```

ServiceList：
---------
> 配置所有Service的Host，ServiceHost配置参考如下：
```json
{
    "Services":[
        {
            "Name": "EmailService",
            "Host" :[
                { "Channel" : "TestChannel", "Address":"http://10.16.75.24:3000" },
                { "Channel" : "E4Channel1", "Address":"http://apis-e41.newegg.org" },
                { "Channel" : "E4Channel1", "Address":"http://apis-e42.newegg.org" },
                { "Channel" : "E4Channel2", "Address":"http://apis-e41.newegg.org" },
                { "Channel" : "E4Channel2", "Address":"http://apis-e42.newegg.org" }
            ]
        }
    ]
}
```

RestfulClient：
---------
> 提供调用API的接口，目前做了精简，请求时不会添加BizUnit等默认参数，后续如果需要可以拓展。RestfulService配置参考如下：
```json
{
    "DefaultTimeout" : "00:00:10",
    "DefaultMaxResponseSize": 20971520,
    "Resources" : [
        { "Key": "QueryEmailLog", "Verb": "Get", "Url": "/EmailService/Email", "Setting": "EmailServiceSetting" },
        { "Key": "SOService_AllDetail", "Verb": "Post", "Url": "/nesovendor/v2/customer/{CustomerNumber}/so", "Setting": "SOServiceSetting" }
    ],
    "SettingGroups" : [
        {
            "Key": "EmailServiceSetting",
            "Host": "EmailService",
            "Headers": [
                { "Name": "Content-Type", "Value": "application/json" },
                { "Name": "Accept", "Value": "application/json" },
                { "Name": "Authorization", "Value": "Bearer 6bAa4HFNqBSPgkFEtdrx2iOe4NBHt2TisWToSKqi" }
            ]
        }
    ]
}
```
> 框架提供的接口：IRestfulClient

DataAccess
---------
> 完全兼容原来的配置，使用XML格式，阅读性也高于Json。目前只支持SqlServer一种Database。
> 框架提供的接口：IDBManager
> Databases配置参考如下：
```xml
<DatabasesConfig xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <dbGroup name="SharedServer">
    <database name="MessageCenter">
      <connectionString>Server=S1DSQL01\ABS_SQL;database=MessageCenter;user id=EComDbo;password=Ecom2Dev</connectionString>
    </database>
  </dbGroup>
</DatabasesConfig>
```
> DataCommands配置参考如下：
```xml
<?xml version="1.0" encoding="utf-8" ?>
<DataCommandsConfig xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <dataCommand name="QueryEmailLog" commandType="Text" database="MessageCenter">
    <commandText>
      <![CDATA[
SELECT 
		MailLogID,SystemID,TemplateID,TemplateName,
		DataVariables AS Variables,CustomerNumber,SendTime,ToName,CCName,BCCName,SendServer,CountryCode,
		CompanyCode,LanguageCode,SONumber,RMANumber,InvoiceNumber
	FROM MessageCenter.dbo.UV_AllGeneralOutboundMessageLog WITH(NOLOCK)
	WHERE MessageType = 'E' and mailLogID = @MailLogID
      ]]>
    </commandText>
    <parameters>
      <param name="@MailLogID" dbType="AnsiString" size="50"/>
    </parameters>
  </dataCommand>
</DataCommandsConfig>
```

   
