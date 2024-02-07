using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvasController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button cancelBtn;
    private NetworkRunnerController networkRunnerController;

    private void Start()
    {
        networkRunnerController = GlobalManagers.Instance.NetworkRunnerController;
        
        networkRunnerController.OnStartedRunnerConnection += OnStartedRunnerConnectionHandler;
        networkRunnerController.OnPlayerJoinedSuccessfully += OnPlayerJoinedSuccessfullyHandler;
        
        cancelBtn.onClick.AddListener(networkRunnerController.ShutDownRunner);
        
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        networkRunnerController.OnStartedRunnerConnection -= OnStartedRunnerConnectionHandler;
        networkRunnerController.OnPlayerJoinedSuccessfully -= OnPlayerJoinedSuccessfullyHandler;
    }

    private void OnStartedRunnerConnectionHandler()
    {
        GameObject go = gameObject;
        go.SetActive(true);
        const string CLIP_NAME = "In";
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(go, animator, CLIP_NAME, true));
    }

    private void OnPlayerJoinedSuccessfullyHandler()
    {
        const string CLIP_NAME = "Out";
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, animator, CLIP_NAME, false));
    }
}
