#region

using GameNetcodeStuff;
using Hax;

#endregion

[DebugCommand("fixcamera")]
class FixCameraCommand : ICommand {
    public void Execute(StringArray _) {
        if (Helper.LocalPlayer is not PlayerControllerB localPlayer) return;

        Helper.Terminal?
            .Reflect()
            .GetInternalField<InteractTrigger>("terminalTrigger")?
            .Interact(localPlayer.transform);
    }
}
