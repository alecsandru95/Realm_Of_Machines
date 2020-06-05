using Assets.Scripts.Game;
using Assets.Scripts.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Scene
{
	public abstract class SceneController : MonoBehaviour
	{
		public PlayerData PlayerData => GameController.Instance.PlayerData;
		public ISession Session => GameController.Instance.CurrentSession;
	}
}