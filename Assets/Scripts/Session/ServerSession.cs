using Assets.Scripts.Game;
using Assets.Scripts.Scene;
using Grpc.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Session
{
	public class ServerSession : ISession
	{
		private static readonly LogHelper _Log = new LogHelper(typeof(ServerSession));
		private readonly object _Lock = new object();

		private Server _Server;

		public RemoteSessionContainer RemoteSessionContainer { get; private set; } = new RemoteSessionContainer();

		public override Type SceneControllerType => typeof(ServerSceneController);

		public override bool IsServer => true;

		protected override void Initialize()
		{
			try
			{
				_Server = new Server
				{
					Services = { SessionService.BindService(new SessionServiceImpl(this)) },
					Ports = { new ServerPort(ServerIP, ServerPort, ServerCredentials.Insecure) }
				};
				_Server.Start();

				_Log.WriteInfo($"Started on {ServerIP}:{ServerPort}");

				RemoteSessionContainer.CreateRemoteSession(GameController.Instance.PlayerData);

				UnityDispatcher.InvokeUpdate(() =>
				{
					SceneManager.LoadScene(1, LoadSceneMode.Single);
				});
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
					_Log.WriteInfo("disposing");
					_Server?.ShutdownAsync().Wait();
					_DisposedValue = true;
				}
			}
		}
	}
}