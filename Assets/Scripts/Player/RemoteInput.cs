using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
	[Serializable]
	public class RemoteInput
	{
		[SerializeField]
		private float _LastVertical = 0;
		[SerializeField]
		private float _LastHorizontal = 0;
	}
}