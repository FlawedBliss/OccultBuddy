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
        
        var enableCrowdSourcing = Plugin.Configuration.EnableCrowdSourcing;
        if(ImGui.Checkbox("Enable crowd sourcing for treasure coffers", ref enableCrowdSourcing))
        {
            Plugin.Configuration.EnableCrowdSourcing = enableCrowdSourcing;
            Plugin.Configuration.Save();
        }

        ImGui.SameLine();
        ImGui.TextUnformatted("(?)");
        if (ImGui.IsItemHovered())
        {
            ImGui.BeginTooltip();
            ImGui.TextUnformatted("Enabling crowd sourcing will send information about treasure coffers you find back to the developer.");
            ImGui.TextUnformatted("Only data related to ingame objects is sent, no information that is linked to your game or character in any way.");
        }
    }

    public void Dispose()
    {
       
    }
}
