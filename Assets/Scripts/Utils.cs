using System.Collections;
using UnityEngine;

public static class Utils
{
	public static IEnumerator PlayAnimAndSetStateWhenFinished(GameObject parent, Animator animator, string clipName, bool setActive)
	{
		animator.Play(clipName);
		float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
		
		yield return new WaitForSeconds(animationLength);
		
		parent.SetActive(setActive);
	}
}
