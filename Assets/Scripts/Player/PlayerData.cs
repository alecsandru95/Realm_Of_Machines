using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
	private static readonly LogHelper _Log = new LogHelper(typeof(PlayerData));

	[NonSerialized]
	private Guid _PlayerGuid = Guid.Empty;

	[SerializeField]
	private string _PlayerGuidString = null;

	[SerializeField]
	private string _PlayerName;

	public Guid PlayerGuid
	{
		get
		{
			if (string.IsNullOrEmpty(_PlayerGuidString) == false && _PlayerGuid == Guid.Empty)
			{
				Guid.TryParse(_PlayerGuidString, out _PlayerGuid);
			}
			return _PlayerGuid;
		}
		set
		{
			_PlayerGuid = value;
			_PlayerGuidString = _PlayerGuid.ToString();
		}
	}
	public string PlayerName { get => _PlayerName; set => _PlayerName = value; }
}