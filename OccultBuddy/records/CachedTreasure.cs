﻿using System;
using FFXIVClientStructs.FFXIV.Common.Math;

namespace OccultBuddy.models;

public record CachedTreasure(ulong gameObjectId, Vector3 pos)
{
    
    public ulong GameObjectId { get; } = gameObjectId;
    public Vector3 Pos { get; } = pos;
    
    
    public override int GetHashCode()
    {
        return HashCode.Combine(GameObjectId, Pos);
    }
    

    public virtual bool Equals(CachedTreasure? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return GameObjectId == other.GameObjectId;
    }
}
