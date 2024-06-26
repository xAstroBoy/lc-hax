#region

using GameNetcodeStuff;
using Hax;
using UnityEngine;

#endregion

public enum RadMechBehaviorState {
    Default,
    Alert,
    Flying
}

class RadMechController : IEnemyController<RadMechAI> {
    readonly Vector3 camOffset = new(0, 8f, -8f);

    bool isFiring = false;
    float shootTimer = 0f;

    public void OnMovement(RadMechAI enemy, bool isMoving, bool isSprinting) {
        bool inFlyingMode = enemy.Reflect().GetInternalField<bool>("inFlyingMode");
        if (inFlyingMode) return;
        if (isSprinting)
            enemy.timeBetweenSteps = 0.2f;
        else
            enemy.timeBetweenSteps = 0.7f;
    }

    public Vector3 GetCameraOffset(RadMechAI enemy) => this.camOffset;

    public bool IsAbleToMove(RadMechAI enemy) => !enemy.inTorchPlayerAnimation || !this.isFiring;

    public bool CanUseEntranceDoors(RadMechAI _) => false;

    public float InteractRange(RadMechAI _) => 0f;

    public bool SyncAnimationSpeedEnabled(RadMechAI _) => false;

    public void OnOutsideStatusChange(RadMechAI enemy) => enemy.StopSearch(enemy.searchForPlayers, true);

    public void UseSecondarySkill(RadMechAI enemy) {
        enemy.SetBehaviourState(RadMechBehaviorState.Alert);
        if (!enemy.spotlight.activeSelf)
            enemy.EnableSpotlight();
        else
            enemy.DisableSpotlight();
    }


    // set special ability to flying mode
    public void UseSpecialAbility(RadMechAI enemy) {
        bool inFlyingMode = enemy.Reflect().GetInternalField<bool>("inFlyingMode");
        if (!inFlyingMode) {
            enemy.SetBehaviourState(RadMechBehaviorState.Flying);
            enemy.StartFlying();
        }
        else
            enemy.EndFlight();
    }


    public void OnPrimarySkillHold(RadMechAI enemy) {
        enemy.SetBehaviourState(RadMechBehaviorState.Alert);
        PlayerControllerB player = enemy.FindClosestPlayer(4f);
        enemy.targetPlayer = player;
        enemy.targetedThreat = player.ToThreat();

        this.GetCurrentTarget(enemy);
        this.isFiring = true;
    }

    public void ReleasePrimarySkill(RadMechAI enemy) {
        if (this.isFiring) {
            enemy.SetBehaviourState(RadMechBehaviorState.Default);
            enemy.SetAimingGun(false);
            this.isFiring = false;
        }
    }

    public bool isHostOnly(RadMechAI _) => true;

    public void OnUnpossess(RadMechAI _) => this.isFiring = false;

    public void Update(RadMechAI enemy, bool isAIControlled) {
        if (isAIControlled) return;
        if (this.isFiring) {
            this.shootTimer += Time.deltaTime;
            if (!enemy.aimingGun) enemy.SetAimingGun(true);
            if (this.shootTimer >= enemy.fireRate) {
                this.shootTimer = 0f;
                enemy.StartShootGun();
            }
        }
    }

    public void GetCurrentTarget(RadMechAI enemy) {
        // if we have a player to target, else look for one
        if (enemy.targetPlayer is not null)
            enemy.targetedThreat = enemy.targetPlayer.ToThreat();
        else {
            enemy.targetPlayer = enemy.FindClosestPlayer(4f);
            enemy.targetedThreat = enemy.targetPlayer.ToThreat();
        }
    }

    public string GetPrimarySkillName(RadMechAI _) => "(HOLD) Fire weapon";

    public string GetSecondarySkillName(RadMechAI enemy)
    {
        enemy.SetBehaviourState(RadMechBehaviorState.Alert);
        if (!enemy.spotlight.activeSelf)
            return "Enable Spotlight";
        else {
            return "Disable Spotlight";
        }
    }
    

public string GetSpecialAbilityName(RadMechAI enemy) {
        bool inFlyingMode = enemy.Reflect().GetInternalField<bool>("inFlyingMode");
        if (!inFlyingMode) {
            return "Enable Flight";
        }
        else
            return "Disable Flight";
    }

}
