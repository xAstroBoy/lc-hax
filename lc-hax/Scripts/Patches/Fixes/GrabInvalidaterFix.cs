using System;
using System.Collections;
using GameNetcodeStuff;
using HarmonyLib;
using Hax;
using UnityEngine;

// Items tend to bug out sometimes, this is a fix to ignore the grab validation RPC flag.

[HarmonyPatch(typeof(PlayerControllerB), "GrabObjectClientRpc")]
class GrabInvalidaterFix
{
    private static void PostFix(ref PlayerControllerB __instance, ref bool ___grabInvalidated)
    {
        if (__instance.IsSelf())
        {
            if (___grabInvalidated)
            {
                Console.WriteLine("Grab invalidated flag has been ignored.");
                ___grabInvalidated = false;
            }
        }
    }
}