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

		public PlayerData PlayerData { get => _PlayerData; internal set => _PlayerData = value; }
		public string Token { get; internal set; }
	}
}
