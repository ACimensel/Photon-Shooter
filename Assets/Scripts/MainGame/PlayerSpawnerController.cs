using Fusion;
using UnityEngine;

public class PlayerSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef playerNetworkPrefab = NetworkPrefabRef.Empty;
    [SerializeField] private Transform[] spawnPoints;

    public override void Spawned()
    {
        //override Photon Spawned() to create the host player, not doing on Start because we need Runner to be setup and it is by the time Spawned() is called
        if (Runner.IsServer)
        {
            foreach (var item in Runner.ActivePlayers)
            {
                SpawnPlayer(item);
            }
        }
    }

    private void SpawnPlayer(PlayerRef playerRef) // PlayerRef is the input authority, the player which has authority to provide inputs to the spawned object
    {
        if (Runner.IsServer)
        {
            var index = playerRef % spawnPoints.Length;
            Vector3 spawnPoint = spawnPoints[index].transform.position;
            // this function could be called on a non-host client as well, and if Runner.Spawn is called from non-host client,
            // it will return null, but we don't need to check since we check if Runner.IsServer already
            var playerObject = Runner.Spawn(playerNetworkPrefab, spawnPoint, Quaternion.identity, playerRef);
            
            Runner.SetPlayerObject(playerRef, playerObject);
        }
    }

    private void DespawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            foreach (var player in Runner.ActivePlayers)
            {
                Debug.Log($"ACTEST Player ID: {player.PlayerId}, Player toString: {player.ToString()}");
            }
            
            if (Runner.TryGetPlayerObject(playerRef, out var playerNetworkObject))
            {
                Debug.Log($"ACTEST DespawnPlayer Despawn"); // todo looks like it didn't reach this part
                Runner.Despawn(playerNetworkObject);
            }
            else
            {
                Debug.LogWarning($"ACTEST Player object for {playerRef} not found in dictionary.");
            }
            
            // Reset player object
            Runner.SetPlayerObject(playerRef, null);
        }
    }

    // not called on host local machine, called if another player joins, meaning local (server) doesn't spawn itself, do it in Spawned()
    public void PlayerJoined(PlayerRef player)
    {
        SpawnPlayer(player);
    }

    public void PlayerLeft(PlayerRef player)
    {
        DespawnPlayer(player);
    }
}
