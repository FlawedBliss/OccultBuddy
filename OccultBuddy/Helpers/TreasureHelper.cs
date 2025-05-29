using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin.Services;

namespace OccultBuddy.Helpers;

public class TreasureHelper
{
    private static TreasureHelper? _instance;
    public static TreasureHelper Instance => _instance ??= new TreasureHelper();

    private readonly float
        updateRange = 100f; // coffers appear in the object table at 150 units away, 140 used as a safe range to update

    private readonly MathHelper MathHelper = MathHelper.Instance;
    private readonly MapHelper MapHelper = MapHelper.Instance;
    private readonly MessageHelper MessageHelper = MessageHelper.Instance;
    
    private TreasureHelper() { }
    private HashSet<IGameObject> nearbyTreasures = new();

    public void UpdateNearbyTreasures(IFramework framework)
    {
        if (!Plugin.PlayerInValidZone()) return;
        bool resetMarkers = false;
        // if we're in a valid zone this should not be null
        var playerPos = Plugin.ClientState.LocalPlayer!.Position;
        // remove all treasures that are in range but no longer visible
        var removed = nearbyTreasures.RemoveWhere((obj) =>
                                                      MathHelper.Distance2D(obj.Position, playerPos) < updateRange &&
                                                      (Plugin.ObjectTable.SearchById(obj.GameObjectId) is null
                                                          || !obj.IsTargetable)
        );
        if (removed > 0)
        {
            Plugin.Log.Debug($"Removed {removed} treasures from cache.");
            resetMarkers = true;
        }
        var treasures = Plugin.ObjectTable.Where(o => o.ObjectKind == ObjectKind.Treasure && !nearbyTreasures.Contains(o) && o.IsTargetable);
        foreach (var treasure in treasures)
        {
            Plugin.Log.Debug($"Found new treasure, adding to cache. Now {nearbyTreasures.Count}");
            resetMarkers = true;
            nearbyTreasures.Add(treasure);
            if(Plugin.Configuration.ShowToastOnTreasureFound)
            {
                MessageHelper.ShowTreasureFoundToast(treasure);
            }
            if(Plugin.Configuration.ShowChatMessageOnTreasureFound)
            {
                MessageHelper.ShowTreasureFoundMessage(treasure);
            }
        }

        if (resetMarkers)
        {
            MapHelper.ResetMapMarkers();
            MapHelper.ResetMiniMapMarkers();
            if (Plugin.Configuration.AutoAddTreasureMapMarkers || Plugin.Configuration.AutoAddTreasureMiniMapMarkers)
            {
                foreach (var treasure in nearbyTreasures)
                {
                    if(Plugin.Configuration.AutoAddTreasureMapMarkers)
                    {
                        MapHelper.PlaceChestMapMarker(treasure.Position);
                    }
                    if(Plugin.Configuration.AutoAddTreasureMiniMapMarkers)
                    {
                        MapHelper.PlaceChestMiniMapMarker(treasure.Position);
                    }
                }
            }
        }
    }
}
