using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Utility;
using OccultBuddy.extensions;

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
        return new SeStringBuilder()
                      .AddUiForeground(570)
                      .AddText($"{TreasureHelper.GetTypeFromDataId(treasure.DataId).GetFriendlyName()}: ")
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
