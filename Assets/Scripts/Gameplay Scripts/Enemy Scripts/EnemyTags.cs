using System.Collections.Generic;
using UnityEngine;
public static class EnemyTags
{
    public static readonly HashSet<string> ValidEnemyTags = new HashSet<string>
    {
        "Enemy", "Tank Enemy"
    };

    public static bool IsEnemyTag(string tag)
    {
        return ValidEnemyTags.Contains(tag);
    }
}