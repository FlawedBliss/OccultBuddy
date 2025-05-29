using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Math;

namespace OccultBuddy.Helpers;

public class MapHelper
{
    private static MapHelper? _instance;
    public static MapHelper Instance => _instance ??= new MapHelper();
    private MapHelper() { }

    public unsafe void PlaceChestMapMarker(Vector3 pos)
    {
        if (!AgentMap.Instance()->AddMapMarker(pos, 60354))
        {
            Plugin.Log.Error("Failed to place map marker for chest at position: " + pos);
        }
    }
    public unsafe void PlaceChestMiniMapMarker(Vector3 pos)
    {
        if (!AgentMap.Instance()->AddMiniMapMarker(pos, 60354))
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
