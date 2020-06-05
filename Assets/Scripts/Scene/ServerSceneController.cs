using Assets.Scripts.Game;
using Assets.Scripts.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Scene
{
	public class ServerSceneController : SceneController
	{
		[SerializeField]
		private RemoteSessionContainer _RemoteSessionContainer;
		private ServerSession _ServerSession;

		private void Awake()
		{
			_ServerSession = GameController.Instance.CurrentSession as ServerSession;
			_RemoteSessionContainer = _ServerSession.RemoteSessionContainer;
		}
	}
}
