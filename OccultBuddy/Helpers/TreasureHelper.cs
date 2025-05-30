using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Common.Math;
using Lumina.Data.Parsing;
using OccultBuddy.extensions;
using OccultBuddy.models;

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
    public HashSet<CachedTreasure> TreasureCache { get; private set; } = new();

    
    private bool IsTreasureInRange(CachedTreasure treasure)
    {
        if (!Plugin.PlayerInValidZone()) return false;
        return MathHelper.Distance2D(treasure.Pos, Plugin.ClientState.LocalPlayer!.Position) < updateRange;
    }
    private bool IsLuckyCarrot(CachedTreasure treasure)
    {
        return treasure.dataId == 2010139;
    }
    
    private bool IsTargetable(ulong gameObjectId)
    {
        return Plugin.ObjectTable.SearchById(gameObjectId)?.IsTargetable ?? false;
    }
    
    public void UpdateNearbyTreasures(IFramework framework)
    {
        if (!Plugin.PlayerInValidZone()) return;
        bool resetMarkers = false;
        // remove treasures that are loaded but not targetable - they have been picked up
        // exception for Lucky Carrots (dataId 2010139) which are not targetable but should not be removed
        var removed1 = TreasureCache.RemoveWhere(obj => IsTreasureInRange(obj) && 
                                                        !IsTargetable(obj.GameObjectId) &&
                                                        !IsLuckyCarrot(obj));

        if (removed1 > 0) Plugin.Log.Debug($"Removed {removed1} treasures from cache that are not targetable.");
        // remove treasures that are in range but not in the object table - they have despawned
        var removed2 = TreasureCache.RemoveWhere(obj => IsTreasureInRange(obj) &&
                                                        Plugin.ObjectTable.SearchById(obj.GameObjectId) is null);
        if (removed2 > 0)
            Plugin.Log.Debug($"Removed {removed1} treasures from cache that are not in the object table.");
        if (removed1 + removed2 > 0)
        {
            Plugin.Log.Debug($"Removed {removed1 + removed2} treasures from cache.");
            resetMarkers = true;
        }

        var treasures =
            Plugin.ObjectTable.Where(o => o.ObjectKind == ObjectKind.Treasure)
                  .Where(o => TreasureCache.All(c => c.GameObjectId != o.GameObjectId))
                  .Where(o => o.IsTargetable);
        
        var carrots = Plugin.ObjectTable.OfType<IEventObj>()
                            .Where(o => o.DataId == 2010139)
                            .Where(o => TreasureCache.All(c => c.GameObjectId != o.GameObjectId));
       
        foreach (var treasure in treasures.Concat(carrots))
        {
            resetMarkers = true;
            TreasureCache.Add(new CachedTreasure(treasure.GameObjectId, treasure.Position, treasure.DataId));
            Plugin.Log.Debug(
                $"Found new treasure ({treasure.GameObjectId},{treasure.ObjectKind}), adding to cache. Now {TreasureCache.Count}");
            if (Plugin.Configuration.ShowToastOnTreasureFound)
            {
                MessageHelper.ShowTreasureFoundToast(treasure);
            }

            if (Plugin.Configuration.ShowChatMessageOnTreasureFound)
            {
                MessageHelper.ShowTreasureFoundMessage(treasure);
            }

            if (Plugin.Configuration.EnableCrowdSourcing)
            {
                CrowdSourceHelper.Instance.SubmitTreasureCoffer(treasure);
            }
        }

        if (resetMarkers)
        {
            MapHelper.ResetMapMarkers();
            MapHelper.ResetMiniMapMarkers();
            if (Plugin.Configuration.AutoAddTreasureMapMarkers || Plugin.Configuration.AutoAddTreasureMiniMapMarkers)
            {
                foreach (var treasure in TreasureCache)
                {
                    if (Plugin.Configuration.AutoAddTreasureMapMarkers)
                    {
                        MapHelper.PlaceChestMapMarker(treasure.Pos, GetTypeFromDataId(treasure.dataId).GetMapIconId());
                    }

                    if (Plugin.Configuration.AutoAddTreasureMiniMapMarkers)
                    {
                        MapHelper.PlaceChestMiniMapMarker(treasure.Pos,
                                                          GetTypeFromDataId(treasure.dataId).GetMapIconId());
                    }
                }
            }
        }
    }

    public static TreasureType GetTypeFromDataId(ulong dataId)
    {
        return dataId switch
        {
            >= 1798 and < 1900 => TreasureType.BronzeCoffer, //this is a guess
            >= 1700 and < 1798 => TreasureType.SilverCoffer, //this too 
            2010139 => TreasureType.LuckyCarrot,
            _ => TreasureType.Unknown
        };
    }
}
