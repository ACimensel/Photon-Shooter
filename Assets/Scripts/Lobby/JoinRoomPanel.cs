using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomPanel : LobbyPanelBase
{
	
	[Header("Join Room Panel Vars")]
	[SerializeField] private Button joinRandomRoomBtn;
	[SerializeField] private Button joinByArgRoomBtn;
	[SerializeField] private Button createRoomBtn;
	[SerializeField] private TMP_InputField joinRoomByArgInputField;
	[SerializeField] private TMP_InputField createRoomInputField;

	private NetworkRunnerController networkRunnerController;
	
	public override void InitPanel(LobbyUIManager lobbyUIManager)
	{
		base.InitPanel(lobbyUIManager);

		networkRunnerController = GlobalManagers.Instance.NetworkRunnerController;
		
		joinRandomRoomBtn.onClick.AddListener(JoinRandomRoom);
		joinByArgRoomBtn.onClick.AddListener(() => CreateRoom(GameMode.Client, joinRoomByArgInputField.text));
		createRoomBtn.onClick.AddListener(() => CreateRoom(GameMode.Host, createRoomInputField.text));
	}

	private void JoinRandomRoom()
	{
		networkRunnerController.StartGame(GameMode.AutoHostOrClient, "");
	}

	private void CreateRoom(GameMode mode, string roomName)
	{
		if (roomName.Length >= 2)
		{
			networkRunnerController.StartGame(mode, roomName);
		}
	}
}
