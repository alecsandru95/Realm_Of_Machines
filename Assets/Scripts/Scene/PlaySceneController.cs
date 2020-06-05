using Assets.Scripts.Game;
using Assets.Scripts.Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneController : MonoBehaviour
{

	private void Awake()
	{
		if (GameController.Instance.CurrentSession != null)
		{
			gameObject.AddComponent(GameController.Instance.CurrentSession.SceneControllerType);
		}
		else
		{
			gameObject.AddComponent(typeof(ServerSceneController));
		}
	}
}