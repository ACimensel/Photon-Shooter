using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkRunnerController : MonoBehaviour, INetworkRunnerCallbacks
{
	const int MAIN_SCENE_IDX = 1;
	
	[SerializeField] private NetworkRunner networkRunnerPrefab;

	private NetworkRunner networkRunnerInstance;
	
	public async void StartGame(GameMode mode, string roomName)
	{
		if (networkRunnerInstance == null)
		{
			networkRunnerInstance = Instantiate(networkRunnerPrefab);
		}
		
		// Register so we will get the callbacks
		networkRunnerInstance.AddCallbacks(this);

		// networkRunnerInstance.ProvideInput = true; //todo will need later

		var startGameArgs = new StartGameArgs()
		{
			GameMode = mode,
			SessionName = roomName,
			PlayerCount = 4, // max player count
			SceneManager = networkRunnerInstance.GetComponent<INetworkSceneManager>()
		};

		var result = await networkRunnerInstance.StartGame(startGameArgs);

		if (result.Ok)
		{
			networkRunnerInstance.LoadScene(SceneRef.FromIndex(MAIN_SCENE_IDX));
		}
		else
		{
			Debug.LogError($"Failed to start: {result.ShutdownReason}");
		}
	}

	public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
	{
		Debug.Log("OnObjectExitAOI");
	}

	public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
	{
		Debug.Log("OnObjectEnterAOI");
	}

	public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
	{
		Debug.Log("OnPlayerJoined");
	}

	public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
	{
		Debug.Log("OnPlayerLeft");
	}

	public void OnInput(NetworkRunner runner, NetworkInput input)
	{
		Debug.Log("OnInput");
	}

	public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
	{
		Debug.Log("OnInputMissing");
	}

	public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
	{
		Debug.Log("OnShutdown");
	}

	public void OnConnectedToServer(NetworkRunner runner)
	{
		Debug.Log("OnConnectedToServer");
	}

	public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
	{
		Debug.Log("OnDisconnectedFromServer");
	}

	public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
	{
		Debug.Log("OnConnectRequest");
	}

	public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
	{
		Debug.Log("OnConnectFailed");
	}

	public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
	{
		Debug.Log("OnUserSimulationMessage");
	}

	public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
	{
		Debug.Log("OnSessionListUpdated");
	}

	public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
	{
		Debug.Log("OnCustomAuthenticationResponse");
	}

	public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
	{
		Debug.Log("OnHostMigration");
	}

	public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
	{
		Debug.Log("OnReliableDataReceived");
	}

	public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
	{
		Debug.Log("OnReliableDataProgress");
	}

	public void OnSceneLoadDone(NetworkRunner runner)
	{
		Debug.Log("OnSceneLoadDone");
	}

	public void OnSceneLoadStart(NetworkRunner runner)
	{
		Debug.Log("OnSceneLoadStart");
	}
}
