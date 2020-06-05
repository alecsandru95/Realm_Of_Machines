using Assets.Scripts.Synchronization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
	[RequireComponent(typeof(Rigidbody))]
	public abstract class Entity : MonoBehaviour, ISynchronizable
	{
		private static readonly LogHelper _Log = new LogHelper(typeof(Entity));

		private Rigidbody __Rigidbody;
		protected Rigidbody _Rigidbody
		{
			get
			{
				if (__Rigidbody == null)
				{
					__Rigidbody = GetComponent<Rigidbody>();
				}
				return __Rigidbody;
			}
		}

		public ulong SyncID { get; set; }

		private HashSet<Block> _BlockSet = null;

		protected virtual void Awake()
		{
			_Log.WriteInfo(name);
		}

		protected virtual void Start()
		{
			_BlockSet = new HashSet<Block>(GetComponentsInChildren<Block>());
			RecalculateRigidbody();
		}

		public void RecalculateRigidbody()
		{
			var localCenterOfMass = Vector3.zero;
			var totalMass = 0f;
			foreach (var block in _BlockSet)
			{
				localCenterOfMass += block.transform.localPosition * block.Mass;
				totalMass += block.Mass;
			}

			if (totalMass > 0.005f)
			{
				localCenterOfMass /= totalMass;
				_Rigidbody.centerOfMass = localCenterOfMass;
				_Rigidbody.mass = totalMass;
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(_Rigidbody.worldCenterOfMass, 0.125f);
		}
	}
}