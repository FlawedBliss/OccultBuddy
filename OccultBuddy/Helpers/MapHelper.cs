using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Math;

namespace OccultBuddy.Helpers;

public class MapHelper
{
    private static MapHelper? _instance;
    public static MapHelper Instance => _instance ??= new MapHelper();
    private MapHelper() { }

    public unsafe void PlaceChestMapMarker(Vector3 pos, uint icon)
    {
        if (!AgentMap.Instance()->AddMapMarker(pos, icon))
        {
            Plugin.Log.Error("Failed to place map marker for chest at position: " + pos);
        }
    }
    public unsafe void PlaceChestMiniMapMarker(Vector3 pos, uint icon)
    {
        if (!AgentMap.Instance()->AddMiniMapMarker(pos, icon))
        {
            Plugin.Log.Error("Failed to place minimap marker for chest at position: " + pos);
        }
    }

    public unsafe void ResetMapMarkers()
    {
        AgentMap.Instance()->ResetMapMarkers();
    }
    
    public unsafe void ResetMiniMapMarkers()
    {
        AgentMap.Instance()->ResetMiniMapMarkers();
    }
    
}
