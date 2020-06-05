using Assets.Scripts.Game;
using Assets.Scripts.Scene;
using Grpc.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Session
{
	public class ClientSession : ISession
	{
		private static readonly LogHelper _Log = new LogHelper(typeof(ClientSession));

		private Channel _Channel;
		private SessionService.SessionServiceClient _SessionService;

		public override Type SceneControllerType => typeof(ClientSceneController);

		public override bool IsServer => false;

		protected override void Initialize()
		{
			try
			{
				_Channel = new Channel($"{ServerIP}:{ServerPort}", ChannelCredentials.Insecure);

				_SessionService = new SessionService.SessionServiceClient(_Channel);

				var playerData = GameController.Instance.PlayerData;

				var response = _SessionService.RequestConnection(new ConnectionRequestMessage
				{
					Guid = playerData.PlayerGuid.ToString(),
					Name = playerData.PlayerName
				});

				_Log.WriteInfo(response.ResponseMessage);

				if (response.ResponseType != ConnectionResponseType.Refused)
				{
					Token = response.ResponseToken;
					UnityDispatcher.InvokeUpdate(() =>
					{
						SceneManager.LoadScene(1);
					});
				}
			}
			catch (Exception exception)
			{
				_Log.WriteError(exception);
			}
		}

		protected override void Dispose(bool disposing = true)
		{
			if (_DisposedValue == false)
			{
				if (disposing)
				{
					try
					{
						_SessionService?.Disconnection(new RequestMessage()
						{
							Token = Token
						});
						_Channel?.ShutdownAsync().Wait();

						_SessionService = null;
						_Channel = null;
					}
					catch (Exception exception)
					{
						_Log.WriteError(exception);
					}
				}
				_DisposedValue = true;
			}
		}
	}
}