#region

using Hax;
using UnityEngine;

#endregion

enum JesterState {
    CLOSED,
    CRANKING,
    OPEN
}

class JesterController : IEnemyController<JesterAI> {
    public Vector3 camOffsets = new(0, 2.5f, -3f);
    public float transitionSpeed = 5f;

    public Vector3 GetCameraOffset(JesterAI enemy) {
        float targetCamOffsetY, targetCamOffsetZ;

        if (!enemy.IsBehaviourState(JesterState.OPEN)) {
            targetCamOffsetY = 2f;
            targetCamOffsetZ = -3f;
        }
        else {
            targetCamOffsetY = 2.3f;
            targetCamOffsetZ = -3.5f;
        }

        // Smoothly interpolate between current and target camera positions
        this.camOffsets.y = Mathf.Lerp(this.camOffsets.y, targetCamOffsetY, Time.deltaTime * this.transitionSpeed);
        this.camOffsets.z = Mathf.Lerp(this.camOffsets.z, targetCamOffsetZ, Time.deltaTime * this.transitionSpeed);

        return this.camOffsets;
    }

    public void UsePrimarySkill(JesterAI enemy) {
        enemy.SetBehaviourState(JesterState.CLOSED);
        this.SetNoPlayerChasetimer(enemy, 0.0f);
        enemy.mainCollider.isTrigger = false;
    }

    public void OnSecondarySkillHold(JesterAI enemy) {
        if (!enemy.IsBehaviourState(JesterState.CLOSED)) return;
        enemy.SetBehaviourState(JesterState.CRANKING);
    }

    public void ReleaseSecondarySkill(JesterAI enemy) {
        if (!enemy.IsBehaviourState(JesterState.CRANKING)) return;
        enemy.SetBehaviourState(JesterState.OPEN);
        enemy.mainCollider.isTrigger = true;
    }

    public void Update(JesterAI enemy, bool isAIControlled) => this.SetNoPlayerChasetimer(enemy, 100.0f);

    public void OnUnpossess(JesterAI enemy) {
        this.SetNoPlayerChasetimer(enemy, 25.0f);
        this.camOffsets = new Vector3(0, 2.5f, -3f);
    }

    public void OnPossess(JesterAI enemy) => this.camOffsets = new Vector3(0, 2.5f, -3f);


    public bool IsAbleToMove(JesterAI enemy) => !enemy.IsBehaviourState(JesterState.CRANKING);

    public bool IsAbleToRotate(JesterAI enemy) => !enemy.IsBehaviourState(JesterState.CRANKING);

    public string GetPrimarySkillName(JesterAI enemy) => enemy.IsBehaviourState(JesterState.CLOSED) ? "" : "Close box";

    public string GetSecondarySkillName(JesterAI enemy) => enemy.IsBehaviourState(JesterState.CLOSED) ? "(HOLD) Begin cranking" : "(RELEASE) Aggro Mode";

    public void OnOutsideStatusChange(JesterAI enemy) => enemy.StopSearch(enemy.roamMap, true);

    void SetNoPlayerChasetimer(JesterAI enemy, float value) =>
        enemy.Reflect().SetInternalField("noPlayersToChaseTimer", value);
}
