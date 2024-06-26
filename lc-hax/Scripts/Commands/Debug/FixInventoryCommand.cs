#region

using GameNetcodeStuff;
using Hax;

#endregion

[DebugCommand("fixinventory")]
class FixInventoryCommand : ICommand {
    public void Execute(StringArray _) {
        if (Helper.LocalPlayer is not PlayerControllerB player) return;
        Helper.Grabbables.ForEach(grabbable => {
            if (grabbable is not null) {
                if (player.HasItemInSlot(grabbable))
                    return; // only target the unheld items that are bugged in player's hand.
                if (grabbable.playerHeldBy == player)
                    grabbable.Detach();
                else if (grabbable.parentObject == player.localItemHolder)
                    grabbable.Detach();
                else if (grabbable.parentObject == player.serverItemHolder) grabbable.Detach();
            }
        });
    }
}
