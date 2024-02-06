using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NicknamePanel : LobbyPanelBase
{
	[Header("Nickname Panel Vars")]
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private Button createNicknameBtn;
	private const int MIN_CHAR_FOR_NICKNAME = 2;
	
	public override void InitPanel(LobbyUIManager lobbyUIManager)
	{
		base.InitPanel(lobbyUIManager);
		createNicknameBtn.interactable = false;
		createNicknameBtn.onClick.AddListener(OnClickCreateNickname);
		inputField.onValueChanged.AddListener(OnInputValueChanged);
	}

	private void OnClickCreateNickname()
	{
		string nickname = inputField.text;
		if (nickname.Length >= MIN_CHAR_FOR_NICKNAME)
		{
			ClosePanel();
			lobbyUIManager.ShowPanel(LobbyPanelType.JoinRoomPanel);
			//todo
		}
	}

	private void OnInputValueChanged(string inputValue)
	{
		createNicknameBtn.interactable = inputValue.Length >= MIN_CHAR_FOR_NICKNAME;
	}
}
