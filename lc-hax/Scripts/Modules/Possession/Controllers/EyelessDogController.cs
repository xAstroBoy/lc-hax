#region

using Hax;
using UnityEngine;

#endregion

enum DogState {
    ROAMING,
    SUSPICIOUS,
    CHASE,
    LUNGE
}

class EyelessDogController : IEnemyController<MouthDogAI> {
    readonly Vector3 camOffset = new(0, 3.2f, -4f);

    public Vector3 GetCameraOffset(MouthDogAI enemy) => this.camOffset;

    public void UsePrimarySkill(MouthDogAI enemy) =>
        enemy.SetBehaviourState(enemy.IsBehaviourState(DogState.CHASE) ? DogState.ROAMING : DogState.CHASE);

    public void UseSecondarySkill(MouthDogAI enemy) => enemy.SetBehaviourState(DogState.LUNGE);

    public void OnOutsideStatusChange(MouthDogAI enemy) => enemy.StopSearch(enemy.roamPlanet, true);
    public string GetPrimarySkillName(MouthDogAI enemy) => enemy.IsBehaviourState(DogState.CHASE) ? "Roam" : "Chase";

    public string GetSecondarySkillName(MouthDogAI _) => "Lunge";


}
