using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Session
{
	[Serializable]
	public class RemoteSession
	{
		[SerializeField]
		private PlayerData _PlayerData;
		private bool _Active = true;

		public PlayerData PlayerData { get => _PlayerData; internal set => _PlayerData = value; }
		public string Token { get; internal set; }
		public bool IsActive => _Active;

		internal void Disconnected()
		{
			_Active = false;
		}
		internal void Connected()
		{
			_Active = true;
		}
	}
}
