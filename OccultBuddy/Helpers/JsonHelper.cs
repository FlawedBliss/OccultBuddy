using FFXIVClientStructs.FFXIV.Common.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OccultBuddy.Helpers;

public class JsonHelper
{
    private static JsonHelper? _instance;
    public static JsonHelper Instance => _instance ??= new JsonHelper();

    public readonly JsonSerializerSettings Settings = new()
    {
        ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
    };


    private JsonHelper() { }
}
