namespace Hax;

static partial class Helper {
    internal static HUDManager? HUDManager => HUDManager.Instance.Unfake();

    internal static RoundManager? RoundManager => RoundManager.Instance.Unfake();

    internal static SoundManager? SoundManager => SoundManager.Instance.Unfake();

    internal static GameNetworkManager? GameNetworkManager => GameNetworkManager.Instance.Unfake();

    internal static StartOfRound? StartOfRound => StartOfRound.Instance.Unfake();

    internal static TimeOfDay? TimeOfDay => TimeOfDay.Instance.Unfake();

    internal static Terminal? Terminal => HUDManager?.Reflect().GetInternalField<Terminal>("terminalScript").Unfake();

    internal static IngamePlayerSettings? IngamePlayerSettings => IngamePlayerSettings.Instance.Unfake();
}
