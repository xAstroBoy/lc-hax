using System.Text.RegularExpressions;
using UnityEngine;
using Steamworks;
using Hax;

internal sealed class DisconnectMod : MonoBehaviour {
    bool IsShiftHeld { get; set; } = false;

    void OnEnable() {
        InputListener.OnShiftButtonHold += this.HoldShift;
        InputListener.OnF4Press += this.TryToDisconnect;
    }

    void OnDisable() {
        InputListener.OnShiftButtonHold -= this.HoldShift;
        InputListener.OnF4Press -= this.TryToDisconnect;
    }

    void HoldShift(bool isHeld) => this.IsShiftHeld = isHeld;

    SteamId? GetSteamIdFromClipboard() {
        string clipboardText = GUIUtility.systemCopyBuffer;

        return !string.IsNullOrWhiteSpace(clipboardText) && Regex.IsMatch(clipboardText, @"^\d{17}$")
            ? new SteamId { Value = ulong.Parse(clipboardText) }
            : null;
    }

    void TryToDisconnect() {
        if (!this.IsShiftHeld) return;
        if (Helper.LocalPlayer is null) return;

        GameNetworkManager.Instance.Disconnect();
        Setting.DisconnectedVoluntarily = true;
    }

    void TryToConnect() {
        if (!this.IsShiftHeld) return;

        SteamId? steamId = this.GetSteamIdFromClipboard() ?? Setting.ConnectedLobbyId;

        if (steamId is not SteamId lobbyId) {
            return;
        }

        GameNetworkManager.Instance.StartClient(lobbyId);
        GUIUtility.systemCopyBuffer = "";
    }
}