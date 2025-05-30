using Dalamud.Configuration;
using System;

namespace OccultBuddy;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public bool AutoAddTreasureMapMarkers { get; set; } = true;
    public bool AutoAddTreasureMiniMapMarkers { get; set; } = true;
    
    public bool ShowToastOnTreasureFound { get; set; } = true;
    
    public bool ShowChatMessageOnTreasureFound { get; set; } = true;
    
    public bool EnableCrowdSourcing { get; set; } = false;
    
    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }
}
