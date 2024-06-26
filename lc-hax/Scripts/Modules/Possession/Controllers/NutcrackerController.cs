#region

using Hax;
using UnityEngine;

#endregion

enum NutcrackerState {
    WALKING,
    SENTRY
}

class NutcrackerController : IEnemyController<NutcrackerEnemyAI> {
    readonly Vector3 CamOffset = new(0, 2.8f, -3f);

    bool InSentryMode { get; set; } = false;

    public Vector3 GetCameraOffset(NutcrackerEnemyAI enemy) => this.CamOffset;

    public void Update(NutcrackerEnemyAI enemy, bool isAIControlled) {
        if (isAIControlled) return;
        Reflector<NutcrackerEnemyAI> Nutcracker = enemy.Reflect();

        float timeSinceFiringGun = Nutcracker.GetInternalField<float>("timeSinceFiringGun");
        bool reloadingGun = Nutcracker.GetInternalField<bool>("reloadingGun");
        bool aimingGun = Nutcracker.GetInternalField<bool>("aimingGun");

        if (timeSinceFiringGun > 0.75f && enemy.gun.shellsLoaded <= 0 && !reloadingGun && !aimingGun) {
            enemy.ReloadGunServerRpc();
            enemy.SetBehaviourState(NutcrackerState.WALKING);
            this.InSentryMode = false;
        }

        if (this.InSentryMode) return;
        enemy.SetBehaviourState(NutcrackerState.WALKING);
    }

    public bool IsAbleToRotate(NutcrackerEnemyAI enemy) => !enemy.IsBehaviourState(NutcrackerState.SENTRY);

    public bool IsAbleToMove(NutcrackerEnemyAI enemy) => !enemy.IsBehaviourState(NutcrackerState.SENTRY);

    public void UsePrimarySkill(NutcrackerEnemyAI enemy) {
        bool reloadingGun = enemy.Reflect().GetInternalField<bool>("reloadingGun");
        if (enemy.gun is not ShotgunItem shotgun || enemy.gun.shellsLoaded <= 0 || reloadingGun) return;
        enemy.AimGunServerRpc(enemy.transform.forward);
        shotgun.gunShootAudio.volume = 0.25f;
        enemy.FireGunServerRpc();
    }

    public void OnSecondarySkillHold(NutcrackerEnemyAI enemy) {
        bool reloadingGun = enemy.Reflect().GetInternalField<bool>("reloadingGun");
        if (reloadingGun) return;
        enemy.SetBehaviourState(NutcrackerState.SENTRY);
        this.InSentryMode = true;
    }

    public void ReleaseSecondarySkill(NutcrackerEnemyAI enemy) {
        enemy.SetBehaviourState(NutcrackerState.WALKING);
        this.InSentryMode = false;
    }

    public void UseSpecialAbility(NutcrackerEnemyAI enemy) {
        Reflector<NutcrackerEnemyAI> Nutcracker = enemy.Reflect();
        bool reloadingGun = Nutcracker.GetInternalField<bool>("reloadingGun");
        int SaveTimesSeeingSamePlayer = Nutcracker.GetInternalField<int>("timesSeeingSamePlayer");
        int SaveHP = enemy.enemyHP;
        int SaveShellsLoaded = enemy.gun.shellsLoaded;
        if (enemy.IsBehaviourState(NutcrackerState.WALKING)) {
            _ = Nutcracker.SetInternalField("timesSeeingSamePlayer", 3);
            enemy.gun.shellsLoaded = 1;
            enemy.enemyHP = 1;
        }

        enemy.AimGunServerRpc(enemy.transform.position);
        _ = Nutcracker.SetInternalField("timesSeeingSamePlayer", SaveTimesSeeingSamePlayer);
        enemy.enemyHP = SaveHP;
        enemy.gun.shellsLoaded = SaveShellsLoaded;
    }

    public void OnUnpossess(NutcrackerEnemyAI enemy) => this.InSentryMode = false;

    public string GetPrimarySkillName(NutcrackerEnemyAI enemy) => "Fire weapon";

    public string GetSecondarySkillName(NutcrackerEnemyAI _) => "(HOLD) Activate Sentry Mode";

    public void OnOutsideStatusChange(NutcrackerEnemyAI enemy) {
        enemy.StopSearch(enemy.attackSearch, true);
        enemy.StopSearch(enemy.patrol, true);
    }
}
