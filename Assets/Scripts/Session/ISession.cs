using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Session
{
	public abstract class ISession : IDisposable
	{
		private readonly object _Lock = new object();
		private string _Token = null;

		public string ServerIP { get; private set; }
		public int ServerPort { get; private set; }

		public ClientPlayer ClientPlayer { get; private set; }

		public string Token
		{
			get
			{
				lock (_Lock)
				{
					return _Token;
				}
			}
			protected set
			{
				lock (_Lock)
				{
					_Token = value;
				}
			}
		}

		public ConnectionResponseType ConnectionResponseType { get; protected set; } = ConnectionResponseType.Refused;
		public abstract Type SceneControllerType { get; }
		public abstract bool IsServer { get; }

		protected bool _DisposedValue;

		public void Initialize(string serverIP, int serverPort)
		{
			ServerIP = serverIP;
			ServerPort = serverPort;

			Task.Run(() => Initialize());
		}
		protected abstract void Initialize();

		protected abstract void Dispose(bool disposing);

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~ISession()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}