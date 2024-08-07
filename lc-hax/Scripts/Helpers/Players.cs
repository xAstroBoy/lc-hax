#pragma warning disable CS8625

#region

using System;
using System.Linq;
using GameNetcodeStuff;
using Unity.Netcode;
using UnityEngine;

#endregion

namespace Hax;

static partial class Helper {
    /// <summary>
    ///     Gets the local player.
    /// </summary>
    internal static PlayerControllerB? LocalPlayer => GameNetworkManager?.localPlayerController.Unfake();

    /// <summary>
    ///     Gets the host player.
    /// </summary>
    internal static PlayerControllerB? HostPlayer => Players[0];

    /// <summary>
    ///     Gets all players (including non-initialized players & dead players).
    /// </summary>
    internal static PlayerControllerB[] Players => StartOfRound?.allPlayerScripts ?? [];

    /// <summary>
    ///     Gets the active players (players that are not dead).
    /// </summary>
    internal static PlayerControllerB[] ActivePlayers =>
        Players.Where(player => player.isPlayerControlled && !player.isPlayerDead).ToArray();

    /// <summary>
    ///     Gets the player ID as an integer.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>The player ID as an integer. Returns <c>-1</c> if the player is <c>null</c>.</returns>
    internal static int GetPlayerId(this PlayerControllerB player) => unchecked((int)player.GetPlayerID_ULong());

    /// <summary>
    ///     Gets the player ID as a ulong.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>The player ID as a ulong. Returns <c>(ulong)-1</c> if the player is <c>null</c>.</returns>
    internal static ulong GetPlayerID_ULong(this PlayerControllerB player) {
        if (player == null) return unchecked((ulong)-1);
        return player.IsSelf() ? player.actualClientId : player.playerClientId;
    }

    /// <summary>
    ///     Gets the player ID as a string.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>The player ID as a string.</returns>
    internal static string GetPlayerIdString(this PlayerControllerB player) => player.GetPlayerID_ULong().ToString();

    /// <summary>
    ///     Determines whether the player is the local player.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns><c>true</c> if the player is the local player; otherwise, <c>false</c>.</returns>
    internal static bool IsSelf(this PlayerControllerB? player) => LocalPlayer is PlayerControllerB localPlayer &&
                                                                   player?.actualClientId == localPlayer.actualClientId;

    /// <summary>
    ///     Damages the player using an RPC.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="damage">The damage amount.</param>
    internal static void DamagePlayerRpc(this PlayerControllerB player, int damage) =>
        player.DamagePlayerFromOtherClientServerRpc(damage, Vector3.zero, -1);

    /// <summary>
    ///     Heals the player.
    /// </summary>
    /// <param name="player">The player.</param>
    internal static void HealPlayer(this PlayerControllerB player) => player.DamagePlayerRpc(-100);

    /// <summary>
    ///     Kills the player.
    /// </summary>
    /// <param name="player">The player.</param>
    internal static void KillPlayer(this PlayerControllerB player) => player.DamagePlayerRpc(100);

    /// <summary>
    ///     Teleports the player to the entrance.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="outside">If set to <c>true</c> teleports outside; otherwise, teleports inside.</param>
    internal static void EntranceTeleport(this PlayerControllerB player, bool outside) {
        player.TeleportPlayer(RoundManager.FindMainEntranceScript(outside).entrancePoint.position);
        player.isInsideFactory = !outside;
    }

    /// <summary>
    ///     Gets a player by their name or ID.
    /// </summary>
    /// <param name="playerNameOrId">The player name or ID.</param>
    /// <returns>The player if found; otherwise, <c>null</c>.</returns>
    internal static PlayerControllerB? GetPlayer(string? playerNameOrId) {
        if (string.IsNullOrEmpty(playerNameOrId)) return null;

        PlayerControllerB[] players = Players;

        return players.First(player =>
                   player.playerUsername.ToLower().Contains(playerNameOrId.ToLower(),
                       StringComparison.InvariantCultureIgnoreCase)) ??
               players.First(player => player.GetPlayerIdString() == playerNameOrId);
    }

    /// <summary>
    ///     Gets a player by their client ID.
    /// </summary>
    /// <param name="playerClientId">The player client ID.</param>
    /// <returns>The player if found; otherwise, <c>null</c>.</returns>
    internal static PlayerControllerB? GetPlayer(ulong playerClientId) =>
        Players.First(player => player.playerClientId == playerClientId);

