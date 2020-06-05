using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PlayerDataSerializer
{
	private static readonly LogHelper _Log = new LogHelper(typeof(PlayerDataSerializer));

#if UNITY_EDITOR
	private static readonly string _PlayerDataFileName = "player_data_editor.json";
#else
	private static readonly string _PlayerDataFileName = "player_data.json";
#endif
	private static readonly string _PlayerDataFullPath = Path.Combine(Application.persistentDataPath, _PlayerDataFileName);

	public static PlayerData ReadPlayerData()
	{
		_Log.WriteInfo($"Reading {_PlayerDataFullPath}");

		if (File.Exists(_PlayerDataFullPath) == false)
		{
			return null;
		}

		try
		{
			using (var streamReader = new StreamReader(_PlayerDataFullPath))
			{
				var allData = streamReader.ReadToEnd();

				var playerData = JsonUtility.FromJson<PlayerData>(allData);
				
				if(playerData.PlayerGuid == Guid.Empty)
				{
					return null;
				}

				return playerData;
			}
		}
		catch (System.Exception exception)
		{
			_Log.WriteError(exception);
			return null;
		}
	}

	public static void SavePlayerData(PlayerData playerData, bool overwrite = true)
	{
		_Log.WriteInfo($"Writing {_PlayerDataFullPath}");

		if (File.Exists(_PlayerDataFullPath) && overwrite == false)
		{
			_Log.WriteWarning($"PlayerData already exists at path {_PlayerDataFullPath}");
			return;
		}

		try
		{
			using (var streamWriter = new StreamWriter(_PlayerDataFullPath))
			{
				var jsonPlayerData = JsonUtility.ToJson(playerData);
				streamWriter.Write(jsonPlayerData);
			}
		}
		catch (System.Exception exception)
		{
			_Log.WriteError(exception);
			throw;
		}
	}
}
