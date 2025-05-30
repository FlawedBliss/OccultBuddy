using FFXIVClientStructs.FFXIV.Client.System.Framework;

namespace OccultBuddy.models;

public record IGameObjectInfo
{
    public IGameObjectInfo(string name, ulong gameObjectId, float positionX, float positionY, float positionZ, float mapPositionX,
                              float mapPositionY, float mapPositionZ, byte objectKind, uint subKind, ulong dataId,
                              long eorzeaTime, bool isEorzeaTimeOverridden)
    {
        Name = name;
        GameObjectId = gameObjectId;
        PositionX = positionX;
        PositionY = positionY;
        PositionZ = positionZ;
        MapPositionX = mapPositionX;
        MapPositionY = mapPositionY;
        MapPositionZ = mapPositionZ;
        ObjectKind = objectKind;
        SubKind = subKind;
        DataId = dataId;
        EorzeaTime = eorzeaTime;
        IsEorzeaTimeOverridden = isEorzeaTimeOverridden;
    }

    public string Name { get; init; }
    
    public ulong GameObjectId { get; init; }
    public float PositionX { get; init; }
    public float PositionY { get; init; }
    public float PositionZ { get; init; }
    public float MapPositionX { get; init; }
    public float MapPositionY { get; init; }
    public float MapPositionZ { get; init; }
    public byte ObjectKind { get; init; }
    public uint SubKind { get; init; }
    public ulong DataId { get; init; }
    public long EorzeaTime { get; init; }
    public bool IsEorzeaTimeOverridden { get; init; }

}
