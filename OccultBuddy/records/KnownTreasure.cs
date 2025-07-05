namespace OccultBuddy.models;

public class KnownTreasure
{
    public KnownTreasure(ulong dataId, float positionX, float positionY, float positionZ)
    {
        DataId = dataId;
        PositionX = positionX;
        PositionY = positionY;
        PositionZ = positionZ;
    }

    public ulong DataId { get; init; }
    public float PositionX { get; init; }
    public float PositionY { get; init; }
    public float PositionZ { get; init; }


}
