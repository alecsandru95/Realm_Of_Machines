using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Session
{
	[Serializable]
	public class RemoteSessionContainer
	{
		private static readonly LogHelper _Log = new LogHelper(typeof(RemoteSessionContainer));

		private readonly object _Lock = new object();

		[SerializeField]
		private List<RemoteSession> _RemoteList = new List<RemoteSession>();
		private Dictionary<string, RemoteSession> _RemoteMapByGuid = new Dictionary<string, RemoteSession>();
		private Dictionary<string, RemoteSession> _RemoteMap = new Dictionary<string, RemoteSession>();

		internal bool HasActiveConnetion(string guid)
		{
			return HasConnection(guid);
		}

		internal bool HasConnection(string guid)
		{
			lock (_Lock)
			{
				return _RemoteMapByGuid.ContainsKey(guid);
			}
		}

		internal void CreateRemoteSession(PlayerData playerData)
		{
			lock (_Lock)
			{
				try
				{
					if (_RemoteMapByGuid.ContainsKey(playerData.PlayerGuid.ToString()))
					{
						return;
					}

					var remoteSession = new RemoteSession
					{
						Token = Guid.NewGuid().ToString(),
						PlayerData = playerData
					};

					_RemoteMapByGuid.Add(playerData.PlayerGuid.ToString(), remoteSession);
					_RemoteMap.Add(remoteSession.Token, remoteSession);
					_RemoteList.Add(remoteSession);
				}
				catch(Exception exception)
				{
					_Log.WriteError(exception);
				}
			}
		}

		internal RemoteSession GetByGuid(Guid guid)
		{
			lock (_Lock)
			{
				try
				{
					if (_RemoteMapByGuid.ContainsKey(guid.ToString()))
					{
						return _RemoteMapByGuid[guid.ToString()];
					}

					return null;
				}
				catch (Exception exception)
				{
					_Log.WriteError(exception);
					return null;
				}
			}
		}
	}
}