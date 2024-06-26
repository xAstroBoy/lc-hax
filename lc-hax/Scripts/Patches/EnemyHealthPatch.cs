#pragma warning disable IDE1006

#region

using GameNetcodeStuff;
using HarmonyLib;
using Hax;

#endregion

[HarmonyPatch(typeof(EnemyAI), nameof(EnemyAI.HitEnemy))]
class EnemyHealthPatch {
    public static void Postfix(EnemyAI __instance, int force, PlayerControllerB playerWhoHit, bool playHitSFX,
        int hitID) {
        if (!playerWhoHit.IsSelf()) return;
        if (!__instance.CanEnemyDie()) return;
        if (__instance.isEnemyDead) return;
        Helper.SendFlatNotification(
            $"Remaining Hits to kill {__instance.enemyType.enemyName} : {__instance.enemyHP - 1}, You are hitting with Force : {force}");
    }
}
