#pragma warning disable IDE1006

using HarmonyLib;
using Unity.Netcode;

[HarmonyPatch(typeof(HoarderBugAI))]
internal class HoarderBugAIFixPatch
{
    [HarmonyPatch(nameof(HoarderBugAI.HitEnemy))]
    private static void Prefix(HoarderBugAI __instance)
    {
        if (!__instance.isEnemyDead) return;
        if (!__instance.heldItem.itemGrabbableObject.TryGetComponent(out NetworkObject networkObject)) return;

        _ = __instance.Reflect().InvokeInternalMethod("DropItemAndCallDropRPC", networkObject, false);
    }
}