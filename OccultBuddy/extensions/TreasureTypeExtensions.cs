using FFXIVClientStructs.FFXIV.Client.Game.Object;
using OccultBuddy.models;

namespace OccultBuddy.extensions;

public static class TreasureTypeExtensions
{
    public static uint GetMapIconId(this TreasureType treasureType)
    {
        return treasureType switch
        {
            TreasureType.BronzeCoffer => 60911,
            TreasureType.SilverCoffer => 60912,
            TreasureType.GoldCoffer => 60913,
            TreasureType.LuckyCarrot => 65100,
            _ => 60911
        };
    }

    public static string GetFriendlyName(this TreasureType treasureType)
    {
        return treasureType switch
        {
            TreasureType.BronzeCoffer => "Bronze Coffer",
            TreasureType.SilverCoffer => "Silver Coffer",
            TreasureType.GoldCoffer => "Gold Coffer",
            TreasureType.LuckyCarrot => "Lucky Carrot",
            _ => "Unknown Treasure"
        };
    }
}
