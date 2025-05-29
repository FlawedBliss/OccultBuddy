using FFXIVClientStructs.FFXIV.Common.Math;

namespace OccultBuddy.Helpers;

public class MathHelper
{
    private static MathHelper? _instance;
    public static MathHelper Instance => _instance ??= new MathHelper();
    private MathHelper() { }

    public float Distance2D(Vector3 a, Vector3 b)
    {
        return (float)System.Math.Sqrt(System.Math.Pow(a.X - b.X, 2) + System.Math.Pow(a.Z - b.Z, 2));
    }
    
}
