using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	private static readonly LogHelper _Log = new LogHelper(typeof(Block));
	[SerializeField]
	private float _Mass;

	public float Mass => _Mass;

	private void Awake()
	{
		Debug.Assert(Mass >= 0.5f);
	}
}