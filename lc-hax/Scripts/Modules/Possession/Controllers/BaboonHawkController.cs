#region

using Hax;
using Unity.Netcode;
using Vector3 = UnityEngine.Vector3;

#endregion


enum BaboonState {
    SCOUTING = 0,
    RETURNING = 1,
    AGGRESSIVE = 2
}

class BaboonHawkController : IEnemyController<BaboonBirdAI> {
    Vector3 OriginalCamp { get; set; } = Vector3.zero;
    Vector3 CustomCamp { get; } = new(1000.0f, 1000.0f, 1000.0f);

    public void OnDeath(BaboonBirdAI enemy) {
        if (enemy.heldScrap is not null) _ = enemy.Reflect().InvokeInternalMethod("DropHeldItemAndSync");
    }


    public void OnOutsideStatusChange(BaboonBirdAI enemy) => enemy.StopSearch(enemy.scoutingSearchRoutine, true);


    public void OnPossess(BaboonBirdAI _) {
        if (BaboonBirdAI.baboonCampPosition != this.CustomCamp) return;

        this.OriginalCamp = BaboonBirdAI.baboonCampPosition;
        BaboonBirdAI.baboonCampPosition = this.CustomCamp;
    }

    public void OnUnpossess(BaboonBirdAI _) {
        if (BaboonBirdAI.baboonCampPosition == this.OriginalCamp) return;
        BaboonBirdAI.baboonCampPosition = this.OriginalCamp;
    }

    public void UsePrimarySkill(BaboonBirdAI enemy) {
        if (enemy.heldScrap is null && enemy.FindNearbyItem(1.5f) is GrabbableObject grabbable) {
            this.GrabItemAndSync(enemy, grabbable);
            return;
        }

        if (enemy.heldScrap is ShotgunItem shotgun) {
            shotgun.ShootShotgun(enemy.transform);
            return;
        }

        enemy.heldScrap?.InteractWithProp();
    }

    public void UseSecondarySkill(BaboonBirdAI enemy) {
        if (enemy.heldScrap is null) return;
        _ = enemy.Reflect().InvokeInternalMethod("DropHeldItemAndSync");
    }

    public string GetPrimarySkillName(BaboonBirdAI enemy) => enemy.heldScrap is not null ? "Interact with Item" : "Grab Item";

    public string GetSecondarySkillName(BaboonBirdAI enemy) => enemy.heldScrap is null ? "" : "Drop Item";

    public void OnEnableAIControl(BaboonBirdAI enemy, bool enabled) {
        if (enabled)
            BaboonBirdAI.baboonCampPosition = this.OriginalCamp;
        else
            BaboonBirdAI.baboonCampPosition = this.CustomCamp;
    }

    void GrabItemAndSync(BaboonBirdAI enemy, GrabbableObject item) {
        if (!item.TryGetComponent(out NetworkObject netItem)) return;
        enemy.SetBehaviourState(BaboonState.RETURNING);
        _ = enemy.Reflect().InvokeInternalMethod("GrabItemAndSync", netItem);
    }
}
