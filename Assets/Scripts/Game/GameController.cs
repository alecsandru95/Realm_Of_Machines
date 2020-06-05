using Assets.Scripts.Player;
using Assets.Scripts.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
	public class GameController
	{
		public static readonly GameController Instance = new GameController();

		public PlayerData PlayerData { get; private set; }

		public ISession CurrentSession { get; set; }

		public bool IsServer => CurrentSession.IsServer;

		private GameController()
		{
			Application.wantsToQuit += Application_wantsToQuit;

			PlayerData = PlayerDataSerializer.ReadPlayerData();

			if (PlayerData == null)
			{
				PlayerData = new PlayerData()
				{
					PlayerGuid = Guid.NewGuid(),
					PlayerName = "Alec"
				};

#if UNITY_EDITOR
				PlayerData.PlayerName = "Alec_Editor";
#elif UNITY_ANDROID
			PlayerData.PlayerName = "Alec_Android";
#endif
				PlayerDataSerializer.SavePlayerData(PlayerData);
			}
		}

		private bool Application_wantsToQuit()
		{
			CurrentSession?.Dispose();
			return true;
		}
	}
}