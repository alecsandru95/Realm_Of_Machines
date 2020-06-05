using Assets.Scripts.Blocks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class UnityDispatcher : MonoBehaviour
    {
        private static readonly LogHelper _Log = new LogHelper(typeof(UnityDispatcher));
		private static UnityDispatcher _Instance = null;

		private readonly object _Lock = new object();
		private Queue<Action> _UpdateActions = new Queue<Action>();

		[SerializeField]
		private long _TotalQueuedActions = 0;

		private void Awake()
		{
			if (_Instance == null)
			{
				DontDestroyOnLoad(this);
				_Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}

			BlockDictionary.Instance.LoadDictionary();
		}

		public static void InvokeUpdate(Action action)
		{
			_Instance.InvokeUpdateInternal(action);
		}
		private void InvokeUpdateInternal(Action action)
		{
			lock (_Lock)
			{
				_UpdateActions.Enqueue(action);
			}
		}
		private void Update()
		{
			List<Action> actionsList = null;
			lock (_Lock)
			{
				actionsList = _UpdateActions.ToList();
				_UpdateActions.Clear();
				_TotalQueuedActions += actionsList.Count;
			}

			foreach (var action in actionsList)
			{
				try
				{
					action.Invoke();
				}
				catch (Exception exception)
				{
					_Log.WriteError(exception);
				}
			}
		}
	}
}