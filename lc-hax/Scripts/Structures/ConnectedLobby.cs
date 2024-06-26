#region

using Steamworks;
using Steamworks.Data;

#endregion

readonly record struct ConnectedLobby {
    internal required Lobby Lobby { get; init; }
    internal required SteamId SteamId { get; init; }
}
