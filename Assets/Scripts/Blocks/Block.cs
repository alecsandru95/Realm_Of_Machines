using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	[SerializeField]
	private long _BlockId = 0;

	private static readonly LogHelper _Log = new LogHelper(typeof(Block));
	
	[SerializeField]
	private float _Mass = -1;

	public long BlockId => _BlockId;
	public float Mass => _Mass;

	private void Awake()
	{
		Debug.Assert(Mass >= 0.5f);
		Debug.Assert(_BlockId > 0);
	}
}