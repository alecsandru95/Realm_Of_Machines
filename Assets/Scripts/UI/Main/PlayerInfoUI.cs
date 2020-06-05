using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField]
    private Text _TxtPlayerName;

	private void Start()
	{
		if (_TxtPlayerName != null)
		{
			_TxtPlayerName.text = GameController.Instance.PlayerData?.PlayerName;
		}
	}
}
