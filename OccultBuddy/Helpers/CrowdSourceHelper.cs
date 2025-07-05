using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Utility;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Common.Math;
using Lumina.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OccultBuddy.models;
using ObjectKind = Dalamud.Game.ClientState.Objects.Enums.ObjectKind;

namespace OccultBuddy.Helpers;

public class CrowdSourceHelper
{
    private static CrowdSourceHelper? _instance;
    public static CrowdSourceHelper Instance => _instance ??= new CrowdSourceHelper();
    private CrowdSourceHelper() { }


    private unsafe long GetEorzeaTime()
    {
        return Framework.Instance()->ClientTime.EorzeaTime;
    }
    private unsafe bool IsEorzeaTimeOverridden()
    {
        return Framework.Instance()->ClientTime.IsEorzeaTimeOverridden;
    }
    public async void SubmitTreasureCoffer(IGameObject treasureCoffer)
    {
        var data = new IGameObjectInfo(
            treasureCoffer.Name.TextValue.Length > 0 ? treasureCoffer.Name.TextValue : "N/A",
            treasureCoffer.GameObjectId,
            treasureCoffer.Position.X,
            treasureCoffer.Position.Y,
            treasureCoffer.Position.Z,
            treasureCoffer.GetMapCoordinates().X,
            treasureCoffer.GetMapCoordinates().Y,
            treasureCoffer.GetMapCoordinates().Z,
            (byte)treasureCoffer.ObjectKind,
            treasureCoffer.SubKind,
            treasureCoffer.DataId,
            GetEorzeaTime(),
            IsEorzeaTimeOverridden()
        );
        var httpClient = new HttpClient();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://kimi.trashprojects.moe/treasure_coffer");
            request.Content = new StringContent(JsonConvert.SerializeObject(data, JsonHelper.Instance.Settings), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);
            httpClient.Dispose();
            if (!response.IsSuccessStatusCode)
            {
                Plugin.Log.Warning("Failed to submit treasure coffer data: " + response.ReasonPhrase);
            }
        } catch (Exception ex)
        {   
            Plugin.Log.Error("Failed to submit treasure coffer data: " + ex.Message);
        }
        
    }
}