    /// <summary>
    ///     Gets a player by their client ID.
    /// </summary>
    /// <param name="playerClientId">The player client ID.</param>
    /// <returns>The player if found; otherwise, <c>null</c>.</returns>
    internal static PlayerControllerB? GetPlayer(int playerClientId) => GetPlayer(unchecked((ulong)playerClientId));

    /// <summary>
    ///     Gets an active player by their name or ID.
    /// </summary>
    /// <param name="playerNameOrId">The player name or ID.</param>
    /// <returns>The active player if found; otherwise, <c>null</c>.</returns>
    internal static PlayerControllerB? GetActivePlayer(string? playerNameOrId) {
        PlayerControllerB? player = GetPlayer(playerNameOrId);
        return player == null || player.IsDead() ? null : player;
    }

    /// <summary>
    ///     Gets an active player by their client ID.
    /// </summary>
    /// <param name="playerClientId">The player client ID.</param>
    /// <returns>The active player if found; otherwise, <c>null</c>.</returns>
    internal static PlayerControllerB? GetActivePlayer(int playerClientId) =>
        GetActivePlayer(playerClientId.ToString());

    /// <summary>
    ///     Checks if the player has a specific item in their slot.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="grabbable">The grabbable object.</param>
    /// <returns><c>true</c> if the player has the item in their slot; otherwise, <c>false</c>.</returns>
    internal static bool HasItemInSlot(this PlayerControllerB player, GrabbableObject grabbable) =>
        player.ItemSlots.Any(slot => slot == grabbable);

    /// <summary>
    ///     Checks if the player has free slots.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns><c>true</c> if the player has free slots; otherwise, <c>false</c>.</returns>
    internal static bool HasFreeSlots(this PlayerControllerB player) => player.ItemSlots.Any(slot => slot == null);

    /// <summary>
    ///     Checks if the player is holding a specific grabbable object.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="grabbable">The grabbable object.</param>
    /// <returns><c>true</c> if the player is holding the grabbable object; otherwise, <c>false</c>.</returns>
    internal static bool IsHoldingGrabbable(this PlayerControllerB player, GrabbableObject grabbable) =>
        player.ItemSlots[player.currentItemSlot] == grabbable;

    /// <summary>
    ///     Checks if the player is holding an item of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the grabbable object.</typeparam>
    /// <param name="player">The player.</param>
    /// <returns><c>true</c> if the player is holding an item of the specified type; otherwise, <c>false</c>.</returns>
    internal static bool IsHoldingItemOfType<T>(this PlayerControllerB player) where T : GrabbableObject =>
        player.ItemSlots[player.currentItemSlot] is T;

    /// <summary>
    ///     Gets the slot of a specific item.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="grabbable">The grabbable object.</param>
    /// <returns>The slot index of the item.</returns>
    internal static int GetSlotOfItem(this PlayerControllerB player, GrabbableObject grabbable) =>
        Array.IndexOf(player.ItemSlots, grabbable);

    /// <summary>
    ///     Grabs an object.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="grabbable">The grabbable object.</param>
    /// <returns><c>true</c> if the object was successfully grabbed; otherwise, <c>false</c>.</returns>
    internal static bool GrabObject(this PlayerControllerB player, GrabbableObject grabbable) {
        if (!player.HasFreeSlots()) return false;
        NetworkObjectReference networkObject = grabbable.NetworkObject;
        _ = player.Reflect().InvokeInternalMethod("GrabObjectServerRpc", networkObject);

        grabbable.parentObject = player.localItemHolder;
        grabbable.GrabItemOnClient();

        return true;
    }

