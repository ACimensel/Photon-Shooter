using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanelBase : MonoBehaviour
{
	const string POP_IN_CLIP_NAME = "In";
	const string POP_OUT_CLIP_NAME = "Out";
	
	[field: SerializeField, Header("Lobby Panel Base Vars")]
	public LobbyPanelType panelType { get; private set; }
	[SerializeField] private Animator panelAnimator;
	
	protected LobbyUIManager lobbyUIManager;

	public enum LobbyPanelType
	{
		None,
		NicknamePanel,
		JoinRoomPanel,
	}
	
	public virtual void InitPanel(LobbyUIManager uiManager)
	{
		lobbyUIManager = uiManager;
	}
	
	public void ShowPanel()
	{
		gameObject.SetActive(true);
		
		panelAnimator.Play(POP_IN_CLIP_NAME);
	}

	protected void ClosePanel()
	{
		StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, panelAnimator, POP_OUT_CLIP_NAME, false));
	}
}
