using System.Collections.Generic;

namespace Hax;

public static partial class Helper {
    public static List<EnemyAI> Enemies => Helper.RoundManager?.SpawnedEnemies ?? [];

    public static T? GetEnemy<T>() where T : EnemyAI =>
        Helper.Enemies.First(enemy => enemy is T) is T enemy ? enemy : null;
}