    /// <summary>
    ///     Discards an object.
    /// </summary>
    /// <param name="localPlayer">The local player.</param>
    /// <param name="item">The item to discard.</param>
    /// <param name="placeObject">If set to <c>true</c>, places the object; otherwise, discards it.</param>
    /// <param name="parentObjectTo">The parent object to attach to.</param>
    /// <param name="placePosition">The position to place the object.</param>
    /// <param name="matchRotationOfParent">If set to <c>true</c>, matches the rotation of the parent object.</param>
    internal static void DiscardObject(this PlayerControllerB localPlayer, GrabbableObject item,
        bool placeObject = false, NetworkObject? parentObjectTo = null, Vector3 placePosition = default,
        bool matchRotationOfParent = true) {
        if (!localPlayer.IsHoldingGrabbable(item)) return;
        int slot = localPlayer.GetSlotOfItem(item);
        if (slot == -1) return;
        _ = localPlayer.Reflect().InvokeInternalMethod("SwitchToItemSlot", slot, null);
        localPlayer.DiscardHeldObject(placeObject, parentObjectTo, placePosition, matchRotationOfParent);
        item.Detach();
        RemoveItemFromHud(slot);
    }

    /// <summary>
    ///     Determines if the specified player controller instance is dead.
    /// </summary>
    /// <param name="instance">The player controller instance.</param>
    /// <returns>
    ///     True if the player controller instance is dead or null, otherwise returns the value of
    ///     <see cref="PlayerControllerB.isPlayerDead" />.
    /// </returns>
    internal static bool IsDead(this PlayerControllerB? instance) => instance?.isPlayerDead ?? true;

    /// <summary>
    ///     Determines whether the player is controlled.
    /// </summary>
    /// <param name="instance">The player instance.</param>
    /// <returns>
    ///     False if the player controller instance is dead or null, otherwise returns the value of
    ///     <see cref="PlayerControllerB.isPlayerControlled" />.
    /// </returns>
    internal static bool IsControlled(this PlayerControllerB? instance) => instance?.isPlayerControlled ?? false;

    /// <summary>
    ///     Determines whether the player is Deactivated (dead and not controlled).
    /// </summary>
    /// <param name="instance">The player instance.</param>
    /// <returns>
    ///     True if the player controller instance is null, otherwise Checks for
    ///     <see cref="PlayerControllerB.isPlayerDead" /> and not <see cref="PlayerControllerB.isPlayerControlled" />.
    /// </returns>
    internal static bool IsDeadAndNotControlled(this PlayerControllerB? instance) => instance == null || instance.IsDead() && !instance.IsControlled();

    /// <summary>
    ///     Gets a player from a body.
    /// </summary>
    /// <param name="body">The ragdoll grabbable object representing the body.</param>
    /// <returns>The player if found; otherwise, <c>null</c>.</returns>
    internal static PlayerControllerB? GetPlayerFromBody(this RagdollGrabbableObject body) =>
        GetPlayer(body.bodyID.Value);

