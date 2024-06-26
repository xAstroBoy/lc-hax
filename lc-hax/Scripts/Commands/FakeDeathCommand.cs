#region

using GameNetcodeStuff;
using Hax;
using UnityEngine;

#endregion

[Command("fakedeath")]
class FakeDeathCommand : ICommand {
    public void Execute(StringArray args) {
        if (Helper.LocalPlayer is not PlayerControllerB player) return;

        Setting.EnableFakeDeath = true;

        _ = player.Reflect().InvokeInternalMethod(
            "KillPlayerServerRpc",
            player.GetPlayerId(),
            true,
            Vector3.zero,
            CauseOfDeath.Unknown,
            0
        );

        Helper.CreateComponent<WaitForBehaviour>("Respawn")
            .SetPredicate(() => player.playersManager.shipIsLeaving)
            .Init(player.KillPlayer);
    }
}
