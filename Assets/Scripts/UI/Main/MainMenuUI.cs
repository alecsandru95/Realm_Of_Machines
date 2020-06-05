using Assets.Scripts.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private static readonly LogHelper _Log = new LogHelper(typeof(MainMenuUI));

        [SerializeField]
        private Button _HostButton;
        [SerializeField]
        private Button _JoinButton;

		private void Start()
		{
			if(_HostButton != null)
			{
				_HostButton.onClick.AddListener(OnHostButtonClicked);
			}

			if(_JoinButton != null)
			{
				_JoinButton.onClick.AddListener(OnJoinButtonClicked);
			}
		}

		private void OnHostButtonClicked()
		{
			var session = Game.GameController.Instance.CurrentSession = new ServerSession();
			session.Initialize("localhost", 50001);
		}
		private void OnJoinButtonClicked()
		{
			var session = Game.GameController.Instance.CurrentSession = new ClientSession();
			session.Initialize("localhost", 50001);
		}
	}
}