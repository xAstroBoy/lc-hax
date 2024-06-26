#region

using UnityEngine;

#endregion

interface IController {
    const float DefaultSprintMultiplier = 2.8f;

    const float DefaultInteractRange = 4.5f;

    static Vector3 DefaultCamOffsets => new(0, 2.5f, -3f);

    static Vector3 DefaultEnemyOffset => new();

    Vector3 GetCameraOffset(EnemyAI enemy);

    Vector3 GetEnemyPositionOffset(EnemyAI enemy);

    void OnEnableAIControl(EnemyAI enemy, bool enabled);

    void OnPossess(EnemyAI enemy);

    void OnUnpossess(EnemyAI enemy);

    void OnDeath(EnemyAI enemy);

    void Update(EnemyAI enemy, bool isAIControlled);

    void LateUpdate(EnemyAI enemy);

    void UsePrimarySkill(EnemyAI enemy);
    void OnPrimarySkillHold(EnemyAI enemy);

    void ReleasePrimarySkill(EnemyAI enemy);

    void OnSecondarySkillHold(EnemyAI enemy);

    void UseSecondarySkill(EnemyAI enemy);

    void ReleaseSecondarySkill(EnemyAI enemy);

    void UseSpecialAbility(EnemyAI enemy);

    void OnMovement(EnemyAI enemy, bool isMoving, bool isSprinting);

    bool IsAbleToMove(EnemyAI enemy);

    bool IsAbleToRotate(EnemyAI enemy);

    bool CanUseEntranceDoors(EnemyAI enemy);

    string? GetPrimarySkillName(EnemyAI enemy);

    string? GetSecondarySkillName(EnemyAI enemy);

    string? GetSpecialAbilityName(EnemyAI enemy);

    float InteractRange(EnemyAI enemy);

    bool isHostOnly(EnemyAI enemy);

    float SprintMultiplier(EnemyAI enemy);

    bool SyncAnimationSpeedEnabled(EnemyAI enemy);

    void OnOutsideStatusChange(EnemyAI enemy);
}

/// <summary>
///     Do Not forget to register the controller in the <see cref="PossessionMod.EnemyControllers" />
/// </summary>
/// <typeparam name="T"></typeparam>
interface IEnemyController<T> : IController where T : EnemyAI {
    void IController.OnEnableAIControl(EnemyAI enemy, bool enabled) => this.OnEnableAIControl((T)enemy, enabled);

    void IController.OnPossess(EnemyAI enemy) => this.OnPossess((T)enemy);

    void IController.OnUnpossess(EnemyAI enemy) => this.OnUnpossess((T)enemy);

    void IController.OnDeath(EnemyAI enemy) => this.OnDeath((T)enemy);

    void IController.Update(EnemyAI enemy, bool isAIControlled) => this.Update((T)enemy, isAIControlled);

    void IController.LateUpdate(EnemyAI enemy) => this.LateUpdate((T)enemy);

    void IController.UsePrimarySkill(EnemyAI enemy) => this.UsePrimarySkill((T)enemy);

    void IController.OnPrimarySkillHold(EnemyAI enemy) => this.OnPrimarySkillHold((T)enemy);

    void IController.ReleasePrimarySkill(EnemyAI enemy) => this.ReleasePrimarySkill((T)enemy);

    void IController.OnSecondarySkillHold(EnemyAI enemy) => this.OnSecondarySkillHold((T)enemy);

    void IController.UseSecondarySkill(EnemyAI enemy) => this.UseSecondarySkill((T)enemy);

    void IController.ReleaseSecondarySkill(EnemyAI enemy) => this.ReleaseSecondarySkill((T)enemy);

    void IController.UseSpecialAbility(EnemyAI enemy) => this.UseSpecialAbility((T)enemy);

    void IController.OnMovement(EnemyAI enemy, bool isMoving, bool isSprinting) =>
        this.OnMovement((T)enemy, isMoving, isSprinting);

    bool IController.IsAbleToMove(EnemyAI enemy) => this.IsAbleToMove((T)enemy);

    bool IController.IsAbleToRotate(EnemyAI enemy) => this.IsAbleToRotate((T)enemy);

    bool IController.CanUseEntranceDoors(EnemyAI enemy) => this.CanUseEntranceDoors((T)enemy);

    string? IController.GetPrimarySkillName(EnemyAI enemy) => this.GetPrimarySkillName((T)enemy);

    string? IController.GetSecondarySkillName(EnemyAI enemy) => this.GetSecondarySkillName((T)enemy);

    string? IController.GetSpecialAbilityName(EnemyAI enemy) => this.GetSpecialAbilityName((T)enemy);

    bool IController.isHostOnly(EnemyAI enemy) => this.isHostOnly((T)enemy);

    float IController.InteractRange(EnemyAI enemy) => this.InteractRange((T)enemy);

    float IController.SprintMultiplier(EnemyAI enemy) => this.SprintMultiplier((T)enemy);

    Vector3 IController.GetCameraOffset(EnemyAI enemy) => this.GetCameraOffset((T)enemy);

    Vector3 IController.GetEnemyPositionOffset(EnemyAI enemy) => this.GetEnemyPositionOffset((T)enemy);

    bool IController.SyncAnimationSpeedEnabled(EnemyAI enemy) => this.SyncAnimationSpeedEnabled((T)enemy);

    void IController.OnOutsideStatusChange(EnemyAI enemy) => this.OnOutsideStatusChange((T)enemy);

    void OnEnableAIControl(T enemy, bool enabled) {
    }

    void OnPossess(T enemy) {
    }

    void OnUnpossess(T enemy) {
    }

    void OnDeath(T enemy) {
    }

    void Update(T enemy, bool isAIControlled) {
    }

    void LateUpdate(T enemy) {
    }

    void UsePrimarySkill(T enemy) {
    }

    void OnPrimarySkillHold(T enemy) {
    }

    void ReleasePrimarySkill(T enemy) {
    }

    void OnSecondarySkillHold(T enemy) {
    }

    void UseSecondarySkill(T enemy) {
    }

    void ReleaseSecondarySkill(T enemy) {
    }

    void UseSpecialAbility(T enemy) {
    }

    void OnMovement(T enemy, bool isMoving, bool isSprinting) {
    }

    bool IsAbleToMove(T enemy) => true;

    bool IsAbleToRotate(T enemy) => true;

    bool CanUseEntranceDoors(T enemy) => true;

    string? GetPrimarySkillName(T enemy) => null;

    string? GetSecondarySkillName(T enemy) => null;

    string? GetSpecialAbilityName(T enemy) => null;

    bool isHostOnly(T enemy) => false;

    float InteractRange(T enemy) => DefaultInteractRange;

    float SprintMultiplier(T enemy) => DefaultSprintMultiplier;

    Vector3 GetCameraOffset(T enemy) => DefaultCamOffsets;

    Vector3 GetEnemyPositionOffset(T enemy) => DefaultEnemyOffset;

    bool SyncAnimationSpeedEnabled(T enemy) => true;

    void OnOutsideStatusChange(T enemy) {
    }
}
