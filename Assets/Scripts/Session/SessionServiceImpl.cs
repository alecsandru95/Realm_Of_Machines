using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Session
{
	public class SessionServiceImpl : SessionService.SessionServiceBase
	{
		private static readonly LogHelper _Log = new LogHelper(typeof(SessionServiceImpl));

		private ServerSession _ServerSession;

		public SessionServiceImpl(ServerSession serverSession)
		{
			_ServerSession = serverSession;
			if (serverSession == null)
			{
				throw new ArgumentNullException(nameof(serverSession));
			}
		}

		public override Task<ConnectionResponseMessage> RequestConnection(ConnectionRequestMessage request, ServerCallContext context)
		{
#if UNITY_EDITOR
			var watch = Stopwatch.StartNew();
#endif

			ConnectionResponseType response;
			string token = "";
			var guid = Guid.Empty;
			string message = "OK";

			if (_ServerSession.RemoteSessionContainer.HasActiveConnetion(request.Guid))
			{
				response = ConnectionResponseType.Refused;
				message = $"User {request.Name} already has active connection!";
			}
			else if (Guid.TryParse(request.Guid, out guid) == false)
			{
				response = ConnectionResponseType.Refused;
				message = $"Guid {request.Guid} not parsable";
			}
			else if (_ServerSession.RemoteSessionContainer.HasConnection(request.Guid))
			{
				response = ConnectionResponseType.Connection;
			}
			else
			{
				response = ConnectionResponseType.NewConnection;

				var playerData = new PlayerData
				{
					PlayerGuid = guid,
					PlayerName = request.Name
				};

				_ServerSession.RemoteSessionContainer.CreateRemoteSession(playerData);
			}

			if (response != ConnectionResponseType.Refused)
			{
				var remoteSession = _ServerSession.RemoteSessionContainer.GetByGuid(guid);

				token = remoteSession.Token;
			}

			_Log.WriteInfo($"Connection message from {request.Name} : {message}");

#if UNITY_EDITOR
			watch.Stop();
			_Log.WriteInfo($"{nameof(RequestConnection)} finnished in {watch.ElapsedMilliseconds}ms");
#endif

			return Task.FromResult(new ConnectionResponseMessage()
			{
				ResponseType = response,
				ResponseToken = token,
				ResponseMessage = message
			});
		}
	}
}