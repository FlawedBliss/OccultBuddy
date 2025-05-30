using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using OccultBuddy.Helpers;
using OccultBuddy.Windows;
using TerritoryType = Lumina.Excel.Sheets.TerritoryType;

namespace OccultBuddy;

public sealed class Plugin : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    [PluginService] internal static IDataManager DataManager { get; private set; } = null!;
    [PluginService] internal static IPluginLog Log { get; private set; } = null!;

    [PluginService]
    internal static IObjectTable ObjectTable { get; private set; } = null!;
    
    [PluginService] internal static IToastGui IToastGui { get; private set; } = null!;
    [PluginService] internal static IChatGui IChatGui { get; private set; } = null!;
    [PluginService] internal static IFramework IFramework { get; private set; } = null!;
    
    private const string CommandName = "/ob";

    public static Configuration Configuration { get; private set; }

    public readonly WindowSystem WindowSystem = new("OccultBuddy");
    private DebugWindow DebugWindow { get; init; }
    private ConfigWindow ConfigWindow { get; init; }
    
    public Plugin()
    {
        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        DebugWindow = new DebugWindow(this);
        ConfigWindow = new ConfigWindow(this);
        WindowSystem.AddWindow(DebugWindow);
        WindowSystem.AddWindow(ConfigWindow);
        
        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Opens the OccultBuddy configuration window."
        });


        PluginInterface.UiBuilder.Draw += DrawUI;
        

        PluginInterface.UiBuilder.OpenConfigUi += ConfigWindow.Toggle;
        IFramework.Update += TreasureHelper.Instance.UpdateNearbyTreasures;
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        DebugWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args)
    {
        if (args.Length == 0 || !args.Equals("debug"))
        {
            ToggleConfigUI();
        }
        else
        {
            ToggleDebugUI();
        }
    }
    

    private void DrawUI() => WindowSystem.Draw();

    public void ToggleDebugUI() => DebugWindow.Toggle();
    public void ToggleConfigUI() => ConfigWindow.Toggle();
    
    internal static bool PlayerInValidZone()
    {
        if (ClientState.LocalPlayer is null) return false;
        var territoryId = ClientState.TerritoryType;
        if (DataManager.GetExcelSheet<TerritoryType>().TryGetRow(territoryId, out var territoryRow))
        {
            return territoryRow.RowId == 1252;
        }

        return false;
    }
}