    /// <summary>
    ///     Retrieves the username of the specified player.
    /// </summary>
    /// <param name="player">The player controller.</param>
    /// <returns>The username of the player, or <c>null</c> if the player is not initialized or the username is not set.</returns>
    internal static string GetPlayerUsername(this PlayerControllerB? player) {
        if (player == null) return null;
        return player.playerUsername == "Player" ? null : player.playerUsername;
    }
    /// <summary>
    /// A helper method to respawn the local player.
    /// </summary>
    internal static void RespawnLocalPlayer() {
        if (Helper.SoundManager is not SoundManager soundManager) return;
        if (Helper.StartOfRound is not StartOfRound startOfRound) return;
        if (Helper.HUDManager is not HUDManager hudManager) return;
        if (Helper.LocalPlayer is not PlayerControllerB localPlayer) return;

        startOfRound.allPlayersDead = false;
        localPlayer.ResetPlayerBloodObjects(localPlayer.isPlayerDead);

        if (localPlayer.isPlayerDead || localPlayer.isPlayerControlled) {
            localPlayer.isClimbingLadder = false;
            localPlayer.ResetZAndXRotation();
            localPlayer.thisController.enabled = true;
            localPlayer.health = 100;
            localPlayer.disableLookInput = false;

            if (localPlayer.isPlayerDead) {
                localPlayer.isPlayerDead = false;
                localPlayer.isPlayerControlled = true;
                localPlayer.isInElevator = true;
                localPlayer.isInHangarShipRoom = true;
                localPlayer.isInsideFactory = false;
                localPlayer.parentedToElevatorLastFrame = false;
                startOfRound.SetPlayerObjectExtrapolate(false);
                localPlayer.TeleportPlayer(startOfRound.playerSpawnPositions[0].position);
                localPlayer.setPositionOfDeadPlayer = false;
                localPlayer.DisablePlayerModel(startOfRound.allPlayerObjects[localPlayer.GetPlayerId()], true, true);
                localPlayer.helmetLight.enabled = false;
                localPlayer.Crouch(false);
                localPlayer.criticallyInjured = false;

                if (localPlayer.playerBodyAnimator != null)
                    localPlayer.playerBodyAnimator.SetBool("Limp", false);

                localPlayer.bleedingHeavily = false;
                localPlayer.activatingItem = false;
                localPlayer.twoHanded = false;
                localPlayer.inSpecialInteractAnimation = false;
                localPlayer.freeRotationInInteractAnimation = false;
                localPlayer.disableSyncInAnimation = false;
                localPlayer.inAnimationWithEnemy = null;
                localPlayer.holdingWalkieTalkie = false;
                localPlayer.speakingToWalkieTalkie = false;
                localPlayer.isSinking = false;
                localPlayer.isUnderwater = false;
                localPlayer.sinkingValue = 0.0f;
                localPlayer.statusEffectAudio.Stop();
                localPlayer.DisableJetpackControlsLocally();
                localPlayer.mapRadarDotAnimator.SetBool("dead", false);

                if (localPlayer.IsOwner) {
                    hudManager.gasHelmetAnimator.SetBool("gasEmitting", false);
                    localPlayer.hasBegunSpectating = false;
                    hudManager.RemoveSpectateUI();
                    hudManager.gameOverAnimator.SetTrigger("revive");
                    localPlayer.hinderedMultiplier = 1f;
                    localPlayer.isMovementHindered = 0;
                    localPlayer.sourcesCausingSinking = 0;
                    localPlayer.reverbPreset = startOfRound.shipReverb;
                }
            }

            soundManager.earsRingingTimer = 0.0f;
            localPlayer.voiceMuffledByEnemy = false;
            soundManager.playerVoicePitchTargets[localPlayer.GetPlayerId()] = 1f;
            soundManager.SetPlayerPitch(1f, localPlayer.GetPlayerId());

            if (localPlayer.currentVoiceChatIngameSettings == null)
                startOfRound.RefreshPlayerVoicePlaybackObjects();

            if (localPlayer.currentVoiceChatIngameSettings != null) {
                if (localPlayer.currentVoiceChatIngameSettings.voiceAudio == null)
                    localPlayer.currentVoiceChatIngameSettings.InitializeComponents();

                if (localPlayer.currentVoiceChatIngameSettings.voiceAudio != null)
                    localPlayer.currentVoiceChatIngameSettings.voiceAudio.GetComponent<OccludeAudio>().overridingLowPass = false;
            }
        }

        localPlayer.bleedingHeavily = false;
        localPlayer.criticallyInjured = false;
        localPlayer.playerBodyAnimator.SetBool("Limp", false);
        localPlayer.health = 100;
        hudManager.UpdateHealthUI(100, false);
        localPlayer.spectatedPlayerScript = null;
        hudManager.audioListenerLowPass.enabled = false;
        startOfRound.SetSpectateCameraToGameOverMode(false, localPlayer);

        RagdollGrabbableObject[] objectsOfType = UnityEngine.Object.FindObjectsOfType<RagdollGrabbableObject>();
        for (int index = 0; index < objectsOfType.Length; ++index) {
            if (!objectsOfType[index].isHeld) {
                if (startOfRound.IsServer) {
                    if (objectsOfType[index].NetworkObject.IsSpawned)
                        objectsOfType[index].NetworkObject.Despawn();
                    else
                        UnityEngine.Object.Destroy(objectsOfType[index].gameObject);
                }
            }
            else if (objectsOfType[index].isHeld && objectsOfType[index].playerHeldBy != null) {
                objectsOfType[index].playerHeldBy.DropAllHeldItems();
            }
        }

        DeadBodyInfo[] deadBodies = UnityEngine.Object.FindObjectsOfType<DeadBodyInfo>();
        foreach (var deadBody in deadBodies) {
            UnityEngine.Object.Destroy(deadBody.gameObject);
        }

        startOfRound.livingPlayers = startOfRound.connectedPlayersAmount + 1;
        startOfRound.allPlayersDead = false;
        startOfRound.UpdatePlayerVoiceEffects();
        startOfRound.shipAnimator.ResetTrigger("ShipLeave");
    }
}
