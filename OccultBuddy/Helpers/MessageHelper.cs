using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Utility;
namespace OccultBuddy.Helpers;

public class MessageHelper
{
    private static MessageHelper? _instance;
    public static MessageHelper Instance => _instance ??= new MessageHelper();
    private MessageHelper() { }

    private SeString BuildTreasureFoundString(IGameObject treasure)
    {
        var xz = treasure.GetMapCoordinates();
        var link = SeString.CreateMapLink(Plugin.ClientState.TerritoryType, Plugin.ClientState.MapId,
                                          xz.X, xz.Y);
        var kind = treasure.DataId switch
        {
            >= 1798 and < 1900 => "Bronze", //this is a guess
            >= 1700 and < 1798 => "Silver", //this too 
            _ => "Unknown"
        };
        return new SeStringBuilder()
                      .AddUiForeground(570)
                      .AddText($"{kind} Treasure Coffer: ")
                      .AddUiGlowOff()
                      .AddUiForegroundOff()
                      .BuiltString
                      .Append(link);
    }

    public void ShowTreasureFoundMessage(IGameObject treasure)
    {
        
        Plugin.IChatGui.Print(BuildTreasureFoundString(treasure));
    }

    public void ShowTreasureFoundToast(IGameObject treasure)
    {
        Plugin.IToastGui.ShowQuest(BuildTreasureFoundString(treasure));
    }
    
}
