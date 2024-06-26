#region

using Hax;
using UnityEngine;

#endregion

enum GiantState {
    DEFAULT = 0,
    CHASE = 1
}

class ForestGiantController : IEnemyController<ForestGiantAI> {
    readonly Vector3 camOffset = new(0, 8f, -8f);

    bool IsUsingSecondarySkill { get; set; } = false;

    public Vector3 GetCameraOffset(ForestGiantAI enemy) => this.camOffset;

    public void Update(ForestGiantAI enemy, bool isAIControlled) {
        if (!this.IsUsingSecondarySkill)
            enemy.SetBehaviourState(GiantState.DEFAULT);
        else
            enemy.SetBehaviourState(GiantState.CHASE);
    }


    public void OnSecondarySkillHold(ForestGiantAI enemy) {
        this.IsUsingSecondarySkill = true;
        enemy.SetBehaviourState(GiantState.CHASE);
    }

    public void ReleaseSecondarySkill(ForestGiantAI enemy) {
        this.IsUsingSecondarySkill = false;
        enemy.SetBehaviourState(GiantState.DEFAULT);
    }

    public bool IsAbleToMove(ForestGiantAI enemy) => !enemy.Reflect().GetInternalField<bool>("inEatingPlayerAnimation");

    public string GetSecondarySkillName(ForestGiantAI _) => "(HOLD) Chase";

    public bool CanUseEntranceDoors(ForestGiantAI _) => false;

    public float InteractRange(ForestGiantAI _) => 0f;

    public void OnUnpossess(ForestGiantAI enemy) => this.IsUsingSecondarySkill = false;

    public bool SyncAnimationSpeedEnabled(ForestGiantAI _) => false;

    public void OnOutsideStatusChange(ForestGiantAI enemy) {
        enemy.StopSearch(enemy.roamPlanet, true);
        enemy.StopSearch(enemy.searchForPlayers, true);
    }
}
