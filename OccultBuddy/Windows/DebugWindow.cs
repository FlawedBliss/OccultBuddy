using System;
using System.Linq;
using System.Numerics;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;
using ImGuiNET;
using OccultBuddy.Helpers;

namespace OccultBuddy.Windows;

public class DebugWindow : Window, IDisposable
{
    private string GoatImagePath;
    private Plugin Plugin;


    public DebugWindow(Plugin plugin)
        : base("OccultBuddy##f9gq45xw2qxbu3r", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
        Plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (!Plugin.PlayerInValidZone())
            return;
        ImGui.TextUnformatted($"pos: {Plugin.ClientState.LocalPlayer?.Position}");
        ImGui.BeginTable("OccultHelper#tabletable", 9);
        ImGui.TableSetupColumn("Name");
        ImGui.TableSetupColumn("Pos");
        ImGui.TableSetupColumn("MapPos");
        ImGui.TableSetupColumn("ObjectKind");
        ImGui.TableSetupColumn("SubKind");
        ImGui.TableSetupColumn("DataId");
        ImGui.TableSetupColumn("Distance");
        ImGui.TableSetupColumn("Dead");
        ImGui.TableSetupColumn("Targetable");
        ImGui.TableHeadersRow();
        foreach (var obj in Plugin.ObjectTable.Where(o => o.ObjectKind == ObjectKind.Treasure))
        {
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{obj.Name}");
            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{obj.Position}");
            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{obj.GetMapCoordinates()}");
            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{obj.ObjectKind}");
            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{obj.SubKind}");
            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{obj.DataId}");
            ImGui.TableNextColumn();
            ImGui.TextUnformatted(
            $"{MathHelper.Instance.Distance2D(Plugin.ClientState.LocalPlayer?.Position ?? Vector3.Zero, obj.Position)}");
            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{(obj.IsDead ? "Yes" : "No")}");
            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{(obj.IsTargetable ? "Yes" : "No")}");

        }

        ImGui.EndTable();

        var target = Plugin.ClientState.LocalPlayer?.TargetObject;
        if (target is not null)
        {
            ImGui.TextUnformatted(target.Name.TextValue);
            ImGui.TextUnformatted($"{target.DataId}");
            ImGui.TextUnformatted($"{target.ObjectKind}");
            ImGui.TextUnformatted($"{target.GetType()}");
        }
    }
}
