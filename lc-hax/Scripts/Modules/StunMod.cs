using System.Linq;
using UnityEngine;
using GameNetcodeStuff;

namespace Hax;

public class StunMod : MonoBehaviour {
    void OnEnable() {
        InputListener.onLeftButtonPress += this.Stun;
    }

    void OnDisable() {
        InputListener.onLeftButtonPress -= this.Stun;
    }

    void Stun() {
        if (!Helper.Extant(Helper.LocalPlayer, out PlayerControllerB player)) return;

        Physics.OverlapSphere(player.transform.position, 25.0f, 524288)
               .Select(collider => collider.GetComponent<EnemyAICollisionDetect>())
               .Where(enemy => enemy != null)
               .ToList()
               .ForEach(enemy => enemy.mainScript.SetEnemyStunned(true));
    }
}