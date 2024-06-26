#region

using Hax;
using UnityEngine;

#endregion

enum BeesState {
    IDLE,
    DEFENSIVE,
    ATTACK
}

class CircuitBeesController : IEnemyController<RedLocustBees> {
    Vector3 CamOffset { get; } = new(0, 2f, -3f);

    public Vector3 GetCameraOffset(RedLocustBees _) => this.CamOffset;

    public void UsePrimarySkill(RedLocustBees enemy) {
        enemy.SetBehaviourState(BeesState.ATTACK);
        enemy.EnterAttackZapModeServerRpc(-1);
    }

    public void UseSecondarySkill(RedLocustBees enemy) => enemy.SetBehaviourState(BeesState.IDLE);

    public void OnOutsideStatusChange(RedLocustBees enemy) => enemy.StopSearch(enemy.searchForHive, true);

    public string GetPrimarySkillName(RedLocustBees _) => "Attack";

    public string GetSecondarySkillName(RedLocustBees _) => "Idle";


}
