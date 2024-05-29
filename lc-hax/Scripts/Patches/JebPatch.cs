#pragma warning disable IDE1006

using HarmonyLib;

[HarmonyPatch(typeof(DepositItemsDesk), nameof(DepositItemsDesk.AttackPlayersServerRpc))]
internal class JebPatch
{
    private static void Prefix(DepositItemsDesk __instance, ref bool __state)
    {
        __state = __instance.inGrabbingObjectsAnimation;
        __instance.attacking = false;
        __instance.inGrabbingObjectsAnimation = false;
    }

    private static void Postfix(DepositItemsDesk __instance, bool __state)
    {
        __instance.inGrabbingObjectsAnimation = __state;
    }
}