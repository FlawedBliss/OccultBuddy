using System;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace OccultBuddy.Windows;

public class ConfigWindow : Window, IDisposable
{
    public ConfigWindow(Plugin plugin) : base("OccultBuddy Configuration")
    {
    }

    public override void Draw()
    {
        var autoAddMapMarkers = Plugin.Configuration.AutoAddTreasureMapMarkers;
        if(ImGui.Checkbox("Automatically add map markers for nearby treasure coffers", ref autoAddMapMarkers))
        {
            Plugin.Configuration.AutoAddTreasureMapMarkers = autoAddMapMarkers;
            Plugin.Configuration.Save();
        }
        var autoAddMiniMapMarkers = Plugin.Configuration.AutoAddTreasureMiniMapMarkers;
        if(ImGui.Checkbox("Automatically add minimap markers for nearby treasure coffers", ref autoAddMiniMapMarkers))
        {
            Plugin.Configuration.AutoAddTreasureMiniMapMarkers = autoAddMiniMapMarkers;
            Plugin.Configuration.Save();
        }
        
        var showToastOnTreasureFound = Plugin.Configuration.ShowToastOnTreasureFound;
        if(ImGui.Checkbox("Show toast notification when a treasure coffer is found", ref showToastOnTreasureFound))
        {
            Plugin.Configuration.ShowToastOnTreasureFound = showToastOnTreasureFound;
            Plugin.Configuration.Save();
        }
        
        var showChatMessageOnTreasureFound = Plugin.Configuration.ShowChatMessageOnTreasureFound;
        if(ImGui.Checkbox("Show chat message when a treasure coffer is found", ref showChatMessageOnTreasureFound))
        {
            Plugin.Configuration.ShowChatMessageOnTreasureFound = showChatMessageOnTreasureFound;
            Plugin.Configuration.Save();
        }
    }

    public void Dispose()
    {
       
    }
}
