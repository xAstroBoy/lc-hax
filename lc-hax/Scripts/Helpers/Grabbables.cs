using System.Collections.Generic;
using System.Linq;
using GameNetcodeStuff;
using UnityEngine;

namespace Hax;

internal static partial class Helper {
    internal static HashSet<GrabbableObject> Grabbables { get; } = Helper.LocalPlayer is null
        ? []
        : Helper.FindObjects<GrabbableObject>()
            .WhereIsNotNull()
            .Where(scrap => scrap.IsSpawned)
            .ToHashSet();


    internal static void ShootShotgun(this ShotgunItem item, Transform origin) {
        item.gunShootAudio.volume = 0.15f;
        item.shotgunRayPoint = origin;
        item.ShootGunAndSync(false);
    }

    internal static GrabbableObject? GetGrabbableFromGift(this GiftBoxItem giftBox) {
        GameObject? content = giftBox.Reflect().GetInternalField<GameObject>("objectInPresent");
        if (content == null) return null;
        return content.TryGetComponent(out GrabbableObject grabbable) ? grabbable : null;
    }

    internal static int GetGiftBoxActualValue(this GiftBoxItem giftBox) {
        return giftBox == null ? 0 : giftBox.Reflect().GetInternalField<int>("objectInPresentValue");
    }

    internal static string ToEspLabel(this GrabbableObject grabbable) {
        if (grabbable == null) return "";
        if (grabbable is RagdollGrabbableObject ragdollGrabbableObject) {
            PlayerControllerB? player = ragdollGrabbableObject.GetPlayerFromBody();
            return player == null ? "Body" : $"Body of {player.playerUsername}";
        }
        else if (grabbable is GiftBoxItem giftBox) {
            GrabbableObject? content = giftBox.GetGrabbableFromGift();
            if (content != null) {
                return $"Gift : ({content.itemProperties.itemName})";
            }
        }

        return grabbable.itemProperties.itemName;
    }

    internal static int GetScrapValue(this GrabbableObject grabbable) =>
        grabbable switch {
            null => 0,
            GiftBoxItem gift => gift.GetGiftBoxActualValue(),
            _ => grabbable.scrapValue,
        };
}
